namespace DatabaseUtil.SourceGen.Test
{
	[DbParams]
	public sealed partial class ParametersTest
	{
		public ParametersTest(string p1, int p2)
		{
			P1 = p1;
			P2 = p2;
		}
		public string P1 { get; }
		public int P2 { get; }
	}
}