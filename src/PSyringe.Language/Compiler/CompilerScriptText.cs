namespace PSyringe.Language.Compiler;

public class CompilerScriptText {
  public const string TrueVariable = "$true";

  /// <summary>
  ///   Comment inserted inline before a compiler generated variable declaration.
  ///   <code>
  ///  $SomeVariable = <![CDATA[ <# COMPILER GENERATED VARIABLE #> ]]> $ɵɵvar;
  /// </code>
  /// </summary>
  public const string GeneratedVariableInline = "<# COMPILER GENERATED VARIABLE #>";

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
  public const string GeneratedScriptBlockOpen = "# COMPILER GENERATED CODE {";

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
  public const string GeneratedScriptBlockClose = "# }";
}