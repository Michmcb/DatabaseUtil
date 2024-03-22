namespace DatabaseUtil.SourceGen;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public sealed class ParameterInfo
{
	public ParameterInfo(string codeName, string? dbName, int index, ITypeSymbol type, ParameterSyntax syntax)
	{
		CodeName = codeName;
		DbName = dbName;
		Index = index;
		Type = type;
		Syntax = syntax;
	}
	public string CodeName { get; }
	public string? DbName { get; }
	public int Index { get; }
	public ITypeSymbol Type { get; }
	public ParameterSyntax Syntax { get; }
}
