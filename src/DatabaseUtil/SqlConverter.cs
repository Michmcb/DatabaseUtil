//namespace DatabaseUtil;

//using System;

//public class SqlConverter
//{
//	public DateOnly ToDateOnly(int value)
//	{
//		// So, when we have a value on the database we're reading, we read it as an int, then invoke this method to convert it to DateOnly
//		// And, when we are getting the value back from the parameter, we cast to an int, then call this method, and we're good.
//		return DateOnly.FromDayNumber(value);
//	}
//	public int FromDateOnly(DateOnly value)
//	{
//		// This is used for parameters; when we are passing in a DateOnly, we need to invoke this method to convert it to an int to set it on the parameter
//		return value.DayNumber;
//	}
//}
