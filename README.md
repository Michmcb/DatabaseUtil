# DatabaseUtil
Has a bunch of useful utilities to make working with databases easier. Works best when used with the source generator, DatabaseUtil.SourceGenerator.


# DatabaseUtil.SourceGenerator
To use this, first create a partial class, and decorate it with the `[DbRecordReader]` attribute. This class will be the class that gets methods generated on it.

Then, decorate classes or structs with `[DbRecord]`. For every class or struct you decorate, methods will be generated on the class decorated with `[DbRecordReader]`. These methods get column ordinals, read a single row, read the first or default row, or read an enumerable of rows.

Ideally, decorate records. If not, ensure that the class or struct has only one constructor. Otherwise, you will get an error.


If you have any custom types in your classes/structs decorated with `[DbRecord]`, then all you need to do is create a property of type `ISqlConverter<TDb, TNet>` on the class `[DbRecordReader]` and decorate that method with `[DbConverter]`.

You can also override built-in type parsing by doing the same as the above. Enums are read as their underlying integer type.

By default, ordinals are obtained by finding fields that match the parameter names in code. You can change this by using the `[HasName]` attribute and supplying a custom name. Or, if you want to provide explicit ordinals, ignoring column names entirely, use `[HasOrdinal]` instead. Note that you need to decorate with `[DbRecord(ReadBy.Ordinal)]` in order to do this.


As for parameters, can decorate classes, structs, or records with `[DbParams]` to have an implementation of `IDbParamsApplicator<T>` generated on the class you decorated with `[DbRecordReader]` (though the target class will probably change in the future).

By default, the parameter names are the same as the property names. You can change this by decorating it with the `[HasName]` attribute.