namespace DatabaseUtil.SourceGen.Test
{
	using System;

	[DbParams]
	[DbRecord]
	public sealed record class TestRecordClassParams(int Integer, [HasName("TheDate")] DateOnly? Date);
}