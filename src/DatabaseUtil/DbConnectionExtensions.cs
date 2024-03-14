namespace DatabaseUtil;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

/// <summary>
/// Extension methods for <see cref="IDbConnection"/> and <see cref="DbConnection"/>.
/// </summary>
public static class DbConnectionExtensions
{
	/// <summary>
	/// Creates a command using the provided parameters.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static DbCommand GetCommand(this DbConnection connection, string sql, IDbParams? parameters = null, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		parameters?.ApplyTo(cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd;
	}
	/// <summary>
	/// Creates a command using the provided parameters.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static IDbCommand GetCommand(this IDbConnection connection, string sql, IDbParams? parameters = null, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		parameters?.ApplyTo(cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd;
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the number of rows affected.
	/// Takes care of disposing of the created <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static int ExecuteNonQuery(this DbConnection connection, string sql, IDbParams? parameters = null, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		parameters?.ApplyTo(cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd.ExecuteNonQuery();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the number of rows affected.
	/// Takes care of disposing of the created <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static async Task<int> ExecuteNonQueryAsync(this DbConnection connection, string sql, IDbParams? parameters = null, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		parameters?.ApplyTo(cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return await cmd.ExecuteNonQueryAsync();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static object? ExecuteScalar(this DbConnection connection, string sql, IDbParams? parameters = null, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		parameters?.ApplyTo(cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd.ExecuteScalar();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static async Task<object?> ExecuteScalarAsync(this DbConnection connection, string sql, IDbParams? parameters = null, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		parameters?.ApplyTo(cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return await cmd.ExecuteScalarAsync();
	}
	/// <summary>
	/// Executes a command using the provided parameters, invokes <paramref name="getData"/> to get an enumerable of objects of type <typeparamref name="T"/>, and returns the enumerable.
	/// Takes care of disposing of the created <see cref="DbCommand"/> and <see cref="DbDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="getData">The function that reads data from the <see cref="DbDataReader"/> and returns an <see cref="IEnumerable{T}"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static IEnumerable<T> Execute<T>(this DbConnection connection, string sql, Func<DbDataReader, IEnumerable<T>> getData, IDbParams? parameters = null, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		parameters?.ApplyTo(cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using DbDataReader reader = cmd.ExecuteReader();
		foreach (var d in getData(reader))
		{
			yield return d;
		}
		reader.ReadAllResults();
	}
	/// <summary>
	/// Executes a command using the provided parameters, invokes <paramref name="getData"/> to get an enumerable of objects of type <typeparamref name="T"/>, and returns the enumerable.
	/// Takes care of disposing of the created <see cref="DbCommand"/> and <see cref="DbDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="getData">The function that reads data from the <see cref="DbDataReader"/> and returns an <see cref="IEnumerable{T}"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static async IAsyncEnumerable<T> ExecuteAsync<T>(this DbConnection connection, string sql, Func<DbDataReader, IAsyncEnumerable<T>> getData, IDbParams? parameters = null, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		parameters?.ApplyTo(cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using DbDataReader reader = cmd.ExecuteReader();
		await foreach (var d in getData(reader))
		{
			yield return d;
		}
		await reader.ReadAllResultsAsync();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the number of rows affected.
	/// Takes care of disposing of the created <see cref="IDbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static int ExecuteNonQuery(this IDbConnection connection, string sql, IDbParams? parameters = null, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		parameters?.ApplyTo(cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd.ExecuteNonQuery();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="IDbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static object? ExecuteScalar(this IDbConnection connection, string sql, IDbParams? parameters = null, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		parameters?.ApplyTo(cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd.ExecuteScalar();
	}
	/// <summary>
	/// Executes a command using the provided parameters, invokes <paramref name="getData"/> to get an enumerable of objects of type <typeparamref name="T"/>, and returns the enumerable.
	/// Takes care of disposing of the created <see cref="IDbCommand"/> and <see cref="IDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="getData">The function that reads data from the <see cref="IDataReader"/> and returns an <see cref="IEnumerable{T}"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static IEnumerable<T> Execute<T>(this IDbConnection connection, string sql, Func<IDataReader, IEnumerable<T>> getData, IDbParams? parameters = null, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		parameters?.ApplyTo(cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using IDataReader reader = cmd.ExecuteReader();
		foreach (var d in getData(reader))
		{
			yield return d;
		}
		reader.ReadAllResults();
	}
}
