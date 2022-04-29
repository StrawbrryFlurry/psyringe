namespace PSyringe.Language.AstTransformation;

public static class CodeGenConstants {
  public const string True = "$true";
  public const string False = "$true";
  public const string Null = "$null";
  public const string NewLine = "\r\n";

  /// <summary>
  ///   Comment inserted inline before a compiler generated expression.
  ///   <code>
  ///  $SomeVariable = <![CDATA[ <# COMPILER GENERATED EXPRESSION #> ]]> $ɵɵvar;
  /// </code>
  /// </summary>
  public const string InlineExpression = "<# COMPILER GENERATED EXPRESSION #>";

  /// <summary>
  ///   Comment inserted above a compiler generated script block or code section.
  ///   <code>
  ///   # COMPILER GENERATED CODE {
  ///   if(SomeCondition) {
  ///     # ... Do something
  ///   }
  ///   # }
  ///   </code>
  /// </summary>
  public const string BlockOpen = "# COMPILER GENERATED CODE {";

  /// <summary>
  ///   Comment inserted below a compiler generated script block or code section.
  ///   <code>
  ///   # COMPILER GENERATED CODE {
  ///   if(SomeCondition) {
  ///     # ... Do something
  ///   }
  ///   # }
  ///   </code>
  /// </summary>
  public const string BlockClose = "# }";

  /// <summary>
  ///   The prefix before a compiler generated variable name.
  /// </summary>
  public const string VariablePrefix = "ɵɵ";
}