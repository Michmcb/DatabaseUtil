namespace DatabaseUtil;

using System;

/// <summary>
/// Any errors encountered reading a value from the database.
/// Note that the default value corresponds to <see cref="Null"/>, NOT <see cref="Ok"/>.
/// </summary>
[Flags]
public enum DbValError : byte
{
	/// <summary>
	/// The value was <see cref="DBNull"/>.
	/// </summary>
	Null = 0,
	/// <summary>
	/// The value was fine.
	/// </summary>
	Ok = 1,
	/// <summary>
	/// No results received from the database.
	/// </summary>
	NoRow = 2,
	/// <summary>
	/// Value was the wrong type.
	/// </summary>
	WrongType = 4,
}
