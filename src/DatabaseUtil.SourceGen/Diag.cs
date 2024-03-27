namespace DatabaseUtil.SourceGen;

using Microsoft.CodeAnalysis;

public static class Diag
{
	public static class Cat
	{
		public const string InternalError = nameof(InternalError);
		public const string Usage = nameof(Usage);
	}

	private const string P = "DBU";
	private const string missingReadMethod = P + "0001";
	private const string malformedReadMethod = P + "0002";
	//private const string readerClassUnusable = P + "0003";
	private const string readerClassMissingNamespace = P + "0004";
	//private const string isNotARecord = P + "0005";
	private const string missingParameters = P + "0006";
	private const string missingIndex = P + "0007";
	private const string missingName = P + "0008";
	private const string wrongAttributeDecorated = P + "0009";
	private const string classNotPartial = P + "0010";
	private const string onlyOneCtorAllowed = P + "0011";
	private const string missingConverterMethod = P + "0012";
	private const string converterWrongTypeKind = P + "0013";
	private const string cannotGetSymbol = P + "9998";
	//private const string somethingButIDontRemberWhatItWas = P + "9999";
	//public static Diagnostic MissingReadMethod(Location location, string className, string typeName)
	//{
	//	return Diagnostic.Create(new DiagnosticDescriptor(id: missingReadMethod, title: "Missing read method",
	//		messageFormat: "Could not find any method on class {0} to read a field of type {1}. Add a method decorated with [" + Attrib.DbGetField.Name + "], declared like: \"public {1} Get{1}(IDataRecord reader, int index)\"" +
	//		". The first parameter can be anything that implements IDataRecord (such as IDataRecord, IDataReader, or DbDataReader).",
	//		Cat.Usage, DiagnosticSeverity.Error, isEnabledByDefault: true), location, className, typeName);
	//}
	public static Diagnostic MalformedReadMethod(Location location, string className, string methodName)
	{
		return Diagnostic.Create(new DiagnosticDescriptor(id: malformedReadMethod, title: "Malformed read method",
			messageFormat: "Read method {1} on class {0} is incorrect and cannot be used. Return type must be non-null, first param must implement IDataRecord, second param must be int. Like this: \"public Foo GetFoo(DbDataReader reader, int index)\". " +
			"The method name can be anything you want.",
			Cat.Usage, DiagnosticSeverity.Warning, isEnabledByDefault: true), location, className, methodName);
	}
	public static Diagnostic ReaderClassMissingNamespace(Location location, string className)
	{
		return Diagnostic.Create(new DiagnosticDescriptor(id: readerClassMissingNamespace, title: "Reader class missing namespace",
			messageFormat: "Class {0} must be declared in a namespace.",
			Cat.Usage, DiagnosticSeverity.Error, isEnabledByDefault: true), location, className);
	}
	public static Diagnostic MissingParameters(Location location, string classStructOrRecord, string className)
	{
		return Diagnostic.Create(new DiagnosticDescriptor(id: missingParameters, title: "Record missing parameters",
			messageFormat: "The {0} {1} needs parameters to have code generated to read it from an IDataRecord.",
			Cat.Usage, DiagnosticSeverity.Warning, isEnabledByDefault: true), location, classStructOrRecord, className);
	}
	public static Diagnostic MissingIndex(Location location, string parameterName)
	{
		return Diagnostic.Create(new DiagnosticDescriptor(id: missingIndex, title: "Parameter missing index",
			messageFormat: "Parameter {0} needs an index defined on it when decorated with the " + Attrib.HasOrdinal.Name + " attribute.",
			Cat.Usage, DiagnosticSeverity.Error, isEnabledByDefault: true), location, parameterName);
	}
	public static Diagnostic MissingName(Location location, string parameterName)
	{
		return Diagnostic.Create(new DiagnosticDescriptor(id: missingName, title: "Parameter missing name",
			messageFormat: "Parameter {0} needs a name defined on it when decorated with the " + Attrib.HasName.Name + " attribute.",
			Cat.Usage, DiagnosticSeverity.Error, isEnabledByDefault: true), location, parameterName);
	}
	public static Diagnostic WrongAttributeDecorated(Location location, string parameterName, string classStructOrRecord)
	{
		return Diagnostic.Create(new DiagnosticDescriptor(id: wrongAttributeDecorated, title: "Wrong attribute decorated",
			messageFormat: "Parameter {0} is decorated with the " + Attrib.HasOrdinal.Name + " attribute, but its {1} indicates that it is indexed by name. Decorate it with the " + Attrib.HasName.Name + " attribute instead.",
			Cat.Usage, DiagnosticSeverity.Error, isEnabledByDefault: true), location, parameterName, classStructOrRecord);
	}
	public static Diagnostic ClassNotPartial(Location location, string className)
	{
		return Diagnostic.Create(new DiagnosticDescriptor(id: classNotPartial, title: "Class is not partial",
			messageFormat: "Class {0} must be declared as partial to be used as the class which holds generated read methods.",
			Cat.Usage, DiagnosticSeverity.Error, isEnabledByDefault: true), location, className);
	}
	public static Diagnostic OnlyOneCtorAllowed(Location location, string className, string classStructOrRecord)
	{
		return Diagnostic.Create(new DiagnosticDescriptor(id: onlyOneCtorAllowed, title: "Only 1 constructor allowed",
			messageFormat: "The {1} {0} must have exactly 1 and only 1 constructor to be used as a " + Attrib.DbRecord.Name,
			Cat.Usage, DiagnosticSeverity.Error, isEnabledByDefault: true), location, className, classStructOrRecord);
	}
	public static Diagnostic MissingConverterMethod(Location location, string missingType, string containingType, string dbRecordReaderClassName)
	{
		return Diagnostic.Create(new DiagnosticDescriptor(id: missingConverterMethod, title: "Missing converter method",
			messageFormat: "Could not find any converter that can produce type {0}. The type in question that needs this is {1}. Add a ISqlConverter as a property to class {2}, and decorate it with [" + Attrib.DbConverter.Name + "]. " +
			"If the type is an enum, you may instead add a converter for the underlying integer type of the enum, which will work fine as well.",
			Cat.Usage, DiagnosticSeverity.Error, isEnabledByDefault: true), location, missingType, containingType, dbRecordReaderClassName);
	}
	public static Diagnostic ConverterWrongTypeKind(Location location)
	{
		return Diagnostic.Create(new DiagnosticDescriptor(id: converterWrongTypeKind, title: Attrib.DbConverter.Name + " applied to wrong kind",
			messageFormat: "The attribute [" + Attrib.DbConverter.Name + "] should only be applied to properties whose type is a class, interface, or struct.",
			Cat.InternalError, DiagnosticSeverity.Error, isEnabledByDefault: true), location);
	}
	public static Diagnostic CannotGetSymbol(Location location, string thing, string identifier)
	{
		return Diagnostic.Create(new DiagnosticDescriptor(id: cannotGetSymbol, title: "Could not get symbol",
			messageFormat: "Could not get symbol for {0} {1}.",
			Cat.InternalError, DiagnosticSeverity.Error, isEnabledByDefault: true), location, thing, identifier);
	}
}