namespace DatabaseUtil.SourceGen;
using System.Collections.Generic;

public sealed class Attrib
{
	public static readonly Attrib DbRecordReader = New("DbRecordReader", "DatabaseUtil.");
	public static readonly Attrib DbRecord = New("DbRecord", "DatabaseUtil.");
	public static readonly Attrib DbConverter = New("DbConverter", "DatabaseUtil.");
	public static readonly Attrib DbParams = New("DbParams", "DatabaseUtil.");
	public static readonly Attrib HasName = New("HasName", "DatabaseUtil.");
	public static readonly Attrib HasOrdinal = New("HasOrdinal", "DatabaseUtil.");
	public Attrib(string name, string fullName, string fullNameWithNamespace, HashSet<string> names)
	{
		Name = name;
		FullName = fullName;
		FullNameWithNamespace = fullNameWithNamespace;
		Names = names;
	}
	public string Name { get; }
	public string FullName { get; }
	public string FullNameWithNamespace { get; }
	public HashSet<string> Names { get; }
	public static Attrib New(string name, string nsWithDot)
	{
		HashSet<string> hs = [];
		string fullName = name + "Attribute";
		hs.Add(name);
		hs.Add(fullName);
		hs.Add(nsWithDot + name);
		hs.Add(nsWithDot + fullName);
		return new Attrib(name, fullName, nsWithDot + fullName, hs);
	}
}
