namespace DatabaseUtil.SourceGen;
public sealed class ReadMethod
{
	public ReadMethod(string name, bool builtIn)
	{
		Name = name;
		BuiltIn = builtIn;
	}
	public string Name { get; }
	public bool BuiltIn { get; }
}
