namespace DatabaseUtil.SourceGen;

using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

public sealed class NamedTypeSymbol
{
	public NamedTypeSymbol(string name, ITypeSymbol symbol, ImmutableArray<AttributeData> attributes)
	{
		Name = name;
		Symbol = symbol;
		Attributes = attributes;
	}
	public string Name { get; }
	public ITypeSymbol Symbol { get; }
	public ImmutableArray<AttributeData> Attributes { get; }
}