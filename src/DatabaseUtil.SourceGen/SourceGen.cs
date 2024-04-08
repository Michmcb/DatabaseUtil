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
using System.Reflection.Metadata;
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
	//public bool ImplementsInterface(string interfaceName, ITypeSymbol sym)
	//{
	//	return interfaceName == FullyQualifiedName(sym) || sym.Interfaces.Any(nts => ImplementsInterface(interfaceName, nts));
	//}
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

			string targetClassOpenBrace = "\n\t{\n";
			List<string> targetClassInterfaces = [];

			StringBuilder sbTargetClassEnd = new("\t}\n}\n#nullable restore");

			Dictionary<ITypeSymbol, ReadConverterMethod> readConverterMethods = new(SymbolEqualityComparer.Default);
			Dictionary<ITypeSymbol, WriteConverterMethod> writeConverterMethods = new(SymbolEqualityComparer.Default);
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
									else if ("DotNetToDb".Equals(m.Name, StringComparison.Ordinal))
									{
										// This one is super simple. There's nothing special we need to do, since it'll just be converted to object in the end.
										var fromType = m.Parameters[0].Type;
										writeConverterMethods.Add(fromType, new(m.Name, converterTypeName, converterPropertyName, m.ReturnType, fromType));
									}
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

			GeneratedClassAndStructDbRecordReadMethods("class", methods, builtInReadMethods, readConverterMethods, types, dbRecordReaderClassDecl, context, context.Compilation.SyntaxTrees
				.SelectMany(x => x.GetRoot(ct).DescendantNodes())
				.OfType<ClassDeclarationSyntax>()
				.Where(x => x.AttributeLists.Any(al => al.Attributes.Any(a => Attrib.DbRecord.Names.Contains(a.Name.ToString())))), ct);

			GeneratedClassAndStructDbRecordReadMethods("struct", methods, builtInReadMethods, readConverterMethods, types, dbRecordReaderClassDecl, context, context.Compilation.SyntaxTrees
				.SelectMany(x => x.GetRoot(ct).DescendantNodes())
				.OfType<StructDeclarationSyntax>()
				.Where(x => x.AttributeLists.Any(al => al.Attributes.Any(a => Attrib.DbRecord.Names.Contains(a.Name.ToString())))), ct);

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
					GenerateDbRecordReadMethods("record", methods, builtInReadMethods, readConverterMethods, types, d, dbRecordReaderClassDecl, semanticModel, symDtoRec, rec.ParameterList, context, ct);
				}
				else
				{
					context.ReportDiagnostic(Diag.CannotGetSymbol(dbRecordReaderClass.GetLocation(), "record", rec.Identifier.ToString()));
				}
			}

			// Now do the parameter applicators
			GenerateClassAndStructParamMethods("class", methods, targetClassInterfaces, writeConverterMethods, dbRecordReaderClass, context, context.Compilation.SyntaxTrees
				.SelectMany(x => x.GetRoot(ct).DescendantNodes())
				.OfType<ClassDeclarationSyntax>()
				.Where(x => x.AttributeLists.Any(al => al.Attributes.Any(a => Attrib.DbParams.Names.Contains(a.Name.ToString())))));

			GenerateClassAndStructParamMethods("struct", methods, targetClassInterfaces, writeConverterMethods, dbRecordReaderClass, context, context.Compilation.SyntaxTrees
				.SelectMany(x => x.GetRoot(ct).DescendantNodes())
				.OfType<StructDeclarationSyntax>()
				.Where(x => x.AttributeLists.Any(al => al.Attributes.Any(a => Attrib.DbParams.Names.Contains(a.Name.ToString())))));

			foreach (RecordDeclarationSyntax rec in context.Compilation.SyntaxTrees
				.SelectMany(x => x.GetRoot(ct).DescendantNodes())
				.OfType<RecordDeclarationSyntax>()
				.Where(x => x.AttributeLists.Any(al => al.Attributes.Any(a => Attrib.DbParams.Names.Contains(a.Name.ToString())))))
			{
				SemanticModel semanticModel = context.Compilation.GetSemanticModel(rec.SyntaxTree);
				ISymbol? symDtoRec = semanticModel.GetDeclaredSymbol(rec);
				if (symDtoRec != null)
				{
					if (rec.ParameterList != null && rec.ParameterList.Parameters.Count != 0)
					{
						List<NamedTypeSymbol> parameters = new(rec.ParameterList.Parameters.Count);
						foreach (var p in rec.ParameterList.Parameters)
						{
							var psym = semanticModel.GetDeclaredSymbol(p);
							if (psym != null)
							{
								parameters.Add(new(p.Identifier.ToString(), psym.Type));
							}
							else
							{
								context.ReportDiagnostic(Diag.CannotGetSymbol(p.GetLocation(), "property", p.Identifier.ToString()));
							}
						}
						Decl d = new(rec.Identifier, rec.Span, rec.SyntaxTree);
						GenerateParamMethods(methods, targetClassInterfaces, writeConverterMethods, symDtoRec, new(parameters));
					}
					else
					{
						context.ReportDiagnostic(Diag.MissingDbParamProperties(dbRecordReaderClass.GetLocation(), "record", rec.Identifier.ToString()));
					}
				}
				else
				{
					context.ReportDiagnostic(Diag.CannotGetSymbol(dbRecordReaderClass.GetLocation(), "record", rec.Identifier.ToString()));
				}
			}

			StringBuilder sb = new();
			sb.Append(sbTargetClassStart);

			if (targetClassInterfaces.Count > 0)
			{
				sb.Append(" :\n");
				foreach (var interfaceImpl in targetClassInterfaces)
				{
					sb.Append("\t\t").Append(interfaceImpl).Append(",\n");
				}
				// Last interface implementation cannot have a trailing comma, so get rid of it
				sb.Length -= 2;
			}

			sb.Append(targetClassOpenBrace);
			foreach (StringBuilder m in methods)
			{
				sb.Append(m);
			}
			sb.Append(sbTargetClassEnd);
			string source = sb.ToString();
			context.AddSource(dbRecordReaderClass.Identifier.ToString() + ".g.cs", SourceText.From(source, Encoding.UTF8));
		}
	}
	public void GeneratedClassAndStructDbRecordReadMethods(
		string classOrStruct,
		List<StringBuilder> methods,
		Dictionary<ITypeSymbol, string> builtInReadMethods,
		Dictionary<ITypeSymbol, ReadConverterMethod> readConverterMethods,
		Types types,
		Decl dbRecordReaderClassDecl,
		GeneratorExecutionContext context,
		IEnumerable<TypeDeclarationSyntax> objs,
		CancellationToken ct)
	{
		foreach (var obj in objs)
		{
			var ctors = obj.DescendantNodes().OfType<ConstructorDeclarationSyntax>().ToList();
			if (ctors.Count == 1)
			{
				ConstructorDeclarationSyntax ctor = ctors[0];
				SemanticModel semanticModel = context.Compilation.GetSemanticModel(obj.SyntaxTree);
				ISymbol? symDtoClass = semanticModel.GetDeclaredSymbol(obj);
				if (symDtoClass != null)
				{
					Decl d = new(obj.Identifier, obj.Span, obj.SyntaxTree);
					GenerateDbRecordReadMethods(classOrStruct, methods, builtInReadMethods, readConverterMethods, types, d, dbRecordReaderClassDecl, semanticModel, symDtoClass, ctor.ParameterList, context, ct);
				}
				else
				{
					context.ReportDiagnostic(Diag.CannotGetSymbol(dbRecordReaderClassDecl.GetLocation(), classOrStruct, obj.Identifier.ToString()));
				}
			}
			else
			{
				context.ReportDiagnostic(Diag.OnlyOneCtorAllowed(obj.GetLocation(), obj.Identifier.ToString(), classOrStruct));
			}
		}
	}
	public void GenerateClassAndStructParamMethods(
		string classOrStruct,
		List<StringBuilder> methods,
		List<string> targetClassInterfaces,
		Dictionary<ITypeSymbol, WriteConverterMethod> writeConverterMethods,
		ClassDeclarationSyntax dbRecordReaderClass,
		GeneratorExecutionContext context,
		IEnumerable<TypeDeclarationSyntax> objs)
	{
		foreach (var obj in objs)
		{
			SemanticModel semanticModel = context.Compilation.GetSemanticModel(obj.SyntaxTree);
			ISymbol? symDtoClass = semanticModel.GetDeclaredSymbol(obj);
			if (symDtoClass != null)
			{
				List<NamedTypeSymbol> parameters = [];
				foreach (var prop in obj
					.DescendantNodes()
					.OfType<PropertyDeclarationSyntax>())
				{
					var psym = semanticModel.GetDeclaredSymbol(prop);
					if (psym != null)
					{
						parameters.Add(new(prop.Identifier.ToString(), psym.Type));
					}
					else
					{
						context.ReportDiagnostic(Diag.CannotGetSymbol(prop.GetLocation(), "property", prop.Identifier.ToString()));
					}
				}
				Decl d = new(obj.Identifier, obj.Span, obj.SyntaxTree);
				GenerateParamMethods(methods, targetClassInterfaces, writeConverterMethods, symDtoClass, parameters);
			}
			else
			{
				context.ReportDiagnostic(Diag.CannotGetSymbol(dbRecordReaderClass.GetLocation(), classOrStruct, obj.Identifier.ToString()));
			}
		}
	}
	public void GenerateParamMethods(
		List<StringBuilder> methods,
		List<string> interfaceImpls,
		Dictionary<ITypeSymbol, WriteConverterMethod> writeConverterMethods,
		ISymbol symDtoClass,
		List<NamedTypeSymbol> properties)
	{
		if (properties.Count == 0)
		{
			return;
		}
		StringBuilder sb = new();
		Indent indent = new(1, '\t', 2);
		methods.Add(sb);
		
		sb.Append(indent).Append("public void ApplyParameters").Append("(").Append(FullyQualifiedName(symDtoClass)).Append(" parameters, IDbCommand cmd)\n")
			.Append(indent).Append("{\n");
		indent.In();

		sb.Append(indent).Append("IDbDataParameter p;\n");
		foreach (var p in properties)
		{
			ITypeSymbol pType = p.Symbol;

			sb.Append(indent).Append("p = cmd.CreateParameter();\n");
			sb.Append(indent).Append("p.ParameterName = \"").Append(p.Name).Append("\";\n");
			// We need the name and type of the property
			bool isNullableValue = false;
			if (p.Symbol.NullableAnnotation == NullableAnnotation.Annotated)
			{
				if (p.Symbol.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
				{
					isNullableValue = true;
					sb.Append(indent).Append("if (!parameters.").Append(p.Name).Append(".HasValue)\n").Append(indent).Append("{\n");
					if (pType is INamedTypeSymbol nts1 && nts1.TypeArguments.Length == 1)
					{
						pType = nts1.TypeArguments[0];
					}
				}
				else
				{
					sb.Append(indent).Append("if (parameters.").Append(p.Name).Append(" is null)\n").Append(indent).Append("{\n");
				}
				sb.Append(indent.In()).Append("p.Value = System.DBNull.Value;\n");
				sb.Append(indent.Out()).Append("}\n");
				sb.Append(indent).Append("else\n");
				sb.Append(indent).Append("{\n");
				indent.In();
			}
			if (writeConverterMethods.TryGetValue(pType, out WriteConverterMethod method))
			{
				sb.Append(indent).Append("p.Value = ").Append(method.ConverterPropertyName).Append('.').Append(method.ConverterMethodName);
				sb.Append("(parameters.").Append(p.Name);
				if (isNullableValue)
				{
					sb.Append(".Value");
				}
				sb.Append(");\n");
			}
			else
			{
				// No converter, so we just assign it
				sb.Append(indent).Append("p.Value = parameters.").Append(p.Name);
				if (isNullableValue)
				{
					sb.Append(".Value");
				}
				sb.Append(";\n");
			}
			if (p.Symbol.NullableAnnotation == NullableAnnotation.Annotated)
			{
				indent.Out();
				sb.Append(indent).Append("}\n");
			}
			sb.Append(indent).Append("cmd.Parameters.Add(p);\n");
		}
		sb.Append(indent.Out()).Append("}\n");
		interfaceImpls.Add(string.Concat("IDbParamsApplicator<", FullyQualifiedName(symDtoClass), ">"));
	}
	public void GenerateDbRecordReadMethods(
		string classStructOrRecord,
		List<StringBuilder> methods,
		Dictionary<ITypeSymbol, string> builtInReadMethods,
		Dictionary<ITypeSymbol, ReadConverterMethod> readConverterMethods,
		Types types,
		Decl recordDecl,
		Decl dbRecordReaderClass,
		SemanticModel semanticModel,
		ISymbol symDtoClass,
		ParameterListSyntax? parameterList,
		GeneratorExecutionContext context,
		CancellationToken ct)
	{
		if (parameterList == null || parameterList.Parameters.Count == 0)
		{
			context.ReportDiagnostic(Diag.MissingDbRecordReadParameters(recordDecl.GetLocation(), classStructOrRecord, recordDecl.Identifier.ToString()));
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

		List<ParameterInfo> dtoClassParams = new(parameterList.Parameters.Count);
		int i = 0;
		foreach (ParameterSyntax p in parameterList.Parameters)
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
		if (dtoClassParams.Count != parameterList.Parameters.Count) return;

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