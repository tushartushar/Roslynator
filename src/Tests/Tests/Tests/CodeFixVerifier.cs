﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Text;
using Roslynator.Tests.Text;
using Xunit;
using static Roslynator.Tests.CompilerDiagnosticVerifier;

namespace Roslynator.Tests
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public abstract class CodeFixVerifier : DiagnosticVerifier
    {
        public abstract CodeFixProvider FixProvider { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"{Descriptor.Id} {Analyzer.GetType().Name} {FixProvider.GetType().Name}"; }
        }

        public async Task VerifyDiagnosticAndFixAsync(
            string source,
            string expected,
            IEnumerable<(string source, string expected)> additionalData = null,
            CodeVerificationOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            SpanParserResult result = SpanParser.GetSpans(source);

            IEnumerable<Diagnostic> diagnostics = result.Spans.Select(f => CreateDiagnostic(f.Span, f.LineSpan));

            string[] additionalSources = additionalData?.Select(f => f.source).ToArray() ?? Array.Empty<string>();

            await VerifyDiagnosticAsync(
                result.Text,
                diagnostics,
                additionalSources: additionalSources,
                options: options,
                cancellationToken: cancellationToken).ConfigureAwait(false);

            await VerifyFixAsync(
                result.Text,
                expected,
                additionalData,
                options,
                cancellationToken).ConfigureAwait(false);
        }

        public async Task VerifyDiagnosticAndFixAsync(
            string theory,
            string fromData,
            string toData,
            CodeVerificationOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            (TextSpan span, string source, string expected) = SpanParser.ReplaceSpan(theory, fromData, toData);

            SpanParserResult result = SpanParser.GetSpans(source);

            if (result.Spans.Any())
            {
                IEnumerable<Diagnostic> diagnostics = result.Spans.Select(f => CreateDiagnostic(f.Span, f.LineSpan));

                await VerifyDiagnosticAsync(result.Text, diagnostics, additionalSources: null, options: options, cancellationToken).ConfigureAwait(false);

                await VerifyFixAsync(result.Text, expected, additionalData: null, options, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await VerifyDiagnosticAsync(source, span, options, cancellationToken).ConfigureAwait(false);

                await VerifyFixAsync(source, expected, additionalData: null, options, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task VerifyFixAsync(
            string theory,
            string fromData,
            string toData,
            CodeVerificationOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            (TextSpan span, string source, string expected) = SpanParser.ReplaceSpan(theory, fromData, toData);

            await VerifyFixAsync(
                source: source,
                expected: expected,
                options: options,
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        //TODO: add parameter equivalenceKey
        public async Task VerifyFixAsync(
            string source,
            string expected,
            IEnumerable<(string source, string expected)> additionalData = null,
            CodeVerificationOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!FixProvider.CanFixAny(Analyzer.SupportedDiagnostics))
                Assert.True(false, $"Code fix provider '{FixProvider.GetType().Name}' cannot fix any diagnostic supported by analyzer '{Analyzer}'.");

            Document document = CreateDocument(source);

            List<(DocumentId documentId, string expected)> additionalDocumentData = null;

            if (additionalData != null)
            {
                Project project = document.Project;

                additionalDocumentData = new List<(DocumentId documentId, string expected)>();

                int i = 1;
                foreach ((string source2, string expected2) in additionalData)
                {
                    Document newDocument = project.AddDocument(CreateFileName(i), SourceText.From(source2));
                    additionalDocumentData.Add((newDocument.Id, expected2));
                    project = newDocument.Project;

                    i++;
                }

                document = project.GetDocument(document.Id);
            }

            Compilation compilation = await document.Project.GetCompilationAsync(cancellationToken).ConfigureAwait(false);

            ImmutableArray<Diagnostic> compilerDiagnostics = compilation.GetDiagnostics(cancellationToken);

            if (options == null)
                options = Options;

            VerifyCompilerDiagnostics(compilerDiagnostics, options);

            if (options.EnableDiagnosticsDisabledByDefault)
                compilation = compilation.EnableDiagnosticsDisabledByDefault(Analyzer);

            ImmutableArray<Diagnostic> diagnostics = await compilation.GetAnalyzerDiagnosticsAsync(Analyzer, DiagnosticComparer.SpanStart, cancellationToken).ConfigureAwait(false);

            ImmutableArray<string> fixableDiagnosticIds = FixProvider.FixableDiagnosticIds;

            bool fixRegistered = false;

            while (diagnostics.Length > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Diagnostic diagnostic = FindFirstFixableDiagnostic();

                if (diagnostic == null)
                    break;

                CodeAction action = null;

                var context = new CodeFixContext(
                    document,
                    diagnostic,
                    (a, d) =>
                    {
                        if (d.Contains(diagnostic)
                            && action == null)
                        {
                            action = a;
                        }
                    },
                    CancellationToken.None);

                await FixProvider.RegisterCodeFixesAsync(context).ConfigureAwait(false);

                if (action == null)
                    break;

                fixRegistered = true;

                document = await document.ApplyCodeActionAsync(action).ConfigureAwait(false);

                compilation = await document.Project.GetCompilationAsync(cancellationToken).ConfigureAwait(false);

                ImmutableArray<Diagnostic> newCompilerDiagnostics = compilation.GetDiagnostics(cancellationToken);

                VerifyCompilerDiagnostics(newCompilerDiagnostics, options);

                if (!options.AllowNewCompilerDiagnostics)
                    VerifyNoNewCompilerDiagnostics(compilerDiagnostics, newCompilerDiagnostics, options);

                if (options.EnableDiagnosticsDisabledByDefault)
                    compilation = compilation.EnableDiagnosticsDisabledByDefault(Analyzer);

                diagnostics = await compilation.GetAnalyzerDiagnosticsAsync(Analyzer, DiagnosticComparer.SpanStart, cancellationToken).ConfigureAwait(false);
            }

            Assert.True(fixRegistered, "No code fix has been registered.");

            string actual = await document.ToFullStringAsync(simplify: true, format: true).ConfigureAwait(false);

            Assert.Equal(expected, actual);

            if (additionalDocumentData != null)
            {
                Project project = document.Project;

                foreach ((DocumentId documentId, string expected2) in additionalDocumentData)
                {
                    Document document2 = project.GetDocument(documentId);

                    string actual2 = await document2.ToFullStringAsync(simplify: true, format: true).ConfigureAwait(false);

                    Assert.Equal(expected2, actual2);
                }
            }

            Diagnostic FindFirstFixableDiagnostic()
            {
                foreach (Diagnostic diagnostic in diagnostics)
                {
                    if (fixableDiagnosticIds.Contains(diagnostic.Id))
                        return diagnostic;
                }

                return null;
            }
        }

        public async Task VerifyNoFixAsync(
            string source,
            IEnumerable<string> additionalSources = null,
            CodeVerificationOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            Document document = CreateDocument(source);

            if (additionalSources != null)
            {
                Project project = document.Project;

                int i = 1;
                foreach (string additionalSource in additionalSources)
                {
                    project = project
                        .AddDocument(CreateFileName(i), SourceText.From(additionalSource))
                        .Project;

                    i++;
                }

                document = project.GetDocument(document.Id);
            }

            Compilation compilation = await document.Project.GetCompilationAsync(cancellationToken).ConfigureAwait(false);

            ImmutableArray<Diagnostic> compilerDiagnostics = compilation.GetDiagnostics(cancellationToken);

            if (options == null)
                options = Options;

            VerifyCompilerDiagnostics(compilerDiagnostics, options);

            if (options.EnableDiagnosticsDisabledByDefault)
                compilation = compilation.EnableDiagnosticsDisabledByDefault(Analyzer);

            ImmutableArray<Diagnostic> diagnostics = await compilation.GetAnalyzerDiagnosticsAsync(Analyzer, DiagnosticComparer.SpanStart, cancellationToken).ConfigureAwait(false);

            ImmutableArray<string> fixableDiagnosticIds = FixProvider.FixableDiagnosticIds;

            foreach (Diagnostic diagnostic in diagnostics)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (!string.Equals(diagnostic.Id, Descriptor.Id, StringComparison.Ordinal))
                    continue;

                if (!fixableDiagnosticIds.Contains(diagnostic.Id))
                    continue;

                var context = new CodeFixContext(
                    document,
                    diagnostic,
                    (_, d) => Assert.True(!d.Contains(diagnostic), "No code fix expected."),
                    CancellationToken.None);

                await FixProvider.RegisterCodeFixesAsync(context).ConfigureAwait(false);
            }
        }
    }
}
