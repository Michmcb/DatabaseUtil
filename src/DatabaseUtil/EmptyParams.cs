namespace DatabaseUtil;

using System.Data;
using System.Data.Common;

/// <summary>
/// Empty parameters.
/// </summary>
public sealed class EmptyParams : IDbParams
{
	/// <summary>
	/// The singleton instance.
	/// </summary>
	public static readonly EmptyParams Instance = new();
	/// <summary>
	/// Does nothing.
	/// </summary>
	public void ApplyTo(IDbCommand cmd) { }
	/// <summary>
	/// Does nothing.
	/// </summary>
	public void ApplyTo(DbCommand cmd) { }
}
