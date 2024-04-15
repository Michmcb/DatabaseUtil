namespace DatabaseUtil.SourceGen;

using Microsoft.CodeAnalysis;

public sealed class WriteConverterMethod
{
	public WriteConverterMethod(string converterMethodName, string converterTypeName, string converterPropertyName, ITypeSymbol fromType, ITypeSymbol toType)
	{
		ConverterMethodName = converterMethodName;
		ConverterTypeName = converterTypeName;
		ConverterPropertyName = converterPropertyName;
		FromType = fromType;
		ToType = toType;
	}
	public string ConverterMethodName { get; }
	public string ConverterTypeName { get; }
	public string ConverterPropertyName { get; }
	public ITypeSymbol FromType { get; }
	public ITypeSymbol ToType { get; }
}
