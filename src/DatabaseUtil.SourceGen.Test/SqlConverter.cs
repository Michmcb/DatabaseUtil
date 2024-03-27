namespace DatabaseUtil.SourceGen.Test
{
	using System;

	public sealed class SqlConverter : ISqlConverter<int, DateOnly>
	{
		public DateOnly DbToDotNet(int dbValue)
		{
			return DateOnly.FromDayNumber(dbValue);
		}
		public int DotNetToDb(DateOnly dotNetValue)
		{
			return dotNetValue.DayNumber;
		}
	}
}