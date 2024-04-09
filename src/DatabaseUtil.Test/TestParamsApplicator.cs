namespace DatabaseUtil.Test
{
	using System.Data;

	public sealed class TestParamsApplicator : IDbParamsApplicator<TestParams>
	{
		public static readonly TestParamsApplicator Instance = new();
		public void ApplyParameters(TestParams p, IDbCommand cmd)
		{
			cmd.AddParameter(nameof(p.Value), p.Value);
		}
	}
}