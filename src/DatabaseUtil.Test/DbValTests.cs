namespace DatabaseUtil.Test
{
	using System;

	public static class DbValTests
	{
		[Fact]
		public static void Convert()
		{
			DateTime dt = new(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			DbVal<long> longVal = new(dt.Ticks);
			DbVal<DateTime> dtVal = longVal.Convert(new SqlUtcDateTimeConverter());
			Assert.Equal(longVal.Errors, dtVal.Errors);
			Assert.True(dtVal.TryGet(out DateTime convertedDt));
			Assert.Equal(dt, convertedDt);
		}
		[Fact]
		public static void HasAValue_Struct()
		{
			DbVal<int> d = new(1);
			Assert.True(d.Ok);
			Assert.False(d.Null);
			Assert.False(d.NoRow);
			Assert.False(d.WrongType);
			Assert.True(d.TryGet(out int value1));
			Assert.Equal(1, value1);
			Assert.True(d.TryGetOrNull(out int? value2));
			Assert.Equal(1, value2);
			Assert.Equal(1, d.ValueOr(0));
		}
		[Fact]
		public static void HasAValue_Class()
		{
			DbVal<string> d = new("Foo");
			Assert.True(d.Ok);
			Assert.False(d.Null);
			Assert.False(d.NoRow);
			Assert.False(d.WrongType);
			Assert.True(d.TryGet(out string value1));
			Assert.Equal("Foo", value1);
			Assert.True(d.TryGetOrNull(out string? value2));
			Assert.Equal("Foo", value2);
			Assert.Equal("Foo", d.ValueOr(""));
		}
		[Fact]
		public static void IsNull_Struct()
		{
			DbVal<int> d = DbVal<int>.From(DBNull.Value);
			Assert.False(d.Ok);
			Assert.True(d.Null);
			Assert.False(d.NoRow);
			Assert.False(d.WrongType);
			Assert.False(d.TryGet(out int value1));
			Assert.Equal(default, value1);
			Assert.True(d.TryGetOrNull(out int? value2));
			Assert.Null(value2);
			Assert.Equal(50, d.ValueOr(50));
		}
		[Fact]
		public static void IsNull_Class()
		{
			DbVal<string> d = DbVal<string>.From(DBNull.Value);
			Assert.False(d.Ok);
			Assert.True(d.Null);
			Assert.False(d.NoRow);
			Assert.False(d.WrongType);
			Assert.False(d.TryGet(out string? value1));
			Assert.Equal(default, value1);
			Assert.True(d.TryGetOrNull(out string? value2));
			Assert.Null(value2);
			Assert.Equal("Foo", d.ValueOr("Foo"));
		}
		[Fact]
		public static void NoRow_Struct()
		{
			DbVal<int> d = DbVal<int>.From(null);
			Assert.False(d.Ok);
			Assert.False(d.Null);
			Assert.True(d.NoRow);
			Assert.False(d.WrongType);
			Assert.False(d.TryGet(out int value1));
			Assert.Equal(default, value1);
			Assert.False(d.TryGetOrNull(out int? value2));
			Assert.Null(value2);
			Assert.Equal(50, d.ValueOr(50));
		}
		[Fact]
		public static void NoRow_Class()
		{
			DbVal<string> d = DbVal<string>.From(null);
			Assert.False(d.Ok);
			Assert.False(d.Null);
			Assert.True(d.NoRow);
			Assert.False(d.WrongType);
			Assert.False(d.TryGet(out string? value1));
			Assert.Equal(default, value1);
			Assert.False(d.TryGetOrNull(out string? value2));
			Assert.Null(value2);
			Assert.Equal("Foo", d.ValueOr("Foo"));
		}
		[Fact]
		public static void WrongType_Struct()
		{
			DbVal<int> d = DbVal<int>.From(5.20);
			Assert.False(d.Ok);
			Assert.False(d.Null);
			Assert.False(d.NoRow);
			Assert.True(d.WrongType);
			Assert.False(d.TryGet(out int value1));
			Assert.Equal(default, value1);
			Assert.False(d.TryGetOrNull(out int? value2));
			Assert.Null(value2);
			Assert.Equal(50, d.ValueOr(50));
		}
		[Fact]
		public static void WrongType_Class()
		{
			DbVal<string> d = DbVal<string>.From(5.20);
			Assert.False(d.Ok);
			Assert.False(d.Null);
			Assert.False(d.NoRow);
			Assert.True(d.WrongType);
			Assert.False(d.TryGet(out string? value1));
			Assert.Equal(default, value1);
			Assert.False(d.TryGetOrNull(out string? value2));
			Assert.Null(value2);
			Assert.Equal("Foo", d.ValueOr("Foo"));
		}
	}
}