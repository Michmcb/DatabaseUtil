namespace DatabaseUtil.SourceGen.Test
{
	using Microsoft.Data.Sqlite;
	using System;
	using System.Collections.Generic;
	using System.Linq;
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
				List<TestClass> results = dbReader.ReadAllTestClass(reader).ToList();
				Assert.Collection(results,
					x => Assert.Equal(1, x.Number),
					x => Assert.Equal(2, x.Number),
					x => Assert.Equal(3, x.Number));
			}
			using (var cmd2 = cn.GetCommand("select MyNumber from Tbl order by MyNumber asc;"))
			{
				using var reader = cmd2.ExecuteReader();
				TestClass? result = dbReader.ReadFirstOrDefaultTestClass(reader);
				Assert.NotNull(result);
				Assert.Equal(1, result.Number);
			}
		}
		[Fact]
		public static void Test2()
		{
			Assert.Equal(0, new DateOnly(1, 1, 1).DayNumber);

			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();
			using (var cmd1 = cn.GetCommand("create table Tbl(MyDate int not null);insert into Tbl(MyDate)values(1),(2),(3);"))
			{
				cmd1.ExecuteNonQuery();
			}
			DbReader dbReader = new();
			using (var cmd2 = cn.GetCommand("select MyDate from Tbl order by MyDate asc;"))
			{
				using var reader = cmd2.ExecuteReader();
				List<TestStruct> results = dbReader.ReadAllTestStruct(reader).ToList();
				Assert.Collection(results,
					x => Assert.Equal(new DateOnly(1, 1, 2), x.Date),
					x => Assert.Equal(new DateOnly(1, 1, 3), x.Date),
					x => Assert.Equal(new DateOnly(1, 1, 4), x.Date));
			}
			using (var cmd2 = cn.GetCommand("select MyDate from Tbl order by MyDate asc;"))
			{
				using var reader = cmd2.ExecuteReader();
				TestStruct? result = dbReader.ReadFirstOrDefaultTestStruct(reader);
				Assert.NotNull(result);
				Assert.Equal(new DateOnly(1, 1, 2), result.Value.Date);
			}
		}
		[Fact]
		public static void TestParameters()
		{
			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();
			using (var cmd1 = cn.GetCommand("create table Tbl(Integer int not null, Date int null);"))
			{
				cmd1.ExecuteNonQuery();
			}
			DbReader dbReader = new();
			using (var cmd2 = cn.GetCommand("insert into Tbl(Integer,Date)values(@Integer, @TheDate);", parameters: new TestRecordClassParams(10, new DateOnly(2000, 1, 2)), dbReader))
			{
				Assert.Equal(1, cmd2.ExecuteNonQuery());
			}
			using (var cmd3 = cn.GetCommand("select Integer,Date as TheDate from Tbl;"))
			{
				using var reader = cmd3.ExecuteReader();
				List<TestRecordClassParams> results = dbReader.ReadAllTestRecordClassParams(reader).ToList();
				TestRecordClassParams x = Assert.Single(results);
				Assert.Equal(10, x.Integer);
				Assert.Equal(new DateOnly(2000, 1, 2), x.Date);
			}
		}
	}
}