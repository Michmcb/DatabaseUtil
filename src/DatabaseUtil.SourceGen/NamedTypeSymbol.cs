namespace DatabaseUtil.SourceGen;

using Microsoft.CodeAnalysis;

public sealed class NamedTypeSymbol
{
	public NamedTypeSymbol(string name, ITypeSymbol symbol)
	{
		Name = name;
		Symbol = symbol;
	}
	public string Name { get; }
	public ITypeSymbol Symbol { get; }
}