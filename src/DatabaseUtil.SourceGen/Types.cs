namespace DatabaseUtil.SourceGen;

public sealed class Types
{
	public Types(string getOrdinals, string readAll, string readAllAsync, string readFirstOrDefault, string readFirstOrDefaultAsync, string read)
	{
		GetOrdinals = getOrdinals;
		ReadAll = readAll;
		ReadAllAsync = readAllAsync;
		ReadFirstOrDefault = readFirstOrDefault;
		ReadFirstOrDefaultAsync = readFirstOrDefaultAsync;
		Read = read;
	}
	public string GetOrdinals { get; }
	public string ReadAll { get; }
	public string ReadAllAsync { get; }
	public string ReadFirstOrDefault { get; }
	public string ReadFirstOrDefaultAsync { get; }
	public string Read { get; }
}
