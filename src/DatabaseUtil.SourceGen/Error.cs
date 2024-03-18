namespace DatabaseUtil.SourceGen;

using System;

// TODO best to replace this with a static class with methods that can generate Diagnostics for us.
public static class Error
{
	public const string P = "DBU";

	public const string MissingReadMethod = P + "0001";
	public const string MalformedReadMethod = P + "0002";
	public const string ReaderClassUnusable = P + "0003";
	public const string ReaderClassMissingNamespace = P + "0004";
	[Obsolete("This is no longer a concert")]public const string IsNotARecord = P + "0005";
	public const string MissingParameters = P + "0006";
	public const string MissingIndex = P + "0007";
	public const string MissingName = P + "0008";
	public const string WrongAttributeDecorated = P + "0009";
	public const string ClassNotPartial = P + "0010";
	public const string OnlyOneCtorAllowed = P + "0011";

	public const string CannotGetSymbol = P + "9998";
}
public static class DiagCat
{
	public const string InternalError = nameof(InternalError);
	public const string Usage = nameof(Usage);
}