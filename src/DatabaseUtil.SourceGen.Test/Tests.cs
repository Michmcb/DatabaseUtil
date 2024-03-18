namespace DatabaseUtil.SourceGen.Test
{
	using Microsoft.Data.Sqlite;
	using System;
	using System.Collections.Generic;
	using System.Linq;

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

	[DbRecordReader(DbDataReaderPref.IDataReader)]
	public sealed partial class DbReader { }
	public enum ByteEnum : byte { }
	public enum IntEnum : int { }
	public enum ShortEnum : short { }
	public enum LongEnum : long { }
	[DbRecord]
	public sealed class TestRecord
	{
		public TestRecord([HasName("MyNumber")] long number)
		{
			Number = number;
		}
		public long Number { get; }
	}
	[DbRecord]
	public readonly struct TestRecord2
	{
		public TestRecord2([HasName("MyNumber")] long number)
		{
			Number = number;
		}
		public long Number { get; }
	}
	[DbRecord(ReadBy.Ordinal)]
	public sealed record class Blah
	(
		[HasOrdinal(2)] bool Boolean,
		byte Byte,
		short Short,
		int Int,
		long Long,
		float Float,
		double Double,
		decimal Decimal,
		char Char,
		string String,
		DateTime DateTime,
		Guid Guid,
		object Object,
		ByteEnum ByteEnum,
		IntEnum IntEnum,
		ShortEnum ShortEnum,
		LongEnum LongEnum,

		bool? NullableBoolean,
		byte? NullableByte,
		short? NullableShort,
		int? NullableInt,
		long? NullableLong,
		float? NullableFloat,
		double? NullableDouble,
		decimal? NullableDecimal,
		char? NullableChar,
		string? NullableString,
		DateTime? NullableDateTime,
		Guid? NullableGuid,
		object? NullableObject,
		ByteEnum? NullableByteEnum,
		IntEnum? NullableIntEnum,
		ShortEnum? NullableShortEnum,
		LongEnum? NullableLongEnum
	);
	public static class Tests
	{
		[Fact]
		public static void Test1()
		{
			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();
			using (var cmd1 = cn.GetCommand("create table Tbl(MyNumber int not null);insert into Tbl(MyNumber)values(1),(2),(3);"))
			{
				cmd1.ExecuteNonQuery();
			}
			DbReader dbReader = new();
			using (var cmd2 = cn.GetCommand("select MyNumber from Tbl order by MyNumber asc;"))
			{
				using var reader = cmd2.ExecuteReader();
				List<TestRecord> results = dbReader.ReadAllTestRecord(reader).ToList();
				Assert.Collection(results,
					x => Assert.Equal(x.Number, 1),
					x => Assert.Equal(2, x.Number),
					x => Assert.Equal(3, x.Number));
			}
			using (var cmd2 = cn.GetCommand("select MyNumber from Tbl order by MyNumber asc;"))
			{
				using var reader = cmd2.ExecuteReader();
				TestRecord? result = dbReader.ReadFirstOrDefaultTestRecord(reader);
				Assert.NotNull(result);
				Assert.Equal(1, result.Number);
			}
		}
		[Fact]
		public static void Test2()
		{
			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();
			using (var cmd1 = cn.GetCommand("create table Tbl(MyNumber int not null);insert into Tbl(MyNumber)values(1),(2),(3);"))
			{
				cmd1.ExecuteNonQuery();
			}
			DbReader dbReader = new();
			using (var cmd2 = cn.GetCommand("select MyNumber from Tbl order by MyNumber asc;"))
			{
				using var reader = cmd2.ExecuteReader();
				var results = dbReader.ReadAllTestRecord2(reader).ToList();
				Assert.Collection(results,
					x => Assert.Equal(1, x.Number),
					x => Assert.Equal(2, x.Number),
					x => Assert.Equal(3, x.Number));
			}
			using (var cmd2 = cn.GetCommand("select MyNumber from Tbl order by MyNumber asc;"))
			{
				using var reader = cmd2.ExecuteReader();
				TestRecord2? result = dbReader.ReadFirstOrDefaultTestRecord2(reader);
				Assert.NotNull(result);
				Assert.Equal(1, result.Value.Number);
			}
		}
	}
}