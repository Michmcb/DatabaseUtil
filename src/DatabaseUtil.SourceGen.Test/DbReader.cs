namespace DatabaseUtil.SourceGen.Test
{
	[DbRecordReader(DbDataReaderPref.IDataReader)]
	public sealed partial class DbReader
	{
		[DbConverter]
		public SqlConverter MyConverter { get; } = new();
	}
}