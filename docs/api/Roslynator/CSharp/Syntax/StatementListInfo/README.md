# StatementListInfo Struct

[Properties](#properties) &#x2022; [Methods](#methods) &#x2022; [Operators](#operators) &#x2022; [Explicit Interface Implementations](#explicit-interface-implementations)

**Namespace**: [Roslynator.CSharp.Syntax](../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Provides information about a list of statements\.

```csharp
public readonly struct StatementListInfo : System.IEquatable<StatementListInfo>,
    System.Collections.Generic.IEnumerable<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax>,
    System.Collections.Generic.IReadOnlyCollection<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax>,
    System.Collections.Generic.IReadOnlyList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; StatementListInfo

### Implements

* [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[StatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.statementsyntax)>
* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[StatementListInfo](./README.md)>
* [IReadOnlyCollection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)\<[StatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.statementsyntax)>
* [IReadOnlyList](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)\<[StatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.statementsyntax)>

## Properties

| Property | Summary |
| -------- | ------- |
| [Count](Count/README.md) | The number of statement in the list\. \(Implements [IReadOnlyCollection\<StatementSyntax>.Count](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1.count)\) |
| [IsParentBlock](IsParentBlock/README.md) | Determines whether the statements are contained in a [BlockSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.blocksyntax)\. |
| [IsParentSwitchSection](IsParentSwitchSection/README.md) | Determines whether the statements are contained in a [SwitchSectionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.switchsectionsyntax)\. |
| [Item\[Int32\]](Item/README.md) | Gets the statement at the specified index in the list\. \(Implements [IReadOnlyList\<StatementSyntax>.Item](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1.item)\) |
| [Parent](Parent/README.md) | The node that contains the statements\. It can be either a [BlockSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.blocksyntax) or a [SwitchSectionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.switchsectionsyntax)\. |
| [ParentAsBlock](ParentAsBlock/README.md) | Gets a block that contains the statements\. Returns null if the statements are not contained in a block\. |
| [ParentAsSwitchSection](ParentAsSwitchSection/README.md) | Gets a switch section that contains the statements\. Returns null if the statements are not contained in a switch section\. |
| [Statements](Statements/README.md) | The list of statements\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Add(StatementSyntax)](Add/README.md) | Creates a new [StatementListInfo](./README.md) with the specified statement added at the end\. |
| [AddRange(IEnumerable\<StatementSyntax>)](AddRange/README.md) | Creates a new [StatementListInfo](./README.md) with the specified statements added at the end\. |
| [Any()](Any/README.md) | True if the list has at least one statement\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. \(Overrides [ValueType.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.equals)\) |
| [Equals(StatementListInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. \(Implements [IEquatable\<StatementListInfo>.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1.equals)\) |
| [First()](First/README.md) | The first statement in the list\. |
| [FirstOrDefault()](FirstOrDefault/README.md) | The first statement in the list or null if the list is empty\. |
| [GetEnumerator()](GetEnumerator/README.md) | Gets the enumerator the list of statements\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. \(Overrides [ValueType.GetHashCode](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.gethashcode)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [IndexOf(Func\<StatementSyntax, Boolean>)](IndexOf/README.md) | Searches for a statement that matches the predicate and returns returns zero\-based index of the first occurrence in the list\. |
| [IndexOf(StatementSyntax)](IndexOf/README.md) | The index of the statement in the list\. |
| [Insert(Int32, StatementSyntax)](Insert/README.md) | Creates a new [StatementListInfo](./README.md) with the specified statement inserted at the index\. |
| [InsertRange(Int32, IEnumerable\<StatementSyntax>)](InsertRange/README.md) | Creates a new [StatementListInfo](./README.md) with the specified statements inserted at the index\. |
| [Last()](Last/README.md) | The last statement in the list\. |
| [LastIndexOf(Func\<StatementSyntax, Boolean>)](LastIndexOf/README.md) | Searches for a statement that matches the predicate and returns returns zero\-based index of the last occurrence in the list\. |
| [LastIndexOf(StatementSyntax)](LastIndexOf/README.md) | Searches for a statement and returns zero\-based index of the last occurrence in the list\. |
| [LastOrDefault()](LastOrDefault/README.md) | The last statement in the list or null if the list is empty\. |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [Remove(StatementSyntax)](Remove/README.md) | Creates a new [StatementListInfo](./README.md) with the specified statement removed\. |
| [RemoveAt(Int32)](RemoveAt/README.md) | Creates a new [StatementListInfo](./README.md) with the statement at the specified index removed\. |
| [RemoveNode(SyntaxNode, SyntaxRemoveOptions)](RemoveNode/README.md) | Creates a new [StatementListInfo](./README.md) with the specified node removed\. |
| [Replace(StatementSyntax, StatementSyntax)](Replace/README.md) | Creates a new [StatementListInfo](./README.md) with the specified statement replaced with the new statement\. |
| [ReplaceAt(Int32, StatementSyntax)](ReplaceAt/README.md) | Creates a new [StatementListInfo](./README.md) with the statement at the specified index replaced with a new statement\. |
| [ReplaceNode(SyntaxNode, SyntaxNode)](ReplaceNode/README.md) | Creates a new [StatementListInfo](./README.md) with the specified old node replaced with a new node\. |
| [ReplaceRange(StatementSyntax, IEnumerable\<StatementSyntax>)](ReplaceRange/README.md) | Creates a new [StatementListInfo](./README.md) with the specified statement replaced with new statements\. |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. \(Overrides [ValueType.ToString](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.tostring)\) |
| [WithStatements(IEnumerable\<StatementSyntax>)](WithStatements/README.md) | Creates a new [StatementListInfo](./README.md) with the statements updated\. |
| [WithStatements(SyntaxList\<StatementSyntax>)](WithStatements/README.md) | Creates a new [StatementListInfo](./README.md) with the statements updated\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(StatementListInfo, StatementListInfo)](op_Equality/README.md) | |
| [Inequality(StatementListInfo, StatementListInfo)](op_Inequality/README.md) | |

## Explicit Interface Implementations

| Member | Summary |
| ------ | ------- |
| [IEnumerable.GetEnumerator()](System-Collections-IEnumerable-GetEnumerator/README.md) | |
| [IEnumerable\<StatementSyntax>.GetEnumerator()](System-Collections-Generic-IEnumerable-Microsoft-CodeAnalysis-CSharp-Syntax-StatementSyntax--GetEnumerator/README.md) | |
