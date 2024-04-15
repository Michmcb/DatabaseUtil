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
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static IDbCommand GetCommand(this IDbConnection connection, string sql, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd;
	}
	/// <summary>
	/// Creates a command using the provided parameters.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static IDbCommand GetCommand<TParams>(this IDbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd;
	}
	/// <summary>
	/// Creates a command using the provided parameters.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static DbCommand GetCommand(this DbConnection connection, string sql, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd;
	}
	/// <summary>
	/// Creates a command using the provided parameters.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static DbCommand GetCommand<TParams>(this DbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd;
	}/// <summary>
	 /// Executes a command using the provided parameters, and returns the number of rows affected.
	 /// Takes care of disposing of the created <see cref="DbCommand"/>.
	 /// </summary>
	 /// <param name="connection">The connection from which to create the command.</param>
	 /// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	 /// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	 /// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	 /// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	 /// <returns>The created command.</returns>
	public static int ExecuteNonQuery(this DbConnection connection, string sql, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
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
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static int ExecuteNonQuery<TParams>(this DbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd.ExecuteNonQuery();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the number of rows affected.
	/// Takes care of disposing of the created <see cref="IDbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static int ExecuteNonQuery(this IDbConnection connection, string sql, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd.ExecuteNonQuery();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the number of rows affected.
	/// Takes care of disposing of the created <see cref="IDbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static int ExecuteNonQuery<TParams>(this IDbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd.ExecuteNonQuery();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the number of rows affected.
	/// Takes care of disposing of the created <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static async Task<int> ExecuteNonQueryAsync(this DbConnection connection, string sql, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return await cmd.ExecuteNonQueryAsync();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the number of rows affected.
	/// Takes care of disposing of the created <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static async Task<int> ExecuteNonQueryAsync<TParams>(this DbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return await cmd.ExecuteNonQueryAsync();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static object? ExecuteScalar(this DbConnection connection, string sql, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd.ExecuteScalar();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="IDbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static object? ExecuteScalar(this IDbConnection connection, string sql, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
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
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static object? ExecuteScalar<TParams>(this DbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd.ExecuteScalar();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="IDbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static object? ExecuteScalar<TParams>(this IDbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return cmd.ExecuteScalar();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static async Task<object?> ExecuteScalarAsync(this DbConnection connection, string sql, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return await cmd.ExecuteScalarAsync();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static async Task<object?> ExecuteScalarAsync<TParams>(this DbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return await cmd.ExecuteScalarAsync();
	}

	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static DbVal<TDatum> ExecuteScalarAs<TDatum>(this DbConnection connection, string sql, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text) where TDatum : notnull
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return DbVal<TDatum>.From(cmd.ExecuteScalar());
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="IDbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static DbVal<TDatum> ExecuteScalarAs<TDatum>(this IDbConnection connection, string sql, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text) where TDatum : notnull
	{
		using IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return DbVal<TDatum>.From(cmd.ExecuteScalar());
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static DbVal<TDatum> ExecuteScalarAs<TDatum, TParams>(this DbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text) where TDatum : notnull
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return DbVal<TDatum>.From(cmd.ExecuteScalar());
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="IDbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static DbVal<TDatum> ExecuteScalarAs<TDatum, TParams>(this IDbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text) where TDatum : notnull
	{
		using IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return DbVal<TDatum>.From(cmd.ExecuteScalar());
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static async Task<DbVal<TDatum>> ExecuteScalarAsAsync<TDatum>(this DbConnection connection, string sql, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text) where TDatum : notnull
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return DbVal<TDatum>.From(await cmd.ExecuteScalarAsync());
	}
	/// <summary>
	/// Executes a command using the provided parameters, and returns the value of the first column of the first row in the first result set.
	/// Takes care of disposing of the created <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static async Task<DbVal<TDatum>> ExecuteScalarAsAsync<TDatum, TParams>(this DbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text) where TDatum : notnull
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		return DbVal<TDatum>.From(await cmd.ExecuteScalarAsync());
	}
	/// <summary>
	/// Executes a command using the provided parameters, invokes <paramref name="getData"/> to get an enumerable of objects of type <typeparamref name="TDatum"/>, and returns the enumerable.
	/// Takes care of disposing of the created <see cref="DbCommand"/> and <see cref="DbDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="getData">The function that reads data from the <see cref="DbDataReader"/> and returns an <see cref="IEnumerable{T}"/>.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static IEnumerable<TDatum> Execute<TDatum>(this DbConnection connection, string sql, Func<DbDataReader, IEnumerable<TDatum>> getData, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using DbDataReader reader = cmd.ExecuteReader();
		foreach (var d in getData(reader))
		{
			yield return d;
		}
		reader.ReadAllResults();
	}
	/// <summary>
	/// Executes a command using the provided parameters, invokes <paramref name="getData"/> to get an enumerable of objects of type <typeparamref name="TDatum"/>, and returns the enumerable.
	/// Takes care of disposing of the created <see cref="DbCommand"/> and <see cref="DbDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="getData">The function that reads data from the <see cref="DbDataReader"/> and returns an <see cref="IEnumerable{T}"/>.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static IEnumerable<TDatum> Execute<TDatum, TParams>(this DbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, Func<DbDataReader, IEnumerable<TDatum>> getData, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using DbDataReader reader = cmd.ExecuteReader();
		foreach (var d in getData(reader))
		{
			yield return d;
		}
		reader.ReadAllResults();
	}
	/// <summary>
	/// Executes a command using the provided parameters, invokes <paramref name="getData"/> to get an enumerable of objects of type <typeparamref name="TDatum"/>, and returns the enumerable.
	/// Takes care of disposing of the created <see cref="DbCommand"/> and <see cref="DbDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="getData">The function that reads data from the <see cref="DbDataReader"/> and returns an <see cref="IAsyncEnumerable{T}"/>.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static async IAsyncEnumerable<TDatum> ExecuteAsync<TDatum>(this DbConnection connection, string sql, Func<DbDataReader, IAsyncEnumerable<TDatum>> getData, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using DbDataReader reader = cmd.ExecuteReader();
		await foreach (var d in getData(reader))
		{
			yield return d;
		}
		await reader.ReadAllResultsAsync();
	}
	/// <summary>
	/// Executes a command using the provided parameters, invokes <paramref name="getData"/> to get an enumerable of objects of type <typeparamref name="TDatum"/>, and returns the enumerable.
	/// Takes care of disposing of the created <see cref="DbCommand"/> and <see cref="DbDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="getData">The function that reads data from the <see cref="DbDataReader"/> and returns an <see cref="IAsyncEnumerable{T}"/>.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static async IAsyncEnumerable<TDatum> ExecuteAsync<TDatum, TParams>(this DbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, Func<DbDataReader, IAsyncEnumerable<TDatum>> getData, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using DbDataReader reader = cmd.ExecuteReader();
		await foreach (var d in getData(reader))
		{
			yield return d;
		}
		await reader.ReadAllResultsAsync();
	}
	/// <summary>
	/// Executes a command using the provided parameters, invokes <paramref name="getData"/> to get an enumerable of objects of type <typeparamref name="TDatum"/>, and returns the enumerable.
	/// Takes care of disposing of the created <see cref="IDbCommand"/> and <see cref="IDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="getData">The function that reads data from the <see cref="IDataReader"/> and returns an <see cref="IEnumerable{T}"/>.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static IEnumerable<TDatum> Execute<TDatum>(this IDbConnection connection, string sql, Func<IDataReader, IEnumerable<TDatum>> getData, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using IDataReader reader = cmd.ExecuteReader();
		foreach (var d in getData(reader))
		{
			yield return d;
		}
		reader.ReadAllResults();
	}
	/// <summary>
	/// Executes a command using the provided parameters, invokes <paramref name="getData"/> to get an enumerable of objects of type <typeparamref name="TDatum"/>, and returns the enumerable.
	/// Takes care of disposing of the created <see cref="IDbCommand"/> and <see cref="IDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="getData">The function that reads data from the <see cref="IDataReader"/> and returns an <see cref="IEnumerable{T}"/>.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static IEnumerable<TDatum> Execute<TDatum, TParams>(this IDbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, Func<IDataReader, IEnumerable<TDatum>> getData, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using IDataReader reader = cmd.ExecuteReader();
		foreach (var d in getData(reader))
		{
			yield return d;
		}
		reader.ReadAllResults();
	}
	/// <summary>
	/// Executes a command using the provided parameters, and invokes <paramref name="getData"/> to get a single object of type <typeparamref name="TDatum"/>.
	/// Takes care of disposing of the created <see cref="DbCommand"/> and <see cref="DbDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="getData">The function that reads data from the <see cref="DbDataReader"/> and returns a <typeparamref name="TDatum"/> or null.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static TDatum? ExecuteFirstOrDefault<TDatum>(this DbConnection connection, string sql, Func<DbDataReader, TDatum?> getData, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using DbDataReader reader = cmd.ExecuteReader();
		var d = getData(reader);
		reader.ReadAllResults();
		return d;
	}
	/// <summary>
	/// Executes a command using the provided parameters, and invokes <paramref name="getData"/> to get a single object of type <typeparamref name="TDatum"/>.
	/// Takes care of disposing of the created <see cref="DbCommand"/> and <see cref="DbDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="getData">The function that reads data from the <see cref="DbDataReader"/> and returns a <typeparamref name="TDatum"/> or null.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static TDatum? ExecuteFirstOrDefault<TDatum, TParams>(this DbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, Func<DbDataReader, TDatum?> getData, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using DbDataReader reader = cmd.ExecuteReader();
		var d = getData(reader);
		reader.ReadAllResults();
		return d;
	}
	/// <summary>
	/// Executes a command using the provided parameters, and invokes <paramref name="getData"/> to get a single object of type <typeparamref name="TDatum"/>.
	/// Takes care of disposing of the created <see cref="DbCommand"/> and <see cref="DbDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="getData">The function that reads data from the <see cref="DbDataReader"/> and returns a <typeparamref name="TDatum"/> or null.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static async Task<TDatum?> ExecuteFirstOrDefaultAsync<TDatum>(this DbConnection connection, string sql, Func<DbDataReader, Task<TDatum?>> getData, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using DbDataReader reader = cmd.ExecuteReader();
		var d = await getData(reader);
		reader.ReadAllResults();
		return d;
	}
	/// <summary>
	/// Executes a command using the provided parameters, and invokes <paramref name="getData"/> to get a single object of type <typeparamref name="TDatum"/>.
	/// Takes care of disposing of the created <see cref="DbCommand"/> and <see cref="DbDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="DbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="getData">The function that reads data from the <see cref="DbDataReader"/> and returns a <typeparamref name="TDatum"/> or null.</param>
	/// <param name="transaction">The <see cref="DbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="DbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="DbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static async Task<TDatum?> ExecuteFirstOrDefaultAsync<TDatum, TParams>(this DbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, Func<DbDataReader, Task<TDatum?>> getData, DbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using DbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using DbDataReader reader = cmd.ExecuteReader();
		var d = await getData(reader);
		reader.ReadAllResults();
		return d;
	}
	/// <summary>
	/// Executes a command using the provided parameters, and invokes <paramref name="getData"/> to get a single object of type <typeparamref name="TDatum"/>.
	/// Takes care of disposing of the created <see cref="IDbCommand"/> and <see cref="IDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="getData">The function that reads data from the <see cref="IDataReader"/> and returns a <typeparamref name="TDatum"/> or null.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static TDatum? ExecuteFirstOrDefault<TDatum>(this IDbConnection connection, string sql, Func<IDataReader, TDatum?> getData, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using IDataReader reader = cmd.ExecuteReader();
		var d = getData(reader);
		reader.ReadAllResults();
		return d;
	}
	/// <summary>
	/// Executes a command using the provided parameters, and invokes <paramref name="getData"/> to get a single object of type <typeparamref name="TDatum"/>.
	/// Takes care of disposing of the created <see cref="IDbCommand"/> and <see cref="IDataReader"/>.
	/// </summary>
	/// <param name="connection">The connection from which to create the command.</param>
	/// <param name="sql">The <see cref="IDbCommand.CommandText"/>.</param>
	/// <param name="parameters">The parameters.</param>
	/// <param name="paramsApplicator">The parameter applicator.</param>
	/// <param name="getData">The function that reads data from the <see cref="IDataReader"/> and returns a <typeparamref name="TDatum"/> or null.</param>
	/// <param name="transaction">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <param name="commandTimeout">The <see cref="IDbCommand.CommandTimeout"/>. If equal to <see cref="int.MinValue"/>, not set.</param>
	/// <param name="commandType">The <see cref="IDbCommand.Transaction"/>.</param>
	/// <returns>The created command.</returns>
	public static TDatum? ExecuteFirstOrDefault<TDatum, TParams>(this IDbConnection connection, string sql, TParams parameters, IDbParamsApplicator<TParams> paramsApplicator, Func<IDataReader, TDatum?> getData, IDbTransaction? transaction = null, int commandTimeout = int.MinValue, CommandType commandType = CommandType.Text)
	{
		using IDbCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.CommandType = commandType;
		cmd.Transaction = transaction;
		paramsApplicator.ApplyParameters(parameters, cmd);
		if (commandTimeout != int.MinValue) { cmd.CommandTimeout = commandTimeout; }
		using IDataReader reader = cmd.ExecuteReader();
		var d = getData(reader);
		reader.ReadAllResults();
		return d;
	}
}
