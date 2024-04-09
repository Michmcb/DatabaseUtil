namespace DatabaseUtil;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// A way of indicating a value or an absence of a value. Useful to differentiate between the cases of for example,
/// selecting a scalar, and getting no value, a null value, and a value.
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly struct Val<T>
{
	/// <summary>
	/// Creates a new instance with the provided value. <see cref="HasValue"/> will be true.
	/// </summary>
	/// <param name="value">The value.</param>
	public Val(T value)
	{
		HasValue = true;
		Value = value;
	}
	/// <summary>
	/// True if a value is present, false otherwise.
	/// </summary>
	public bool HasValue { get; }
	/// <summary>
	/// The value.
	/// </summary>
	public T Value { get; }
	/// <summary>
	/// Returns the value if one is present, otherwise returns <paramref name="ifNone"/>.
	/// </summary>
	/// <param name="ifNone">The default value.</param>
	/// <returns>The value or <paramref name="ifNone"/>.</returns>
	[return: NotNullIfNotNull(nameof(ifNone))]
	public T? ValueOrDefault(T? ifNone)
	{
		return HasValue ? Value : ifNone;
	}
	/// <summary>
	/// Equivalent to creating a new instance.
	/// </summary>
	/// <param name="val">The value.</param>
	public static implicit operator Val<T>(T val) => new(val);
}
