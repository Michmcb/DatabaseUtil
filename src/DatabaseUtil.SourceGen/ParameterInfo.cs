namespace DatabaseUtil.SourceGen;

using Microsoft.CodeAnalysis;

public sealed class ParameterInfo
{
	public ParameterInfo(string codeName, string? dbName, int index, ITypeSymbol type)
	{
		CodeName = codeName;
		DbName = dbName;
		Index = index;
		Type = type;
	}
	public string CodeName { get; }
	public string? DbName { get; }
	public int Index { get; }
	public ITypeSymbol Type { get; }
}
