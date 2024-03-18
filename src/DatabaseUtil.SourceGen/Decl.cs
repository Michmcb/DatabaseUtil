namespace DatabaseUtil.SourceGen;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

public sealed class Decl
{
	public Decl(SyntaxToken identifier, TextSpan span, SyntaxTree syntaxTree)
	{
		Identifier = identifier;
		Span = span;
		SyntaxTree = syntaxTree;
	}
	public SyntaxToken Identifier { get; }
	public TextSpan Span { get; }
	public SyntaxTree SyntaxTree { get; }
	public Location GetLocation()
	{
		return Location.Create(SyntaxTree, Span);
	}
}
