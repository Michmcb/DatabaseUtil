namespace DatabaseUtil;

using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

/// <summary>
/// Extensions for <see cref="DbDataReader"/>, <see cref="IDataReader"/>, and <see cref="IDataRecord"/>.
/// </summary>
public static class DbDataReaderExtensions
{
	/// <summary>
	/// Invokes <see cref="IDataReader.NextResult"/> until it returns <see langword="false"/>, to exhaust the reader of results.
	/// </summary>
	public static void ReadAllResults(this IDataReader reader)
	{
		while (reader.NextResult()) ;
	}
	/// <summary>
	/// Invokes <see cref="DbDataReader.NextResult"/> until it returns <see langword="false"/>, to exhaust the reader of results.
	/// </summary>
	public static void ReadAllResults(this DbDataReader reader)
	{
		while (reader.NextResult()) ;
	}
	/// <summary>
	/// Invokes <see cref="DbDataReader.NextResult"/> until it returns <see langword="false"/>, to exhaust the reader of results.
	/// </summary>
	public static async Task ReadAllResultsAsync(this DbDataReader reader)
	{
		while (await reader.NextResultAsync()) ;
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="bool"/>, or returns <see langword="null"/> if
	/// <see cref="IDataRecord.IsDBNull(int)"/> returns <see langword="true"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static bool? GetBooleanOrNull(this IDataRecord dr, int i)
	{
		return dr.IsDBNull(i) ? null : dr.GetBoolean(i);
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="byte"/>, or returns <see langword="null"/> if
	/// <see cref="IDataRecord.IsDBNull(int)"/> returns <see langword="true"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static byte? GetByteOrNull(this IDataRecord dr, int i)
	{
		return dr.IsDBNull(i) ? null : dr.GetByte(i);
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="short"/>, or returns <see langword="null"/> if
	/// <see cref="IDataRecord.IsDBNull(int)"/> returns <see langword="true"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static short? GetInt16OrNull(this IDataRecord dr, int i)
	{
		return dr.IsDBNull(i) ? null : dr.GetInt16(i);
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="int"/>, or returns <see langword="null"/> if
	/// <see cref="IDataRecord.IsDBNull(int)"/> returns <see langword="true"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static int? GetInt32OrNull(this IDataRecord dr, int i)
	{
		return dr.IsDBNull(i) ? null : dr.GetInt32(i);
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="long"/>, or returns <see langword="null"/> if
	/// <see cref="IDataRecord.IsDBNull(int)"/> returns <see langword="true"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static long? GetInt64OrNull(this IDataRecord dr, int i)
	{
		return dr.IsDBNull(i) ? null : dr.GetInt64(i);
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="float"/>, or returns <see langword="null"/> if
	/// <see cref="IDataRecord.IsDBNull(int)"/> returns <see langword="true"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static float? GetFloatOrNull(this IDataRecord dr, int i)
	{
		return dr.IsDBNull(i) ? null : dr.GetFloat(i);
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="double"/>, or returns <see langword="null"/> if
	/// <see cref="IDataRecord.IsDBNull(int)"/> returns <see langword="true"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static double? GetDoubleOrNull(this IDataRecord dr, int i)
	{
		return dr.IsDBNull(i) ? null : dr.GetDouble(i);
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="decimal"/>, or returns <see langword="null"/> if
	/// <see cref="IDataRecord.IsDBNull(int)"/> returns <see langword="true"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static decimal? GetDecimalOrNull(this IDataRecord dr, int i)
	{
		return dr.IsDBNull(i) ? null : dr.GetDecimal(i);
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="char"/>, or returns <see langword="null"/> if
	/// <see cref="IDataRecord.IsDBNull(int)"/> returns <see langword="true"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static char? GetCharOrNull(this IDataRecord dr, int i)
	{
		return dr.IsDBNull(i) ? null : dr.GetChar(i);
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="string"/>, or returns <see langword="null"/> if
	/// <see cref="IDataRecord.IsDBNull(int)"/> returns <see langword="true"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static string? GetStringOrNull(this IDataRecord dr, int i)
	{
		return dr.IsDBNull(i) ? null : dr.GetString(i);
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="DateTime"/>, applying <paramref name="kind"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <param name="kind">The <see cref="DateTimeKind"/> to apply to the resultant <see cref="DateTime"/>.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static DateTime GetDateTime(this IDataRecord dr, int i, DateTimeKind kind)
	{
		return DateTime.SpecifyKind(dr.GetDateTime(i), kind);
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="DateTime"/>, applying <paramref name="kind"/>, or returns <see langword="null"/> if
	/// <see cref="IDataRecord.IsDBNull(int)"/> returns <see langword="true"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <param name="kind">The <see cref="DateTimeKind"/> to apply to the resultant <see cref="DateTime"/>.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static DateTime? GetDateTimeOrNull(this IDataRecord dr, int i, DateTimeKind kind)
	{
		return dr.IsDBNull(i) ? null : DateTime.SpecifyKind(dr.GetDateTime(i), kind);
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="Guid"/>, or returns <see langword="null"/> if
	/// <see cref="IDataRecord.IsDBNull(int)"/> returns <see langword="true"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static Guid? GetGuidOrNull(this IDataRecord dr, int i)
	{
		return dr.IsDBNull(i) ? null : dr.GetGuid(i);
	}
	/// <summary>
	/// Gets the value of the specified column as a <see cref="object"/>, or returns <see langword="null"/> if
	/// <see cref="IDataRecord.IsDBNull(int)"/> returns <see langword="true"/>.
	/// </summary>
	/// <param name="dr">The <see cref="IDataRecord"/> from which to read.</param>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <exception cref="IndexOutOfRangeException"/>
	/// <returns>The value of the column.</returns>
	public static object? GetValueOrNull(this IDataRecord dr, int i)
	{
		return dr.IsDBNull(i) ? null : dr.GetValue(i);
	}
}
