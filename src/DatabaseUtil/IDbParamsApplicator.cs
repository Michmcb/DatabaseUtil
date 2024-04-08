namespace DatabaseUtil;

using System.Data;
using System.Data.Common;

/// <summary>
/// An interface that applies parameters of type <typeparamref name="T"/> to <see cref="DbCommand"/> or an <see cref="IDbCommand"/>.
/// </summary>

public interface IDbParamsApplicator<T>
{
	/// <summary>
	/// Adds parameters to an <see cref="IDbCommand"/>.
	/// </summary>
	/// <param name="p">The parameters.</param>
	/// <param name="cmd">The command to which the parameters should be added.</param>
	void ApplyParameters(T p, IDbCommand cmd);
	/// <summary>
	/// Adds parameters to a <see cref="DbCommand"/>.
	/// </summary>
	/// <param name="p">The parameters.</param>
	/// <param name="cmd">The command to which the parameters should be added.</param>
#if NET8_0_OR_GREATER
	void ApplyParameters(T p, DbCommand cmd)
	{
		ApplyParameters(p, (IDbCommand)cmd);
	}
#else
	void ApplyTo(p, DbCommand cmd);
#endif
}
