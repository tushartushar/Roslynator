﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Roslynator.Utilities;

namespace Roslynator.Documentation
{
    internal static class Program
    {
        private static readonly UTF8Encoding _utf8NoBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

        [SuppressMessage("Redundancy", "RCS1163:Unused parameter.", Justification = "<Pending>")]
        private static void Main(string[] args)
        {
            GenerateDocumentation(@"..\..\..\..\..\..\docs\api", "Roslynator API", "Roslynator.CSharp.dll");
            GenerateDocumentation(@"..\..\..\..\..\..\docs\apitest", "Foo API", "Roslynator.Documentation.DocTest.dll");
        }

        private static void GenerateDocumentation(string directoryPath, string heading, params string[] assemblyNames)
        {
            ImmutableArray<DocumentationSource> sources = assemblyNames
                .Select(DocumentationSource.CreateFromAssemblyName)
                .ToImmutableArray();

            var generator = new DocumentationGenerator(sources);

            foreach (DocumentationFile documentationFile in generator.GenerateFiles(heading))
            {
                string path = directoryPath;

                if (documentationFile.DirectoryPath != null)
                    path = directoryPath + @"\" + string.Join(@"\", documentationFile.DirectoryPath);

                path += @"\README.md";

                Directory.CreateDirectory(Path.GetDirectoryName(path));

                FileHelper.WriteAllText(path, documentationFile.Content, _utf8NoBom, onlyIfChanges: true, fileMustExists: false);
            }
        }
    }
}
