namespace DatabaseUtil.SourceGen;

using Microsoft.CodeAnalysis;

public sealed class ReadConverterMethod
{
	public ReadConverterMethod(string converterMethodName, string converterTypeName, string converterPropertyName, string dataReaderMethodName, ITypeSymbol fromType, ITypeSymbol toType)
	{
		ConverterMethodName = converterMethodName;
		ConverterTypeName = converterTypeName;
		ConverterPropertyName = converterPropertyName;
		DataReaderMethodName = dataReaderMethodName;
		FromType = fromType;
		ToType = toType;
	}
	public string ConverterMethodName { get; }
	public string ConverterTypeName { get; }
	public string ConverterPropertyName { get; }
	public string DataReaderMethodName { get; }
	public ITypeSymbol FromType { get; }
	public ITypeSymbol ToType { get; }
}