namespace DatabaseUtil;

/// <summary>
/// Extension methods, for differentiating between struct/class, since we can't do that on <see cref="DbVal{T}"/> itself.
/// </summary>
public static class DbValExtensions
{
	/// <summary>
	/// Returns <see langword="true"/> if <see cref="DbVal{T}.Ok"/> or <see cref="DbVal{T}.Null"/> are true,
	/// and <see langword="false"/> otherwise (i.e. there were no rows).
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="d">The instance.</param>
	/// <param name="value">The value.</param>
	public static bool TryGetOrNull<T>(this DbVal<T> d, out T? value) where T : struct
	{
		if (d.Ok)
		{
			value = d.value;
			return true;
		}
		if (d.Null)
		{
			value = null;
			return true;
		}
		value = null;
		return false;
	}
	/// <summary>
	/// Returns <see langword="true"/> if <see cref="DbVal{T}.Ok"/> or <see cref="DbVal{T}.Null"/> are true,
	/// and <see langword="false"/> otherwise (i.e. there were no rows).
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="d">The instance.</param>
	/// <param name="value">The value.</param>
	public static bool TryGetOrNull<T>(this DbVal<T> d, out T? value) where T : class
	{
		if (d.Ok)
		{
			value = d.value;
			return true;
		}
		if (d.Null)
		{
			value = null;
			return true;
		}
		value = null;
		return false;
	}
}
