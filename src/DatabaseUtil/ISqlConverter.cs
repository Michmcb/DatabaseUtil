namespace DatabaseUtil;

public interface ISqlConverter<TDb, TNet>
{
	TNet DbToDotNet(TDb dbValue);
	TDb DotNetToDb(TNet dotNetValue);
}
