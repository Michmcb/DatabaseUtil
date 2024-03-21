namespace DatabaseUtil.SourceGen;

using Microsoft.CodeAnalysis;

public sealed class ConverterMethod
{
	public ConverterMethod(string name, string converterTypeName, string converterPropertyName, ITypeSymbol fromType, ITypeSymbol toType)
	{
		Name = name;
		ConverterTypeName = converterTypeName;
		ConverterPropertyName = converterPropertyName;
		FromType = fromType;
		ToType = toType;
	}
	public string Name { get; }
	public string ConverterTypeName { get; }
	public string ConverterPropertyName { get; }
	public ITypeSymbol FromType { get; }
	public ITypeSymbol ToType { get; }
}