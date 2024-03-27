namespace DatabaseUtil.SourceGen.Test
{
	using System;

	[DbRecord]
	public readonly struct TestStruct
	{
		public TestStruct([HasName("MyDate")] DateOnly date)
		{
			Date = date;
		}
		public DateOnly Date { get; }
	}
}