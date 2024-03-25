namespace DatabaseUtil.SourceGen;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

[Generator]
public sealed class SourceGen : ISourceGenerator
{
	public void Initialize(GeneratorInitializationContext context)
	{
		context.RegisterForPostInitialization((x) =>
		{
			x.AddSource("DatabaseUtil.SourceGenEnums.g.cs", SourceText.From("namespace DatabaseUtil\n" +
"{\n" +

"\t/// <summary>\n" +
"\t/// Defines how fields read from the database are indexed.\n" +
"\t/// </summary>\n" +
"\tinternal enum ReadBy\n" +
"\t{\n" +
"\t\t/// <summary>\n" +
"\t\t/// The default. If nothing else is specified, this is by Name. \n" +
"\t\t/// </summary>\n" +
"\t\tDefault = 0,\n" +
"\t\t/// <summary>\n" +
"\t\t/// Fields from the database are indexed by column names.\n" +
"\t\t/// </summary>\n" +
"\t\tName = 1,\n" +
"\t\t/// <summary>\n" +
"\t\t/// Fields from the database are indexed by explicit ordinals.\n" +
"\t\t/// </summary>\n" +
"\t\tOrdinal = 2,\n" +
"\t}\n" +

"\t/// <summary>\n" +
"\t/// Defines which parameter types the generated methods will prefer out of <see cref=\"System.Data.Common.DbDataReader\"/>, <see cref=\"System.Data.IDataReader\"/>, or <see cref=\"System.Data.IDataRecord\"/>." +
"\t/// </summary>\n" +
"\tinternal enum DbDataReaderPref\n" +
"\t{\n" +
"\t\t/// <summary>\n" +
"\t\t/// Use the abstract type <see cref=\"System.Data.Common.DbDataReader\"/> everywhere.\n" +
"\t\t/// </summary>\n" +
"\t\tDbDataReader = 0,\n" +
"\t\t/// <summary>\n" +
"\t\t/// Uses the interface type <see cref=\"System.Data.IDataReader\"/> where possible.\n" +
"\t\t/// Methods that require <see cref=\"System.Data.Common.DbDataReader\"/> will still use it.\n" +
"\t\t/// </summary>\n" +
"\t\tIDataReader = 1,\n" +
"\t\t/// <summary>\n" +
"\t\t/// Uses the interface type <see cref=\"System.Data.IDataRecord\"/> where possible.\n" +
"\t\t/// Methods that require <see cref=\"System.Data.Common.DbDataReader\"/> will still use it.\n" +
"\t\t/// </summary>\n" +
"\t\tIDataRecord = 2,\n" +
"\t}" +
"}", Encoding.UTF8));

			x.AddSource("DatabaseUtil.SourceGenAttributes.g.cs", SourceText.From("namespace DatabaseUtil\n" +
"{\n" +
"\tusing System;\n" +
"\tusing System.Data;\n" +
"\t/// <summary>\n" +
"\t/// Decorating a record class or record struct with this attribute will cause methods to be generated\n" +
"\t/// on a class decorated with <see cref=\"" + Attrib.DbRecordReader.FullName + "\"/>. The generated methods read an instance of the decorated class or struct.\n" +
"\t/// </summary>\n" +
"\t[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]\n" +
"\tinternal sealed class " + Attrib.DbRecord.FullName + " : Attribute\n" +
"\t{\n" +
"\t\tinternal " + Attrib.DbRecord.FullName + "(ReadBy readBy = ReadBy.Default) { }\n" +
"\t}\n" +


"\t/// <summary>\n" +
"\t/// Denotes the name that this has on the database side.\n" +
"\t/// </summary>\n" +
"\t[AttributeUsage(AttributeTargets.Parameter)]\n" +
"\tinternal sealed class " + Attrib.HasName.FullName + " : Attribute\n" +
"\t{\n" +
"\t\tinternal " + Attrib.HasName.FullName + "(string name) { }\n" +
"\t}\n" +


"\t/// <summary>\n" +
"\t/// Denotes the ordinal that this has on the database side.\n" +
"\t/// </summary>\n" +
"\t[AttributeUsage(AttributeTargets.Parameter)]\n" +
"\tinternal sealed class " + Attrib.HasOrdinal.FullName + " : Attribute\n" +
"\t{\n" +
"\t\tinternal " + Attrib.HasOrdinal.FullName + "(int index) { }\n" +
"\t}\n" +


"\t/// <summary>\n" +
"\t/// Decorating a class with this attribute makes it the target of generated methods that read in records decorated with <see cref=\"" + Attrib.DbRecord.FullName + "\"/>\n" +
"\t/// </summary>\n" +
"\t[AttributeUsage(AttributeTargets.Class)]\n" +
"\tinternal sealed class " + Attrib.DbRecordReader.FullName + " : Attribute\n" +
 "\t{\n" +
 "\t\tinternal " + Attrib.DbRecordReader.FullName + "(DbDataReaderPref pref = DbDataReaderPref.DbDataReader) { }\n" +
 "\t}\n" +


"\t/// <summary>\n" +
"\t/// Decorating a method on a class that's also decorated with <see cref=\"" + Attrib.DbRecordReader.FullName + "\"/> will cause it to be used to read fields of\n" +
"\t/// the decorated method's return type from <see cref=\"IDataRecord\"/>. Methods decorated with this attribute take precedence over any native methods on <see cref=\"IDataRecord\"/>.\n" +
"\t/// </summary>\n" +
"\t[AttributeUsage(AttributeTargets.Method)]\n" +
"\t[Obsolete(\"Use DbConverters instead\")]\n" +
"\tinternal sealed class " + Attrib.DbGetField.FullName + " : Attribute { }\n" +


"\t/// <summary>\n" +
"\t/// Decorating a property on a class that's also decorated with <see cref=\"" + Attrib.DbRecordReader.FullName + "\"/> will cause it to be used to convert types to and from database and .net.\n" +
"\t/// </summary>\n" +
"\t[AttributeUsage(AttributeTargets.Property)]\n" +
"\tinternal sealed class " + Attrib.DbConverter.FullName + " : Attribute { }\n" +


"\t/// <summary>\n" +
"\t/// Decorating a method on a class that's also decorated with <see cref=\"" + Attrib.DbParams.FullName + "\"/> will implement the <see cref=\"IDbParams\"/> interface, and set all properties as parameters.\n" +
"\t/// </summary>\n" +
"\t[AttributeUsage(AttributeTargets.Class)]\n" +
"\tinternal sealed class " + Attrib.DbParams.FullName + " : Attribute { }\n" +
"}", Encoding.UTF8));
		});
//#if DEBUG
//		if (!System.Diagnostics.Debugger.IsAttached) { System.Diagnostics.Debugger.Launch(); }
//#endif
	}
	public string FullyQualifiedName(ISymbol sym)
	{
		return sym.ContainingNamespace == null ? sym.Name : string.Concat(sym.ContainingNamespace.ToString(), ".", sym.Name);
	}
	public bool ImplementsInterface(string interfaceName, ITypeSymbol sym)
	{
		return interfaceName == FullyQualifiedName(sym) || sym.Interfaces.Any(nts => ImplementsInterface(interfaceName, nts));
	}
	public void Execute(GeneratorExecutionContext context)
	{
		Dictionary<ITypeSymbol, string> builtInReadMethods = new(SymbolEqualityComparer.Default)
		{
			[context.Compilation.GetSpecialType(SpecialType.System_Boolean)] = nameof(DbDataReader.GetBoolean),
			[context.Compilation.GetSpecialType(SpecialType.System_Byte)] = nameof(DbDataReader.GetByte),
			[context.Compilation.GetSpecialType(SpecialType.System_Int16)] = nameof(DbDataReader.GetInt16),
			[context.Compilation.GetSpecialType(SpecialType.System_Int32)] = nameof(DbDataReader.GetInt32),
			[context.Compilation.GetSpecialType(SpecialType.System_Int64)] = nameof(DbDataReader.GetInt64),
			[context.Compilation.GetSpecialType(SpecialType.System_Single)] = nameof(DbDataReader.GetFloat),
			[context.Compilation.GetSpecialType(SpecialType.System_Double)] = nameof(DbDataReader.GetDouble),
			[context.Compilation.GetSpecialType(SpecialType.System_Decimal)] = nameof(DbDataReader.GetDecimal),
			[context.Compilation.GetSpecialType(SpecialType.System_Char)] = nameof(DbDataReader.GetChar),
			[context.Compilation.GetSpecialType(SpecialType.System_String)] = nameof(DbDataReader.GetString),
			[context.Compilation.GetSpecialType(SpecialType.System_DateTime)] = nameof(DbDataReader.GetDateTime),
			[context.Compilation.GetSpecialType(SpecialType.System_Object)] = nameof(DbDataReader.GetValue),
		};
		{
			INamedTypeSymbol? guidType = context.Compilation.GetTypeByMetadataName(typeof(Guid).FullName);
			if (guidType != null)
			{
				builtInReadMethods[guidType] = nameof(DbDataReader.GetGuid);
			}
		}

		Dictionary<ITypeSymbol, ReadMethod> allReadMethods = new(SymbolEqualityComparer.Default)
		{
			[context.Compilation.GetSpecialType(SpecialType.System_Boolean)] = new(nameof(DbDataReader.GetBoolean), true),
			[context.Compilation.GetSpecialType(SpecialType.System_Byte)] = new(nameof(DbDataReader.GetByte), true),
			[context.Compilation.GetSpecialType(SpecialType.System_Int16)] = new(nameof(DbDataReader.GetInt16), true),
			[context.Compilation.GetSpecialType(SpecialType.System_Int32)] = new(nameof(DbDataReader.GetInt32), true),
			[context.Compilation.GetSpecialType(SpecialType.System_Int64)] = new(nameof(DbDataReader.GetInt64), true),
			[context.Compilation.GetSpecialType(SpecialType.System_Single)] = new(nameof(DbDataReader.GetFloat), true),
			[context.Compilation.GetSpecialType(SpecialType.System_Double)] = new(nameof(DbDataReader.GetDouble), true),
			[context.Compilation.GetSpecialType(SpecialType.System_Decimal)] = new(nameof(DbDataReader.GetDecimal), true),
			[context.Compilation.GetSpecialType(SpecialType.System_Char)] = new(nameof(DbDataReader.GetChar), true),
			[context.Compilation.GetSpecialType(SpecialType.System_String)] = new(nameof(DbDataReader.GetString), true),
			[context.Compilation.GetSpecialType(SpecialType.System_DateTime)] = new(nameof(DbDataReader.GetDateTime), true),
			[context.Compilation.GetSpecialType(SpecialType.System_Object)] = new(nameof(DbDataReader.GetValue), true),
		};
		{
			INamedTypeSymbol? guidType = context.Compilation.GetTypeByMetadataName(typeof(Guid).FullName);
			if (guidType != null)
			{
				allReadMethods[guidType] = new(nameof(DbDataReader.GetGuid), true);
			}
		}

		CancellationToken ct = context.CancellationToken;

		IEnumerable<ClassDeclarationSyntax> targetClasses = context.Compilation.SyntaxTrees
			.SelectMany(x => x.GetRoot(ct).DescendantNodes())
			.OfType<ClassDeclarationSyntax>()
			.Where(x => x.AttributeLists.Any(al => al.Attributes.Any(a => Attrib.DbRecordReader.Names.Contains(a.Name.ToString()))));

		foreach (ClassDeclarationSyntax dbRecordReaderClass in targetClasses)
		{
			if (dbRecordReaderClass == null) { continue; }
			if (!dbRecordReaderClass.Modifiers.Any(x => x.Text == "partial"))
			{
				context.ReportDiagnostic(Diag.ClassNotPartial(dbRecordReaderClass.GetLocation(), dbRecordReaderClass.Identifier.ToString()));
				continue;
			}
			SemanticModel sm = context.Compilation.GetSemanticModel(dbRecordReaderClass.SyntaxTree);
			ISymbol? symTargetClass = sm.GetDeclaredSymbol(dbRecordReaderClass);
			if (symTargetClass == null)
			{
				context.ReportDiagnostic(Diag.CannotGetSymbol(dbRecordReaderClass.GetLocation(), "class", dbRecordReaderClass.Identifier.ToString()));
				continue;
			}
			if (symTargetClass.ContainingNamespace == null)
			{
				context.ReportDiagnostic(Diag.ReaderClassMissingNamespace(dbRecordReaderClass.GetLocation(), dbRecordReaderClass.Identifier.ToString()));
				continue;
			}

			Types types;
			{
				string getOrdinalsType;
				string readAllType;
				string readAllAsyncType;
				string readFirstOrDefaultType;
				string readFirstOrDefaultAsyncType;
				string readType = getOrdinalsType = readAllType = readAllAsyncType = readFirstOrDefaultType = readFirstOrDefaultAsyncType = nameof(DbDataReader);
				AttributeData recordReaderAttrib = symTargetClass.GetAttributes()
					.Where(x => x.AttributeClass != null && Attrib.DbRecordReader.Names.Contains(x.AttributeClass.Name))
					.FirstOrDefault();
				if (recordReaderAttrib != null && recordReaderAttrib.ConstructorArguments.Length > 0)
				{
					object? o = recordReaderAttrib.ConstructorArguments[0].Value;
					if (!(o is null) && o is int drp)
					{
						switch (drp)
						{
							default:
							case 0:
								// DbDataReader
								// All stay as DbDataReader
								break;
							case 1:
								// IDataReader
								getOrdinalsType = nameof(System.Data.IDataReader);
								readAllType = nameof(System.Data.IDataReader);
								readFirstOrDefaultType = nameof(System.Data.IDataReader);
								readType = nameof(System.Data.IDataReader);
								break;
							case 2:
								// IDataRecord
								getOrdinalsType = nameof(System.Data.IDataRecord);
								readAllType = nameof(System.Data.IDataReader);
								readFirstOrDefaultType = nameof(System.Data.IDataReader);
								readType = nameof(System.Data.IDataRecord);
								break;
						}
					}
				}
				types = new Types(getOrdinalsType, readAllType, readAllAsyncType, readFirstOrDefaultType, readFirstOrDefaultAsyncType, readType);
			}

			StringBuilder sbTargetClassStart = new("#nullable enable\nnamespace ");
			sbTargetClassStart.Append(symTargetClass.ContainingNamespace.ToString());
			sbTargetClassStart.Append(
				"\n{" +
				"\n\tusing System.Collections.Generic;" +
				"\n\tusing System.Data;" +
				"\n\tusing System.Data.Common;" +
				"\n\tusing System.Threading.Tasks;\n\t");
			sbTargetClassStart.Append(dbRecordReaderClass.Modifiers.ToString()).Append(" class ").Append(dbRecordReaderClass.Identifier.ToString());
			sbTargetClassStart.Append("\n\t{\n");

			StringBuilder sbTargetClassEnd = new("\t}\n}\n#nullable restore");

			const string requiredFirstParamImpl = "System.Data.IDataRecord";
			foreach (MethodDeclarationSyntax fieldReaderMethod in dbRecordReaderClass
				.DescendantNodes()
				.OfType<MethodDeclarationSyntax>()
				.Where(x => x.AttributeLists.Any(al => al.Attributes.Any(a => Attrib.DbGetField.Names.Contains(a.Name.ToString())))))
			{
				IMethodSymbol? symFieldReaderMethod = sm.GetDeclaredSymbol(fieldReaderMethod);
				if (symFieldReaderMethod != null)
				{
					// The first parameter has to be IDataRecord, or derive from it
					// The second parameter has to be an integer, which represents the ordinal
					if (symFieldReaderMethod.ReturnsVoid == false
						&& symFieldReaderMethod.ReturnType.NullableAnnotation == NullableAnnotation.NotAnnotated
						&& symFieldReaderMethod.Parameters.Length == 2
						&& symFieldReaderMethod.Parameters[1].Type.SpecialType == SpecialType.System_Int32
						&& ImplementsInterface(requiredFirstParamImpl, symFieldReaderMethod.Parameters[0].Type))
					{
						allReadMethods[symFieldReaderMethod.ReturnType] = new(symFieldReaderMethod.Name, false);
					}
					else
					{
						context.ReportDiagnostic(Diag.MalformedReadMethod(dbRecordReaderClass.GetLocation(), dbRecordReaderClass.Identifier.ToString(), symFieldReaderMethod.Name));
					}
				}
				else
				{
					context.ReportDiagnostic(Diag.CannotGetSymbol(dbRecordReaderClass.GetLocation(), "method", fieldReaderMethod.Identifier.ToString()));
				}
			}

			Dictionary<ITypeSymbol, ReadConverterMethod> readConverterMethods = new(SymbolEqualityComparer.Default);
			//Dictionary<ITypeSymbol, ReadConverterMethod> writeConverterMethods = new(SymbolEqualityComparer.Default);
			{
				foreach (var prop in dbRecordReaderClass
					.DescendantNodes()
					.OfType<PropertyDeclarationSyntax>()
					.Where(x => x.AttributeLists.Any(al => al.Attributes.Any(a => Attrib.DbConverter.Names.Contains(a.Name.ToString())))))
				{
					var sym = sm.GetDeclaredSymbol(prop);
					if (sym != null)
					{
						switch (sym.Type.TypeKind)
						{
							case TypeKind.Class:
							case TypeKind.Interface:
							case TypeKind.Struct:
								string converterTypeName = prop.Type.ToString();
								string converterPropertyName = prop.Identifier.ToString();
								foreach (var m in sym.Type.GetMembers()
									.Where(x => x.Kind == SymbolKind.Method)
									.OfType<IMethodSymbol>()
									// Has to convert from 1 type to another type, both of which must be non-null
									.Where(x => x.ReturnsVoid == false
										&& x.ReturnType.NullableAnnotation == NullableAnnotation.NotAnnotated
										&& x.Parameters.Length == 1
										&& x.Parameters[0].NullableAnnotation == NullableAnnotation.NotAnnotated))
								{
									// Stash the target type, the name, and the type from which we convert it
									//  we need to get the methods with specific names. DbToDotNet and DotNetToDb.
									if ("DbToDotNet".Equals(m.Name, StringComparison.Ordinal))
									{
										var fromType = m.Parameters[0].Type;
										if (builtInReadMethods.TryGetValue(fromType, out string? readMethodName))
										{
											readConverterMethods.Add(m.ReturnType, new(m.Name, converterTypeName, converterPropertyName, readMethodName, fromType, m.ReturnType));
										}
										else
										{
											// TODO invalid from type; it's not found on a data reader...or we can just try GetField<T>?
										}
									}
									//else if ("DotNetToDb".Equals(m.Name, StringComparison.Ordinal))
									//{
									//
									//}
								}
								break;
							default:
								context.ReportDiagnostic(Diag.ConverterWrongTypeKind(prop.GetLocation()));
								break;
						}
					}
					else
					{
						context.ReportDiagnostic(Diag.CannotGetSymbol(dbRecordReaderClass.GetLocation(), "property", prop.Identifier.ToString()));
					}
				}
			}

			List<StringBuilder> methods = [];
			Decl dbRecordReaderClassDecl = new(dbRecordReaderClass.Identifier, dbRecordReaderClass.Span, dbRecordReaderClass.SyntaxTree);
			foreach (ClassDeclarationSyntax cls in context.Compilation.SyntaxTrees
				.SelectMany(x => x.GetRoot(ct).DescendantNodes())
				.OfType<ClassDeclarationSyntax>()
				.Where(x => x.AttributeLists.Any(al => al.Attributes.Any(a => Attrib.DbRecord.Names.Contains(a.Name.ToString())))))
			{
				var ctors = cls.DescendantNodes().OfType<ConstructorDeclarationSyntax>().ToList();
				if (ctors.Count == 1)
				{
					ConstructorDeclarationSyntax ctor = ctors[0];
					SemanticModel semanticModel = context.Compilation.GetSemanticModel(cls.SyntaxTree);
					ISymbol? symDtoClass = semanticModel.GetDeclaredSymbol(cls);
					if (symDtoClass != null)
					{
						Decl d = new(cls.Identifier, cls.Span, cls.SyntaxTree);
						AddMethods("class", methods, allReadMethods, builtInReadMethods, readConverterMethods, types, d, dbRecordReaderClassDecl, semanticModel, symDtoClass, ctor.ParameterList, context, ct);
					}
					else
					{
						context.ReportDiagnostic(Diag.CannotGetSymbol(dbRecordReaderClass.GetLocation(), "class", cls.Identifier.ToString()));
					}
				}
				else
				{
					context.ReportDiagnostic(Diag.OnlyOneCtorAllowed(cls.GetLocation(), cls.Identifier.ToString(), "class"));
				}
			}
			foreach (StructDeclarationSyntax struc in context.Compilation.SyntaxTrees
				.SelectMany(x => x.GetRoot(ct).DescendantNodes())
				.OfType<StructDeclarationSyntax>()
				.Where(x => x.AttributeLists.Any(al => al.Attributes.Any(a => Attrib.DbRecord.Names.Contains(a.Name.ToString())))))
			{
				var ctors = struc.DescendantNodes().OfType<ConstructorDeclarationSyntax>().ToList();
				if (ctors.Count == 1)
				{
					ConstructorDeclarationSyntax ctor = ctors[0];
					SemanticModel semanticModel = context.Compilation.GetSemanticModel(struc.SyntaxTree);
					ISymbol? symDtoClass = semanticModel.GetDeclaredSymbol(struc);
					if (symDtoClass != null)
					{
						Decl d = new(struc.Identifier, struc.Span, struc.SyntaxTree);
						AddMethods("struct", methods, allReadMethods, builtInReadMethods, readConverterMethods, types, d, dbRecordReaderClassDecl, semanticModel, symDtoClass, ctor.ParameterList, context, ct);
					}
					else
					{
						context.ReportDiagnostic(Diag.CannotGetSymbol(dbRecordReaderClass.GetLocation(), "struct", struc.Identifier.ToString()));
					}
				}
				else
				{
					context.ReportDiagnostic(Diag.OnlyOneCtorAllowed(struc.GetLocation(), struc.Identifier.ToString(), "struct"));
				}
			}

			// Now we get all of the records decorated with the attribute indicating that they're DTOs
			foreach (RecordDeclarationSyntax rec in context.Compilation.SyntaxTrees
				.SelectMany(x => x.GetRoot(ct).DescendantNodes())
				.OfType<RecordDeclarationSyntax>()
				.Where(x => x.AttributeLists.Any(al => al.Attributes.Any(a => Attrib.DbRecord.Names.Contains(a.Name.ToString())))))
			{
				SemanticModel semanticModel = context.Compilation.GetSemanticModel(rec.SyntaxTree);
				ISymbol? symDtoRec = semanticModel.GetDeclaredSymbol(rec);
				if (symDtoRec != null)
				{
					Decl d = new(rec.Identifier, rec.Span, rec.SyntaxTree);
					AddMethods("record", methods, allReadMethods, builtInReadMethods, readConverterMethods, types, d, dbRecordReaderClassDecl, semanticModel, symDtoRec, rec.ParameterList, context, ct);
				}
				else
				{
					context.ReportDiagnostic(Diag.CannotGetSymbol(dbRecordReaderClass.GetLocation(), "record", rec.Identifier.ToString()));
				}
			}

			StringBuilder sb = new();
			sb.Append(sbTargetClassStart);
			foreach (StringBuilder m in methods)
			{
				sb.Append(m);
			}
			sb.Append(sbTargetClassEnd);
			string source = sb.ToString();
			context.AddSource(dbRecordReaderClass.Identifier.ToString() + ".g.cs", SourceText.From(source, Encoding.UTF8));
		}
		//IEnumerable<ClassDeclarationSyntax> targetParamClasses = context.Compilation.SyntaxTrees
		//	.SelectMany(x => x.GetRoot(ct).DescendantNodes())
		//	.OfType<ClassDeclarationSyntax>()
		//	.Where(x => x.AttributeLists.Any(al => al.Attributes.Any(a => DbParams.Names.Contains(a.Name.ToString()))));
		//foreach (var targetClass in targetParamClasses)
		//{
		//	if (targetClass == null) { continue; }
		//	if (!targetClass.Modifiers.Any(x => x.Text == "partial"))
		//	{
		//		context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor(id: Error.ClassNotPartial, title: "Class is not partial",
		//			messageFormat: "Class {0} must be declared as partial to have code generated for it.",
		//			DiagCat.Usage, DiagnosticSeverity.Error, isEnabledByDefault: true), targetClass.GetLocation(), targetClass.Identifier.ToString()));
		//		continue;
		//	}
		//	SemanticModel smTargetClass = context.Compilation.GetSemanticModel(targetClass.SyntaxTree);
		//	ISymbol? symTargetClass = smTargetClass.GetDeclaredSymbol(targetClass);
		//	if (symTargetClass == null)
		//	{
		//		context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor(id: Error.CannotGetSymbol, title: "Could not get class symbol",
		//			messageFormat: "Could not get symbol for class {0}",
		//			DiagCat.Usage, DiagnosticSeverity.Error, isEnabledByDefault: true), targetClass.GetLocation(), targetClass.Identifier.ToString()));
		//		continue;
		//	}
		//	if (symTargetClass.ContainingNamespace == null)
		//	{
		//		context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor(id: Error.ReaderClassMissingNamespace, title: "Reader class missing namespace",
		//			messageFormat: "Class {0} must be declared in a namespace.",
		//			DiagCat.Usage, DiagnosticSeverity.Error, isEnabledByDefault: true), targetClass.GetLocation(), targetClass.Identifier.ToString()));
		//		continue;
		//	}
		//
		//	// The issue that we have here is that the parameters may also be a type that we need to convert.
		//	// The converter should just be an interface, really.
		//	// The other problem is that the parameters can't implement a consistent interface if they have to take varying amounts of converters.
		//	// Actually, the best method of doing it, is probably having a custom interface/abstract class that works as a converter to/from types.
		//	// That way, all we need to do is pass that type along, and invoke its get method.
		//
		//	//var properties = targetClass.DescendantNodes().OfType<PropertyDeclarationSyntax>().ToList();
		//
		//	StringBuilder sb = new();
		//
		//	string source = sb.ToString();
		//	context.AddSource(targetClass.Identifier.ToString() + ".g.cs", SourceText.From(source, Encoding.UTF8));
		//}
	}
	public void AddMethods(
		string classStructOrRecord,
		List<StringBuilder> methods,
		Dictionary<ITypeSymbol, ReadMethod> allReadMethods,
		Dictionary<ITypeSymbol, string> builtInReadMethods,
		Dictionary<ITypeSymbol, ReadConverterMethod> readConverterMethods,
		Types types,
		Decl recordDecl,
		Decl dbRecordReaderClass,
		SemanticModel semanticModel,
		ISymbol symDtoClass,
		ParameterListSyntax? ParameterList,
		GeneratorExecutionContext context,
		CancellationToken ct)
	{
		if (ParameterList == null || ParameterList.Parameters.Count == 0)
		{
			context.ReportDiagnostic(Diag.MissingParameters(recordDecl.GetLocation(), classStructOrRecord, recordDecl.Identifier.ToString()));
			return;
		}

		var attrib = symDtoClass.GetAttributes()
			.Where(x => x.AttributeClass != null && Attrib.DbRecord.Names.Contains(x.AttributeClass.Name))
			.FirstOrDefault();
		int readBy = 0;
		if (attrib != null)
		{
			ImmutableArray<TypedConstant> attribArgs = attrib.ConstructorArguments;
			if (attribArgs.Length > 0)
			{
				object? o = attribArgs[0].Value;
				if (!(o is null) && o is int rb)
				{
					readBy = rb;
				}
			}
		}

		// TODO What we can do to make things a bit better is have public const int fields and directly use those instead of having a method invocation 

		List<ParameterInfo> dtoClassParams = new(ParameterList.Parameters.Count);
		int i = 0;
		foreach (ParameterSyntax p in ParameterList.Parameters)
		{
			IParameterSymbol? psym = semanticModel.GetDeclaredSymbol(p, ct);
			if (psym != null)
			{
				var pattribs = psym.GetAttributes();
				var pattribName = pattribs.Where(x => x.AttributeClass != null && Attrib.HasName.Names.Contains(x.AttributeClass.Name)).FirstOrDefault();
				var pattribIndex = pattribs.Where(x => x.AttributeClass != null && Attrib.HasOrdinal.Names.Contains(x.AttributeClass.Name)).FirstOrDefault();

				switch (readBy)
				{
					default:
					case 0: // Default
					case 1: // Name
						{
							string? dbName = psym.Name;
							if (pattribIndex != null)
							{
								context.ReportDiagnostic(Diag.WrongAttributeDecorated(p.GetLocation(), p.Identifier.ToString(), classStructOrRecord));
							}
							if (pattribName != null)
							{
								dbName = null;
								ImmutableArray<TypedConstant> pattribArgs = pattribName.ConstructorArguments;
								if (pattribArgs.Length > 0)
								{
									object? o = pattribArgs[0].Value;
									if (!(o is null) && o is string str)
									{
										dbName = str.Replace("\"", "\\\"");
									}
								}
							}
							if (dbName == null)
							{
								context.ReportDiagnostic(Diag.MissingName(p.GetLocation(), p.Identifier.ToString()));
							}
							dtoClassParams.Add(new(psym.Name, dbName, -1, psym.Type, p));
						}
						break;
					case 2: // Index
						{
							int index = i;
							if (pattribName != null)
							{
								context.ReportDiagnostic(Diag.WrongAttributeDecorated(p.GetLocation(), p.Identifier.ToString(), classStructOrRecord));
							}
							if (pattribIndex != null)
							{
								index = int.MinValue;
								ImmutableArray<TypedConstant> pattribArgs = pattribIndex.ConstructorArguments;
								if (pattribArgs.Length > 0)
								{
									object? o = pattribArgs[0].Value;
									if (!(o is null) && o is int attribIndex)
									{
										index = attribIndex;
										i = attribIndex;
									}
								}
							}
							if (index < 0)
							{
								context.ReportDiagnostic(Diag.MissingIndex(p.GetLocation(), p.Identifier.ToString()));
							}
							dtoClassParams.Add(new(psym.Name, null, index, psym.Type, p));
						}
						break;
				}
			}
			else
			{
				context.ReportDiagnostic(Diag.CannotGetSymbol(dbRecordReaderClass.GetLocation(), "parameter", p.Identifier.ToString()));
			}
			++i;
		}
		if (dtoClassParams.Count != ParameterList.Parameters.Count) return;

		Indent indent = new(1, '\t', 2);

		StringBuilder methodGetOrdinals = new();
		methods.Add(methodGetOrdinals);
		methodGetOrdinals.Append(indent).Append("public void Get").Append(symDtoClass.Name).Append("Ordinals(").Append(types.GetOrdinals).Append(" reader");
		WriteOutIntOrdinals(methodGetOrdinals, dtoClassParams);
		methodGetOrdinals.Append(")\n");
		methodGetOrdinals.Append(indent).Append("{\n");
		indent.In();
		foreach (var parameter in dtoClassParams)
		{
			methodGetOrdinals.Append(indent).Append("o").Append(parameter.CodeName);
			if (parameter.DbName != null)
			{
				methodGetOrdinals.Append(" = reader.GetOrdinal(\"").Append(parameter.DbName).Append("\");\n");
			}
			else
			{
				methodGetOrdinals.Append(" = ").Append(parameter.Index.ToString(CultureInfo.InvariantCulture)).Append(";\n");
			}
		}
		indent.Out();
		methodGetOrdinals.Append(indent).Append("}\n");


		string fullClassName = FullyQualifiedName(symDtoClass);
		StringBuilder methodReadAll = new();
		StringBuilder methodReadAllAsync = new();
		methods.Add(methodReadAll);
		methods.Add(methodReadAllAsync);
		methodReadAll.Append(indent).Append("public IEnumerable<").Append(fullClassName).Append("> ReadAll").Append(symDtoClass.Name).Append("(").Append(types.ReadAll).Append(" reader)\n");
		methodReadAllAsync.Append(indent).Append("public async IAsyncEnumerable<").Append(fullClassName).Append("> ReadAll").Append(symDtoClass.Name).Append("Async(").Append(types.ReadAllAsync).Append(" reader)\n");

		methodReadAll.Append(indent).Append("{\n");
		methodReadAllAsync.Append(indent).Append("{\n");
		indent.In();

		methodReadAll.Append(indent).Append("Get").Append(symDtoClass.Name).Append("Ordinals(reader");
		methodReadAllAsync.Append(indent).Append("Get").Append(symDtoClass.Name).Append("Ordinals(reader");

		WriteOutIntOrdinals(methodReadAll, dtoClassParams);
		WriteOutIntOrdinals(methodReadAllAsync, dtoClassParams);

		methodReadAll.Append(");\n");
		methodReadAllAsync.Append(");\n");

		methodReadAll.Append(indent).Append("while (reader.Read())\n").Append(indent).Append("{\n");
		methodReadAllAsync.Append(indent).Append("while (await reader.ReadAsync())\n").Append(indent).Append("{\n");
		indent.In();

		methodReadAll.Append(indent).Append("yield return Read").Append(symDtoClass.Name).Append("(reader");
		methodReadAllAsync.Append(indent).Append("yield return Read").Append(symDtoClass.Name).Append("(reader");

		WriteOrdinals(methodReadAll, dtoClassParams);
		WriteOrdinals(methodReadAllAsync, dtoClassParams);

		methodReadAll.Append(");\n");
		methodReadAllAsync.Append(");\n");
		indent.Out();

		methodReadAll.Append(indent).Append("}\n");
		methodReadAllAsync.Append(indent).Append("}\n");
		indent.Out();

		methodReadAll.Append(indent).Append("}\n");
		methodReadAllAsync.Append(indent).Append("}\n");

		StringBuilder methodReadFirstOrDefault = new();
		methodReadFirstOrDefault.Append(indent).Append("public ").Append(fullClassName).Append("? ReadFirstOrDefault").Append(symDtoClass.Name).Append("(").Append(types.ReadFirstOrDefault).Append(" reader)\n");
		methodReadFirstOrDefault.Append(indent).Append("{\n");
		methodReadFirstOrDefault.Append(indent.In()).Append("Get").Append(symDtoClass.Name).Append("Ordinals(reader");
		WriteOutIntOrdinals(methodReadFirstOrDefault, dtoClassParams);
		methodReadFirstOrDefault.Append(");\n");
		methodReadFirstOrDefault.Append(indent).Append("return reader.Read() ? Read").Append(symDtoClass.Name).Append("(reader");
		WriteOrdinals(methodReadFirstOrDefault, dtoClassParams);
		methodReadFirstOrDefault.Append(") : null;\n");
		methodReadFirstOrDefault.Append(indent.Out()).Append("}\n");
		methods.Add(methodReadFirstOrDefault);

		StringBuilder methodReadFirstOrDefaultAsync = new();
		methodReadFirstOrDefaultAsync.Append(indent).Append("public async Task<").Append(fullClassName).Append("?> ReadFirstOrDefault").Append(symDtoClass.Name).Append("Async(").Append(types.ReadFirstOrDefaultAsync).Append(" reader)\n");
		methodReadFirstOrDefaultAsync.Append(indent).Append("{\n");
		methodReadFirstOrDefaultAsync.Append(indent.In()).Append("Get").Append(symDtoClass.Name).Append("Ordinals(reader");
		WriteOutIntOrdinals(methodReadFirstOrDefaultAsync, dtoClassParams);
		methodReadFirstOrDefaultAsync.Append(");\n");
		methodReadFirstOrDefaultAsync.Append(indent).Append("return await reader.ReadAsync() ? Read").Append(symDtoClass.Name).Append("(reader");
		WriteOrdinals(methodReadFirstOrDefaultAsync, dtoClassParams);
		methodReadFirstOrDefaultAsync.Append(") : null;\n");
		methodReadFirstOrDefaultAsync.Append(indent.Out()).Append("}\n");
		methods.Add(methodReadFirstOrDefaultAsync);

		StringBuilder methodRead = new(indent);
		methods.Add(methodRead);

		methodRead.Append("public ").Append(fullClassName).Append(" Read").Append(symDtoClass.Name).Append("(");
		methodRead.Append(types.Read).Append(" reader");

		WriteIntOrdinals(methodRead, dtoClassParams);

		methodRead.Append(")\n");
		methodRead.Append(indent);
		methodRead.Append("{\n");
		methodRead.Append(indent.In()).Append("return new ").Append(fullClassName).Append('\n').Append(indent).Append("(");
		indent.In();
		foreach (var parameter in dtoClassParams)
		{
			methodRead.Append('\n').Append(indent);
			ITypeSymbol pType = parameter.Type;

			ReadConverterMethod? converterMethod = null;
			string? builtInReadMethod = null;
			ReadMethod? dataReaderMethod = null;
			string cast = "";
			bool mayBeNull = pType.NullableAnnotation == NullableAnnotation.Annotated;

			// If the type is Nullable<T>, then we need to get its generic type
			if (pType.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T && pType is INamedTypeSymbol nts1 && nts1.TypeArguments.Length == 1)
			{
				mayBeNull = true;
				pType = nts1.TypeArguments[0];
			}

			// If there's a user-defined method for this type, then use that
			// We don't check enums YET, because the user may have a specific parser for their enum
			if (readConverterMethods.Count > 0)
			{
				if (readConverterMethods.TryGetValue(pType, out converterMethod)) { }
				else
				{
					if (builtInReadMethods.TryGetValue(pType, out builtInReadMethod)) { }
					else if (pType.TypeKind == TypeKind.Enum && pType is INamedTypeSymbol nts2 && nts2.EnumUnderlyingType != null)
					{
						ITypeSymbol? pEnumType = pType;
						cast = string.Concat("(", FullyQualifiedName(pEnumType), mayBeNull ? "?)" : ")");
						if (readConverterMethods.TryGetValue(nts2.EnumUnderlyingType, out converterMethod)) { }
						else if (builtInReadMethods.TryGetValue(nts2.EnumUnderlyingType, out builtInReadMethod)) { }
						else
						{
							// If we can't find anything, issue an error
							context.ReportDiagnostic(Diag.MissingConverterMethod(parameter.Syntax.GetLocation(), pEnumType.Name, recordDecl.Identifier.ToString(), dbRecordReaderClass.Identifier.ToString()));
						}
					}
					else
					{
						context.ReportDiagnostic(Diag.MissingConverterMethod(parameter.Syntax.GetLocation(), pType.Name, recordDecl.Identifier.ToString(), dbRecordReaderClass.Identifier.ToString()));
					}
				}
			}
			else
			{
				// If there's a user-defined method for this type, then use that
				// We don't check enums YET, because the user may have a specific parser for their enum
				if (allReadMethods.TryGetValue(pType, out dataReaderMethod)) { }
				else
				{
					// If we have an enum, then check the underlying type and try to get a read method for it
					// If we can't get anything then just give up
					if (pType.TypeKind == TypeKind.Enum && pType is INamedTypeSymbol nts2 && nts2.EnumUnderlyingType != null)
					{
						ITypeSymbol? pEnumType = pType;
						cast = string.Concat("(", FullyQualifiedName(pEnumType), mayBeNull ? "?)" : ")");
						if (allReadMethods.TryGetValue(nts2.EnumUnderlyingType, out dataReaderMethod)) { }
						else
						{
							// If we can't find anything, issue an error
							context.ReportDiagnostic(Diag.MissingConverterMethod(parameter.Syntax.GetLocation(), pEnumType.Name, recordDecl.Identifier.ToString(), dbRecordReaderClass.Identifier.ToString()));
						}
					}
					else
					{
						context.ReportDiagnostic(Diag.MissingConverterMethod(parameter.Syntax.GetLocation(), pType.Name, recordDecl.Identifier.ToString(), dbRecordReaderClass.Identifier.ToString()));
					}
				}
			}

			if (mayBeNull)
			{
				methodRead.Append("reader.IsDBNull(o").Append(parameter.CodeName).Append(") ? null : ");
			}
			methodRead.Append(cast);
			if (converterMethod != null)
			{
				methodRead.Append(converterMethod.ConverterPropertyName).Append('.').Append(converterMethod.ConverterMethodName).Append('(');
				methodRead.Append("reader.").Append(converterMethod.DataReaderMethodName).Append("(o").Append(parameter.CodeName).Append("))");
			}
			else if (builtInReadMethod != null)
			{
				methodRead.Append("reader.").Append(builtInReadMethod).Append("(o").Append(parameter.CodeName).Append(')');
			}
			else if (dataReaderMethod != null)
			{
				methodRead.Append(dataReaderMethod.BuiltIn
					? string.Concat("reader.", dataReaderMethod.Name, "(o", parameter.CodeName, ")")
					: string.Concat(dataReaderMethod.Name, "(reader, o", parameter.CodeName, ")"));
			}
			else
			{
				methodRead.Append("default");
			}
			methodRead.Append(',');
		}
		// Snip off the last comma
		if (dtoClassParams.Count > 0)
		{
			methodRead.Remove(methodRead.Length - 1, 1);
		}
		methodRead.Append('\n').Append(indent.Out());
		methodRead.Append(");\n");
		methodRead.Append(indent.Out()).Append("}\n");
	}
	public static void WriteOutIntOrdinals(StringBuilder sb, List<ParameterInfo> pis)
	{
		foreach (var p in pis)
		{
			sb.Append(", out int o").Append(p.CodeName);
		}
	}
	public static void WriteIntOrdinals(StringBuilder sb, List<ParameterInfo> pis)
	{
		foreach (var p in pis)
		{
			sb.Append(", int o").Append(p.CodeName);
		}
	}
	public static void WriteOrdinals(StringBuilder sb, List<ParameterInfo> pis)
	{
		foreach (var p in pis)
		{
			sb.Append(", o").Append(p.CodeName);
		}
	}
}