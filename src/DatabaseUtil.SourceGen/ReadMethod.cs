namespace DatabaseUtil.SourceGen;
public sealed class ReadMethod
{
	public ReadMethod(string name, bool builtIn, ConverterMethod? converter)
	{
		Name = name;
		BuiltIn = builtIn;
		Converter = converter;
	}
	public string Name { get; }
	public bool BuiltIn { get; }
	public ConverterMethod? Converter { get; }
}
