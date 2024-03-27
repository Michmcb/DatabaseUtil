namespace DatabaseUtil.SourceGen.Test
{
	[DbRecord]
	public sealed class TestClass
	{
		public TestClass([HasName("MyNumber")] long number)
		{
			Number = number;
		}
		public long Number { get; }
	}
}