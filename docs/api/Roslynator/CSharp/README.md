# Roslynator\.CSharp Namespace

## Classes

| Class | Summary |
| ----- | ------- |
| [CSharpExtensions](CSharpExtensions/README.md) | A set of extension methods for a [SemanticModel](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.semanticmodel)\. |
| [CSharpFactory](CSharpFactory/README.md) | A factory for syntax nodes, tokens and trivia\. This class is built on top of [SyntaxFactory](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxfactory) members\. |
| [CSharpFacts](CSharpFacts/README.md) | |
| [EnumExtensions](EnumExtensions/README.md) | A set of extension methods for enumerations\. |
| [MemberDeclarationListSelection](MemberDeclarationListSelection/README.md) | Represents selected member declarations in a [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |
| [ModifierList](ModifierList/README.md) | A set of static methods that allows manipulation with modifiers\. |
| [ModifierList\<TNode>](ModifierList-1/README.md) | Represents a list of modifiers\. |
| [Modifiers](Modifiers/README.md) | Serves as a factory for a modifier list\. |
| [StatementListSelection](StatementListSelection/README.md) | Represents selected statements in a [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |
| [SymbolExtensions](SymbolExtensions/README.md) | A set of static methods for [ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol) and derived types\. |
| [SyntaxAccessibility](SyntaxAccessibility/README.md) | A set of static methods that are related to C\# accessibility\. |
| [SyntaxExtensions](SyntaxExtensions/README.md) | A set of extension methods for syntax \(types derived from [CSharpSyntaxNode](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxnode)\)\. |
| [SyntaxInfo](SyntaxInfo/README.md) | Serves as a factory for types in Roslynator\.CSharp\.Syntax namespace\. |
| [WorkspaceExtensions](WorkspaceExtensions/README.md) | A set of extension methods for the workspace layer\. |
| [WorkspaceSyntaxExtensions](WorkspaceSyntaxExtensions/README.md) | A set of extension methods for syntax\. These methods are dependent on the workspace layer\. |

## Structs

| Struct | Summary |
| ------ | ------- |
| [ExpressionChain](ExpressionChain/README.md) | Enables to enumerate expressions of a binary expression and expressions of nested binary expressions of the same kind as parent binary expression\. |
| [ExpressionChain.Enumerator](ExpressionChain/Enumerator/README.md) | |
| [ExpressionChain.Reversed](ExpressionChain/Reversed/README.md) | Enables to enumerate expressions of [ExpressionChain](ExpressionChain/README.md) in a reversed order\. |
| [ExpressionChain.Reversed.Enumerator](ExpressionChain/Reversed/Enumerator/README.md) | |
| [IfStatementCascade](IfStatementCascade/README.md) | Enables to enumerate if statement cascade\. |
| [IfStatementCascade.Enumerator](IfStatementCascade/Enumerator/README.md) | |
| [IfStatementOrElseClause](IfStatementOrElseClause/README.md) | A wrapper for either an [IfStatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.ifstatementsyntax) or an [ElseClauseSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.elseclausesyntax)\. |

## Enums

| Enum | Summary |
| ---- | ------- |
| [CommentKinds](CommentKinds/README.md) | Specifies C\# comments\. |
| [ModifierKinds](ModifierKinds/README.md) | Specifies C\# modifier\. |
| [NullCheckStyles](NullCheckStyles/README.md) | Specifies a null check\. |
| [PreprocessorDirectiveKinds](PreprocessorDirectiveKinds/README.md) | Specifies C\# preprocessor directives\. |
