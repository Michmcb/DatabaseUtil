namespace DatabaseUtil;

using System.Data;
using System.Data.Common;

/// <summary>
/// A helper class to concisely add parameters.
/// </summary>
public sealed class ParamsDbCommand
{
	/// <summary>
	/// Creates a new instance, wrapping <paramref name="cmd"/>.
	/// </summary>
	/// <param name="cmd">The <see cref="DbCommand"/> to wrap.</param>
	public ParamsDbCommand(DbCommand cmd)
	{
		Cmd = cmd;
	}
	/// <summary>
	/// The <see cref="DbCommand"/> to which parameters are added.
	/// </summary>
	public DbCommand Cmd { get; }
	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	public ParamsDbCommand Add(string name, object? value)
	{
		Cmd.AddParameter(name, value);
		return this;
	}
	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	/// <param name="p">The added parameter.</param>
	public ParamsDbCommand Add(string name, object? value, out DbParameter p)
	{
		p = Cmd.AddParameter(name, value);
		return this;
	}

	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	/// <param name="direction">The parameter direction.</param>
	public ParamsDbCommand Add(string name, object? value, ParameterDirection direction)
	{
		Cmd.AddParameter(name, value, direction);
		return this;
	}
	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	/// <param name="direction">The parameter direction.</param>
	/// <param name="p">The added parameter.</param>
	public ParamsDbCommand Add(string name, object? value, ParameterDirection direction, out DbParameter p)
	{
		p = Cmd.AddParameter(name, value, direction);
		return this;
	}

	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	/// <param name="precision">The maximum number of digits used to represent the value.</param>
	/// <param name="scale">The number of decimal places to which the value is resolved.</param>
	public ParamsDbCommand Add(string name, object? value, byte precision, byte scale)
	{
		Cmd.AddParameter(name, value, precision, scale);
		return this;
	}
	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	/// <param name="precision">The maximum number of digits used to represent the value.</param>
	/// <param name="scale">The number of decimal places to which the value is resolved.</param>
	/// <param name="p">The added parameter.</param>
	public ParamsDbCommand Add(string name, object? value, byte precision, byte scale, out DbParameter p)
	{
		p = Cmd.AddParameter(name, value, precision, scale);
		return this;
	}

	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	/// <param name="direction">The parameter direction.</param>
	/// <param name="precision">The maximum number of digits used to represent the value.</param>
	/// <param name="scale">The number of decimal places to which the value is resolved.</param>
	public ParamsDbCommand Add(string name, object? value, ParameterDirection direction, byte precision, byte scale)
	{
		Cmd.AddParameter(name, value, direction, precision, scale);
		return this;
	}
	/// <summary>
	/// Adds a parameter.
	/// </summary>
	/// <param name="name">The parameter name.</param>
	/// <param name="value">The parameter value.</param>
	/// <param name="direction">The parameter direction.</param>
	/// <param name="precision">The maximum number of digits used to represent the value.</param>
	/// <param name="scale">The number of decimal places to which the value is resolved.</param>
	/// <param name="p">The added parameter.</param>
	public ParamsDbCommand Add(string name, object? value, ParameterDirection direction, byte precision, byte scale, out DbParameter p)
	{
		p = Cmd.AddParameter(name, value, direction, precision, scale);
		return this;
	}
}
