namespace DatabaseUtil.SourceGen.Test
{
	using System;

	[DbRecord(ReadBy.Ordinal)]
	public sealed record class TestRecordClassHuge
	(
		[HasOrdinal(2)] bool Boolean,
		byte Byte,
		short Short,
		int Int,
		long Long,
		float Float,
		double Double,
		decimal Decimal,
		char Char,
		string String,
		DateTime DateTime,
		Guid Guid,
		object Object,
		EnumByte ByteEnum,
		EnumInt IntEnum,
		EnumShort ShortEnum,
		EnumLong LongEnum,

		bool? NullableBoolean,
		byte? NullableByte,
		short? NullableShort,
		int? NullableInt,
		long? NullableLong,
		float? NullableFloat,
		double? NullableDouble,
		decimal? NullableDecimal,
		char? NullableChar,
		string? NullableString,
		DateTime? NullableDateTime,
		Guid? NullableGuid,
		object? NullableObject,
		EnumByte? NullableByteEnum,
		EnumInt? NullableIntEnum,
		EnumShort? NullableShortEnum,
		EnumLong? NullableLongEnum
	);
}