namespace DatabaseUtil.Test
{
	using Microsoft.Data.Sqlite;
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.Common;
	using System.Data.SqlClient;
	using System.Threading.Tasks;
	public static class Tests
	{
		private static IEnumerable<long> OfInt64_1(DbDataReader reader)
		{
			while (reader.Read())
			{
				yield return reader.GetInt64(0);
			}
			reader.ReadAllResults();
		}
		private static IEnumerable<long> OfInt64_2(IDataReader reader)
		{
			while (reader.Read())
			{
				yield return reader.GetInt64(0);
			}
			reader.ReadAllResults();
		}
		private static async IAsyncEnumerable<long> OfInt64Async(DbDataReader reader)
		{
			while (await reader.ReadAsync())
			{
				yield return reader.GetInt64(0);
			}
			reader.ReadAllResults();
		}
		private static void AssertDbParamEqual(string name, object value, ParameterDirection parameterDirection, byte precision, byte scale, DbParameter parameter)
		{
			Assert.Equal(name, parameter.ParameterName);
			Assert.Equal(value, parameter.Value);
			Assert.Equal(parameterDirection, parameter.Direction);
			Assert.Equal(precision, parameter.Precision);
			Assert.Equal(scale, parameter.Scale);
		}
		[Fact]
		public static void DbParams()
		{
			using SqlConnection cn = new();
			using (SqlCommand cmd = cn.CreateCommand())
			{
				var p = cmd.Params();
				p.Add("Name1", 1);
				p.Add("Name2", "Two", ParameterDirection.Output);
				p.Add("Name3", 3.30m, 3, 5);
				p.Add("Name4", 5.12345m, ParameterDirection.ReturnValue, 5, 5);
				Assert.Equal(4, cmd.Parameters.Count);

				AssertDbParamEqual("Name1", 1, ParameterDirection.Input, 0, 0, cmd.Parameters[0]);
				AssertDbParamEqual("Name2", "Two", ParameterDirection.Output, 0, 0, cmd.Parameters[1]);
				AssertDbParamEqual("Name3", 3.30m, ParameterDirection.Input, 3, 5, cmd.Parameters[2]);
				AssertDbParamEqual("Name4", 5.12345m, ParameterDirection.ReturnValue, 5, 5, cmd.Parameters[3]);
			}
			using (SqlCommand cmd = cn.CreateCommand())
			{
				var p = ((IDbCommand)cmd).Params();
				p.Add("Name1", 1);
				p.Add("Name2", "Two", ParameterDirection.Output);
				p.Add("Name3", 3.30m, 3, 5);
				p.Add("Name4", 5.12345m, ParameterDirection.ReturnValue, 5, 5);
				Assert.Equal(4, cmd.Parameters.Count);

				AssertDbParamEqual("Name1", 1, ParameterDirection.Input, 0, 0, cmd.Parameters[0]);
				AssertDbParamEqual("Name2", "Two", ParameterDirection.Output, 0, 0, cmd.Parameters[1]);
				AssertDbParamEqual("Name3", 3.30m, ParameterDirection.Input, 3, 5, cmd.Parameters[2]);
				AssertDbParamEqual("Name4", 5.12345m, ParameterDirection.ReturnValue, 5, 5, cmd.Parameters[3]);
			}

			using (SqlCommand cmd = cn.CreateCommand())
			{
				var p = cmd.Params();
				p.Add("Name1", 1, out var p1);
				p.Add("Name2", "Two", ParameterDirection.Output, out var p2);
				p.Add("Name3", 3.30m, 3, 5, out var p3);
				p.Add("Name4", 5.12345m, ParameterDirection.ReturnValue, 5, 5, out var p4);
				Assert.Equal(4, cmd.Parameters.Count);
				Assert.Same(p1, cmd.Parameters[0]);
				Assert.Same(p2, cmd.Parameters[1]);
				Assert.Same(p3, cmd.Parameters[2]);
				Assert.Same(p4, cmd.Parameters[3]);

				AssertDbParamEqual("Name1", 1, ParameterDirection.Input, 0, 0, cmd.Parameters[0]);
				AssertDbParamEqual("Name2", "Two", ParameterDirection.Output, 0, 0, cmd.Parameters[1]);
				AssertDbParamEqual("Name3", 3.30m, ParameterDirection.Input, 3, 5, cmd.Parameters[2]);
				AssertDbParamEqual("Name4", 5.12345m, ParameterDirection.ReturnValue, 5, 5, cmd.Parameters[3]);
			}
			using (SqlCommand cmd = cn.CreateCommand())
			{
				var p = ((IDbCommand)cmd).Params();
				p.Add("Name1", 1, out var p1);
				p.Add("Name2", "Two", ParameterDirection.Output, out var p2);
				p.Add("Name3", 3.30m, 3, 5, out var p3);
				p.Add("Name4", 5.12345m, ParameterDirection.ReturnValue, 5, 5, out var p4);
				Assert.Equal(4, cmd.Parameters.Count);
				Assert.Same(p1, cmd.Parameters[0]);
				Assert.Same(p2, cmd.Parameters[1]);
				Assert.Same(p3, cmd.Parameters[2]);
				Assert.Same(p4, cmd.Parameters[3]);

				AssertDbParamEqual("Name1", 1, ParameterDirection.Input, 0, 0, cmd.Parameters[0]);
				AssertDbParamEqual("Name2", "Two", ParameterDirection.Output, 0, 0, cmd.Parameters[1]);
				AssertDbParamEqual("Name3", 3.30m, ParameterDirection.Input, 3, 5, cmd.Parameters[2]);
				AssertDbParamEqual("Name4", 5.12345m, ParameterDirection.ReturnValue, 5, 5, cmd.Parameters[3]);
			}
		}
		[Fact]
		public static void GetCommand()
		{
			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();
			cn.ExecuteNonQuery("create table Tbl(Number int not null);");
			using (var cmd1 = ((IDbConnection)cn).GetCommand("insert into Tbl(Number)values(5);", commandTimeout: 25))
			{
				Assert.Equal("insert into Tbl(Number)values(5);", cmd1.CommandText);
				Assert.Equal(CommandType.Text, cmd1.CommandType);
				Assert.Null(cmd1.Transaction);
				Assert.Equal(25, cmd1.CommandTimeout);
				cmd1.ExecuteNonQuery();
			}
			using (var cmd2 = cn.GetCommand("select Number from Tbl;", commandTimeout: 10))
			{
				Assert.Equal("select Number from Tbl;", cmd2.CommandText);
				Assert.Equal(CommandType.Text, cmd2.CommandType);
				Assert.Null(cmd2.Transaction);
				Assert.Equal(10, cmd2.CommandTimeout);
				var r = cmd2.ExecuteReader();
				Assert.True(r.Read());
				Assert.Equal(5, r.GetInt64(0));
				Assert.False(r.Read());
			}
		}
		[Fact]
		public static void GetCommandParams()
		{
			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();
			cn.ExecuteNonQuery("create table Tbl(Number int not null);");
			using (var cmd1 = ((IDbConnection)cn).GetCommand("insert into Tbl(Number)values(@Value);", new TestParams(5), TestParamsApplicator.Instance, commandTimeout: 25))
			{
				Assert.Equal("insert into Tbl(Number)values(@Value);", cmd1.CommandText);
				Assert.Equal(CommandType.Text, cmd1.CommandType);
				var p = (IDbDataParameter?)Assert.Single(cmd1.Parameters);
				Assert.NotNull(p);
				Assert.Equal(nameof(TestParams.Value), p.ParameterName);
				Assert.Equal(5L, p.Value);
				Assert.Null(cmd1.Transaction);
				Assert.Equal(25, cmd1.CommandTimeout);
				cmd1.ExecuteNonQuery();
			}
			using (var cmd2 = cn.GetCommand("insert into Tbl(Number)values(@Value);", new TestParams(10), TestParamsApplicator.Instance, commandTimeout: 25))
			{
				Assert.Equal("insert into Tbl(Number)values(@Value);", cmd2.CommandText);
				Assert.Equal(CommandType.Text, cmd2.CommandType);
				var p = (IDbDataParameter?)Assert.Single(cmd2.Parameters);
				Assert.NotNull(p);
				Assert.Equal(nameof(TestParams.Value), p.ParameterName);
				Assert.Equal(10L, p.Value);
				Assert.Null(cmd2.Transaction);
				Assert.Equal(25, cmd2.CommandTimeout);
				cmd2.ExecuteNonQuery();
			}
			using (var cmd3 = cn.GetCommand("select Number from Tbl;", commandTimeout: 10))
			{
				Assert.Equal("select Number from Tbl;", cmd3.CommandText);
				Assert.Equal(CommandType.Text, cmd3.CommandType);
				Assert.Null(cmd3.Transaction);
				Assert.Equal(10, cmd3.CommandTimeout);
				var r = cmd3.ExecuteReader();
				Assert.True(r.Read());
				Assert.Equal(5, r.GetInt64(0));
				Assert.True(r.Read());
				Assert.Equal(10, r.GetInt64(0));
				Assert.False(r.Read());
			}
		}
		[Fact]
		public static async Task ExecuteNonQuery()
		{
			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();
			cn.ExecuteNonQuery("create table Tbl(Number int not null);");
			Assert.Equal(1, await cn.ExecuteNonQueryAsync("insert into Tbl(Number)values(5);"));
			Assert.Equal(5L, cn.ExecuteScalar("select Number from Tbl;"));
			Assert.Equal(1, ((IDbConnection)cn).ExecuteNonQuery("delete from Tbl;"));
		}
		[Fact]
		public static async Task ExecuteNonQueryParams()
		{
			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();
			cn.ExecuteNonQuery("create table Tbl(Number int not null);");

			Assert.Equal(1, cn.ExecuteNonQuery("insert into Tbl(Number)values(@Value);", new TestParams(1), TestParamsApplicator.Instance));
			Assert.Equal(1, await cn.ExecuteNonQueryAsync("insert into Tbl(Number)values(@Value);", new TestParams(2), TestParamsApplicator.Instance));
			Assert.Equal(1, ((IDbConnection)cn).ExecuteNonQuery("insert into Tbl(Number)values(@Value);", new TestParams(3), TestParamsApplicator.Instance));
			Assert.Collection(cn.Execute("select Number from Tbl order by Number;", OfInt64_1),
				x => Assert.Equal(1L, x),
				x => Assert.Equal(2L, x),
				x => Assert.Equal(3L, x));
		}
		[Fact]
		public static async Task ExecuteScalar()
		{
			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();
			cn.ExecuteNonQuery("create table Tbl(Number int not null);");
			cn.ExecuteNonQuery("insert into Tbl(Number)values(5);");
			Assert.Equal(5L, cn.ExecuteScalar("select Number from Tbl;"));
			Assert.Equal(5L, await cn.ExecuteScalarAsync("select Number from Tbl;"));
			Assert.Equal(5L, ((IDbConnection)cn).ExecuteScalar("select Number from Tbl;"));
		}
		[Fact]
		public static async Task ExecuteScalarParams()
		{
			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();
			cn.ExecuteNonQuery("create table Tbl(Number int not null);");
			cn.ExecuteNonQuery("insert into Tbl(Number)values(5);");
			Assert.Equal(5L, cn.ExecuteScalar("select Number from Tbl where Number = @Value;", new TestParams(5), TestParamsApplicator.Instance));
			Assert.Equal(5L, await cn.ExecuteScalarAsync("select Number from Tbl where Number = @Value;", new TestParams(5), TestParamsApplicator.Instance));
			Assert.Equal(5L, ((IDbConnection)cn).ExecuteScalar("select Number from Tbl where Number = @Value;", new TestParams(5), TestParamsApplicator.Instance));
		}
		[Fact]
		public static async Task Execute()
		{
			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();
			cn.ExecuteNonQuery("create table Tbl(Number int not null, Words text not null, Decimal real not null);");
			cn.ExecuteNonQuery("insert into Tbl(Number,Words,Decimal)values(1, 'Foo', 1.23), (5, 'Bar', 3.21), (354, 'Baz', 5.92);");

			long[] expected_a = [1, 5, 354];
			string[] expected_b = ["Foo", "Bar", "Baz"];
			double[] expected_c = [1.23, 3.21, 5.92];

			int i = 0;
			foreach (var (a, b, c) in cn.Execute("select * from Tbl;", Read1))
			{
				Assert.Equal(expected_a[i], a);
				Assert.Equal(expected_b[i], b);
				Assert.Equal(expected_c[i++], c);
			}
			i = 0;
			foreach (var (a, b, c) in ((IDbConnection)cn).Execute("select * from Tbl;", Read2))
			{
				Assert.Equal(expected_a[i], a);
				Assert.Equal(expected_b[i], b);
				Assert.Equal(expected_c[i++], c);
			}
			i = 0;
			await foreach (var (a, b, c) in cn.ExecuteAsync("select * from Tbl;", Read3))
			{
				Assert.Equal(expected_a[i], a);
				Assert.Equal(expected_b[i], b);
				Assert.Equal(expected_c[i++], c);
			}

			static IEnumerable<(long a, string b, double c)> Read1(DbDataReader reader)
			{
				while (reader.Read()) { yield return (reader.GetInt64(0), reader.GetString(1), reader.GetDouble(2)); }
				reader.ReadAllResults();
			}
			static IEnumerable<(long a, string b, double c)> Read2(IDataReader reader)
			{
				while (reader.Read()) { yield return (reader.GetInt64(0), reader.GetString(1), reader.GetDouble(2)); }
				reader.ReadAllResults();
			}
			static async IAsyncEnumerable<(long a, string b, double c)> Read3(DbDataReader reader)
			{
				while (await reader.ReadAsync()) { yield return (reader.GetInt64(0), reader.GetString(1), reader.GetDouble(2)); }
				await reader.ReadAllResultsAsync();
			}
		}
		[Fact]
		public static void ExecuteParams()
		{
			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();
			cn.ExecuteNonQuery("create table Tbl(Number int not null);");
			cn.ExecuteNonQuery("insert into Tbl(Number)values(1),(2),(3);");

			Assert.Collection(cn.Execute("select Number from Tbl where Number < @Value order by Number;", new TestParams(3), TestParamsApplicator.Instance, OfInt64_1),
				x => Assert.Equal(1, x), x => Assert.Equal(2, x));

			Assert.Collection(((IDbConnection)cn).Execute("select Number from Tbl where Number > @Value order by Number;", new TestParams(1), TestParamsApplicator.Instance, OfInt64_2),
				x => Assert.Equal(2, x), x => Assert.Equal(3, x));

			Assert.Collection(cn.ExecuteAsync("select Number from Tbl where Number <> @Value order by Number;", new TestParams(2), TestParamsApplicator.Instance, OfInt64Async),
				x => Assert.Equal(1, x), x => Assert.Equal(3, x));
		}
		[Fact]
		public static void ExtensionsNull()
		{
			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();

			using var cmd = cn.GetCommand("select null, null, null, null, null, null, null, null, null, null, null, null, null");

			var r = cmd.ExecuteReader();
			Assert.True(r.Read());

			int i = 0;
			Assert.Null(r.GetBooleanOrNull(i++));
			Assert.Null(r.GetByteOrNull(i++));
			Assert.Null(r.GetInt16OrNull(i++));
			Assert.Null(r.GetInt32OrNull(i++));
			Assert.Null(r.GetInt64OrNull(i++));
			Assert.Null(r.GetFloatOrNull(i++));
			Assert.Null(r.GetDoubleOrNull(i++));
			Assert.Null(r.GetDecimalOrNull(i++));
			Assert.Null(r.GetCharOrNull(i++));
			Assert.Null(r.GetStringOrNull(i++));
			Assert.Null(r.GetDateTimeOrNull(i++, DateTimeKind.Local));
			Assert.Null(r.GetGuidOrNull(i++));
			Assert.Null(r.GetValueOrNull(i++));
		}
		[Fact]
		public static void ExtensionsNotNull()
		{
			using SqliteConnection cn = new("Data Source=:memory:");
			cn.Open();

			using var cmd = cn.GetCommand("select 1, 2, 3, 4, 5, 6.5, 7.8, 10.123, 'a', 'qwerty', '2020-01-02 03:04:05', '2000-10-20 12:40:50', '40625186-0537-470A-8E67-BDA8DCC86B12', 50");

			var r = cmd.ExecuteReader();
			Assert.True(r.Read());

			int i = 0;
			Assert.Equal(true, r.GetBooleanOrNull(i++));
			Assert.Equal((byte)2, r.GetByteOrNull(i++));
			Assert.Equal((short)3, r.GetInt16OrNull(i++));
			Assert.Equal(4, r.GetInt32OrNull(i++));
			Assert.Equal(5L, r.GetInt64OrNull(i++));
			Assert.Equal(6.5f, r.GetFloatOrNull(i++));
			Assert.Equal(7.8d, r.GetDoubleOrNull(i++));
			Assert.Equal(10.123m, r.GetDecimalOrNull(i++));
			Assert.Equal('a', r.GetCharOrNull(i++));
			Assert.Equal("qwerty", r.GetStringOrNull(i++));
			Assert.Equal(new DateTime(2020, 1, 2, 3, 4, 5, DateTimeKind.Local), r.GetDateTime(i++, DateTimeKind.Local));
			Assert.Equal(new DateTime(2000, 10, 20, 12, 40, 50, DateTimeKind.Local), r.GetDateTimeOrNull(i++, DateTimeKind.Local));
			Assert.Equal(new Guid(0x40625186, 0x537, 0x470a, 0x8e, 0x67, 0xbd, 0xa8, 0xdc, 0xc8, 0x6b, 0x12), r.GetGuidOrNull(i++));
			Assert.Equal(50L, r.GetValueOrNull(i++));
		}
	}
}