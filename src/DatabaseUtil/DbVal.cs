namespace DatabaseUtil;

using System;
using System.Diagnostics.CodeAnalysis;
/// <summary>
/// A way of indicating a value from the database.
/// You can use <see cref="From(object?)"/> to create an instance of this type.
/// The default value for this struct is no value and an error of <see cref="DbValError.Null"/>.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public readonly struct DbVal<T> where T : notnull
{
	internal readonly T value;
	internal readonly DbValError errors;
	/// <summary>
	/// Creates a new instance, with <see cref="DbValError.Ok"/>.
	/// </summary>
	/// <param name="value">The value.</param>
	public DbVal(T value)
	{
		this.value = value;
		errors = DbValError.Ok;
	}
	/// <summary>
	/// Creates a new instance, with the default value for <typeparamref name="T"/>, and the provided <paramref name="errors"/>.
	/// </summary>
	/// <param name="errors">The errors.</param>
	public DbVal(DbValError errors)
	{
		value = default!;
		this.errors = errors;
	}
	/// <summary>
	/// Creates a new instance.
	/// </summary>
	/// <param name="value">The value. Only meaningful if <paramref name="errors"/> is <see cref="DbValError.Ok"/>.</param>
	/// <param name="errors">Thee errors.</param>
	public DbVal(T value, DbValError errors)
	{
		this.value = value;
		this.errors = errors;
	}
	/// <summary>
	/// True if there are no errors.
	/// </summary>
	public bool Ok => errors == DbValError.Ok;
	/// <summary>
	/// The errors, if any.
	/// </summary>
	public DbValError Errors => errors;
	/// <summary>
	/// <see langword="true"/> if there were no results, <see langword="false"/> otherwise.
	/// </summary>
	public bool NoRow => (errors & DbValError.NoRow) == DbValError.NoRow;
	/// <summary>
	/// <see langword="true"/> if the value was null, <see langword="false"/> otherwise.
	/// </summary>
	public bool Null => errors == DbValError.Null;
	/// <summary>
	/// <see langword="true"/> if the value was the wrong type, <see langword="false"/> otherwise.
	/// </summary>
	public bool WrongType => (errors & DbValError.WrongType) == DbValError.WrongType;
	/// <summary>
	/// Returns <see cref="Ok"/> and sets <paramref name="value"/> to the value returned from the database.
	/// </summary>
	/// <param name="value">The value. Only meaningful when this method returns <see langword="true"/>.</param>
	public bool TryGet(out T value)
	{
		value = this.value;
		return Ok;
	}
	/// <summary>
	/// Returns the value if one is present, otherwise returns <paramref name="ifNone"/>.
	/// </summary>
	/// <param name="ifNone">The default value.</param>
	/// <returns>The value or <paramref name="ifNone"/>.</returns>
	[return: NotNullIfNotNull(nameof(ifNone))]
	public T ValueOr(T ifNone)
	{
		return Ok ? value : ifNone;
	}
	/// <summary>
	/// Converts from the database value to the .net value, by invoking <see cref="ISqlConverter{TDb, TNet}.DbToDotNet(TDb)"/>.
	/// </summary>
	/// <returns>The converted .net value.</returns>
	public DbVal<TNet> Convert<TNet>(ISqlConverter<T, TNet> sqlConverter) where TNet : notnull
	{
		return new DbVal<TNet>(sqlConverter.DbToDotNet(value), errors);
	}
	/// <summary>
	/// Creates a new instance from <paramref name="value"/>, which should be a result from <see cref="System.Data.IDbCommand.ExecuteScalar"/>.
	/// </summary>
	/// <param name="value">The scalar value.</param>
	/// <returns>A <see cref="DbVal{T}"/> from <paramref name="value"/>.</returns>
	public static DbVal<T> From(object? value)
	{
		if (value is DBNull)
		{
			// Is null
			return default;
		}
		else if (value is null)
		{
			// null; no rows
			return new DbVal<T>(DbValError.NoRow);
		}
		else if (value is T v)
		{
			// Have a value, all good
			return new DbVal<T>(v);
		}
		else
		{
			// Apparently the wrong type
			return new DbVal<T>(DbValError.WrongType);
		}
	}
}