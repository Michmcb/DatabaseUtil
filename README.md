# DatabaseUtil
Has a bunch of useful utilities to make working with databases easier. Works best when used with the source generator.


# DatabaseUtil.SourceGenerator
To use this, first create a partial class, and decorate it with the `[DbRecordReader]` attribute. This class will be the class that gets methods generated on it.

Then, decorate record classes or structs with `[DbRecord]`. For every record you decorate, methods will be generated on the class decorated with `[DbRecordReader]`. These methods get column ordinals, read a single row, read the first or default row, or read an enumerable of rows.

If you have any custom types in your classes/structs decorated with `[DbRecord]`, then all you need to do is create a compliant method on the class `[DbRecordReader]` and decorate that method with `[DbGetField]`. The method must return the custom type, the first parameter must be something that implements `IDataRecord`, and the second parameter must be `int`.

You can also override built-in type parsing by doing the same as the above. Enums are read as their underlying integer type.

By default, ordinals are obtained by finding fields that match the parameter names in code. You can change this by using the `[HasName]` attribute and supplying a custom name. Or, if you want to provide explicit ordinals, ignoring column names entirely, use `[HasOrdinal]` instead. Note that you need to decorate with `[DbRecord(ReadBy.Ordinal)]` in order to do this.