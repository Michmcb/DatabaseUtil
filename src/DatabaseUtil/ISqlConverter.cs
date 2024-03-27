namespace DatabaseUtil;

/// <summary>
/// A converter that can convert between a database value and a .net value.
/// </summary>
/// <typeparam name="TDb">The type of the database value.</typeparam>
/// <typeparam name="TNet">The type of the .net value.</typeparam>
public interface ISqlConverter<TDb, TNet>
{
	/// <summary>
	/// Converts from the database value to the .net value.
	/// </summary>
	/// <param name="dbValue">The database value to convert.</param>
	/// <returns>The converted .net value.</returns>
	TNet DbToDotNet(TDb dbValue);
	/// <summary>
	/// Converts the .net value to the datbase value.
	/// </summary>
	/// <param name="dotNetValue">The .net value to convert.</param>
	/// <returns>The converted database value.</returns>
	TDb DotNetToDb(TNet dotNetValue);
}
