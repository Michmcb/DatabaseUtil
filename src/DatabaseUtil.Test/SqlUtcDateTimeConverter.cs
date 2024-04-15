namespace DatabaseUtil.Test
{
	using System;

	public sealed class SqlUtcDateTimeConverter : ISqlConverter<long, DateTime>
	{
		public DateTime DbToDotNet(long dbValue)
		{
			return new DateTime(ticks: dbValue, DateTimeKind.Utc);
		}
		public long DotNetToDb(DateTime dotNetValue)
		{
			return dotNetValue.ToUniversalTime().Ticks;
		}
	}
}