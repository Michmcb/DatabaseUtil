namespace DatabaseUtil;

using System;
using System.Data;
using System.Data.Common;
/// <summary>
/// Extension methods for <see cref="IDbCommand"/> and <see cref="DbCommand"/>.
/// </summary>
public static class DbCommandExtensions
{
	/// <summary>
	/// Creates a new instance of <see cref="ParamsIDbCommand"/>, wrapping <paramref name="cmd"/>.
	/// </summary>
	public static ParamsIDbCommand Params(this IDbCommand cmd)
	{
		return new ParamsIDbCommand(cmd);
	}
	/// <summary>
	/// Creates a new instance of <see cref="ParamsDbCommand"/>, wrapping <paramref name="cmd"/>.
	/// </summary>
	public static ParamsDbCommand Params(this DbCommand cmd)
	{
		return new ParamsDbCommand(cmd);
	}
	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="cmd">The command to which the parameter shall be added.</param>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	public static IDbDataParameter AddParameter(this IDbCommand cmd, string name, object? value)
	{
		var p = cmd.CreateParameter();
		p.ParameterName = name;
		p.Value = value ?? DBNull.Value;
		cmd.Parameters.Add(p);
		return p;
	}
	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="cmd">The command to which the parameter shall be added.</param>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	/// <param name="direction">The parameter direction.</param>
	public static IDbDataParameter AddParameter(this IDbCommand cmd, string name, object? value, ParameterDirection direction)
	{
		var p = cmd.CreateParameter();
		p.ParameterName = name;
		p.Value = value ?? DBNull.Value;
		p.Direction = direction;
		cmd.Parameters.Add(p);
		return p;
	}
	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="cmd">The command to which the parameter shall be added.</param>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	/// <param name="precision">The maximum number of digits used to represent the value.</param>
	/// <param name="scale">The number of decimal places to which the value is resolved.</param>
	public static IDbDataParameter AddParameter(this IDbCommand cmd, string name, object? value, byte precision, byte scale)
	{
		var p = cmd.CreateParameter();
		p.ParameterName = name;
		p.Value = value ?? DBNull.Value;
		p.Precision = precision;
		p.Scale = scale;
		cmd.Parameters.Add(p);
		return p;
	}
	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="cmd">The command to which the parameter shall be added.</param>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	/// <param name="direction">The parameter direction.</param>
	/// <param name="precision">The maximum number of digits used to represent the value.</param>
	/// <param name="scale">The number of decimal places to which the value is resolved.</param>
	public static IDbDataParameter AddParameter(this IDbCommand cmd, string name, object? value, ParameterDirection direction, byte precision, byte scale)
	{
		var p = cmd.CreateParameter();
		p.ParameterName = name;
		p.Value = value ?? DBNull.Value;
		p.Direction = direction;
		p.Precision = precision;
		p.Scale = scale;
		cmd.Parameters.Add(p);
		return p;
	}
	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="cmd">The command to which the parameter shall be added.</param>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	public static DbParameter AddParameter(this DbCommand cmd, string name, object? value)
	{
		var p = cmd.CreateParameter();
		p.ParameterName = name;
		p.Value = value ?? DBNull.Value;
		cmd.Parameters.Add(p);
		return p;
	}
	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="cmd">The command to which the parameter shall be added.</param>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	/// <param name="direction">The parameter direction.</param>
	public static DbParameter AddParameter(this DbCommand cmd, string name, object? value, ParameterDirection direction)
	{
		var p = cmd.CreateParameter();
		p.ParameterName = name;
		p.Value = value ?? DBNull.Value;
		p.Direction = direction;
		cmd.Parameters.Add(p);
		return p;
	}
	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="cmd">The command to which the parameter shall be added.</param>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	/// <param name="precision">The maximum number of digits used to represent the value.</param>
	/// <param name="scale">The number of decimal places to which the value is resolved.</param>
	public static DbParameter AddParameter(this DbCommand cmd, string name, object? value, byte precision, byte scale)
	{
		var p = cmd.CreateParameter();
		p.ParameterName = name;
		p.Value = value ?? DBNull.Value;
		p.Precision = precision;
		p.Scale = scale;
		cmd.Parameters.Add(p);
		return p;
	}
	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="cmd">The command to which the parameter shall be added.</param>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	/// <param name="direction">The parameter direction.</param>
	/// <param name="precision">The maximum number of digits used to represent the value.</param>
	/// <param name="scale">The number of decimal places to which the value is resolved.</param>
	public static DbParameter AddParameter(this DbCommand cmd, string name, object? value, ParameterDirection direction, byte precision, byte scale)
	{
		var p = cmd.CreateParameter();
		p.ParameterName = name;
		p.Value = value ?? DBNull.Value;
		p.Direction = direction;
		p.Precision = precision;
		p.Scale = scale;
		cmd.Parameters.Add(p);
		return p;
	}
}
