namespace DatabaseUtil.SourceGen;

public sealed class Indent
{
	public Indent(int amt, char c, int init = 0)
	{
		Char = c;
		Amt = amt;
		Val = new string(c, init);
	}
	public char Char { get; }
	public int Amt { get; }
	public string Val { get; private set; }
	public string In()
	{
		Val += new string(Char, Amt);
		return Val;
	}
	public string Out()
	{
		Val = Val.Substring(0, Val.Length - Amt);
		return Val;
	}
	public static implicit operator string(Indent s) => s.Val;
}