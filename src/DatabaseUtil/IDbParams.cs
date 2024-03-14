namespace DatabaseUtil;

using System.Data;
using System.Data.Common;

/// <summary>
/// An interface to abstract adding parameters to a <see cref="DbCommand"/> or an <see cref="IDbCommand"/>.
/// </summary>
public interface IDbParams
{
	/// <summary>
	/// Adds parameters to an <see cref="IDbCommand"/>.
	/// </summary>
	/// <param name="cmd">The command to which the parameters should be added.</param>
	void ApplyTo(IDbCommand cmd);
	/// <summary>
	/// Adds parameters to a <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="cmd">The command to which the parameters should be added.</param>
	void ApplyTo(DbCommand cmd);
}
