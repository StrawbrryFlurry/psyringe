using System.Management.Automation.Language;
using System.Reflection;

namespace PSyringe.Language.AstTransformation.SyntheticAsts;

/// <summary>
///   A custom ScriptExtent whose Text property shows the
///   whole script text, unlike the public `ScriptExtent` of
///   the PowerShell sdk that is restricted to one line.
///   Use the "real" script positions of the extent gathered
///   from the ast to init this extent such that error messages
///   match up with the expected line numbers in the users script.
///   TODO:
///   Waiting for issue https://github.com/PowerShell/PowerShell/issues/16408 to resolve
/// </summary>
public class SyntheticScriptExtent {
  public static readonly IScriptExtent EmptyScriptExtent = new ScriptExtent(null, null);
  internal static readonly IScriptPosition EmptyScriptPosition = new ScriptPosition(null, 0, 0, "0");
  internal static readonly FieldInfo AstExtentBackingField = GetExtentBackingField();

  public string? File => null;

  /// <summary>
  ///   Compiled script text that is going to be executed
  ///   by the PowerShell engine.
  /// </summary>
  public string Text { get; }

  /// <summary>
  ///   The EndScriptPosition of the actual script extent.
  /// </summary>
  public IScriptPosition EndScriptPosition { get; }

  public int EndColumnNumber => EndScriptPosition.ColumnNumber;
  public int EndLineNumber => EndScriptPosition.LineNumber;
  public int EndOffset => EndScriptPosition.Offset;

  /// <summary>
  ///   The StartScriptPosition of the actual script extent.
  /// </summary>
  public IScriptPosition StartScriptPosition { get; }

  public int StartColumnNumber => StartScriptPosition.ColumnNumber;
  public int StartLineNumber => StartScriptPosition.LineNumber;
  public int StartOffset => StartScriptPosition.Offset;

  public SyntheticScriptExtent(string text, IScriptExtent? replacementExtent) {
    Text = text;
    StartScriptPosition = replacementExtent?.StartScriptPosition ?? EmptyScriptPosition;
    EndScriptPosition = replacementExtent?.EndScriptPosition ?? EmptyScriptPosition;
  }

  /// <summary>
  ///   Updates the extent of the AST to be compliant
  ///   with it's string representation.
  /// </summary>
  /// <param name="ast"></param>
  /// <returns></returns>
  public static T UpdateScriptExtent<T>(T ast) where T : Ast {
    // TODO: var replacementExtent = new SyntheticScriptExtent(ast.ToStringFromAst(), ast.Extent);
    var replacementExtent = new ScriptExtent(null, null);
    AstExtentBackingField.SetValue(ast, replacementExtent);
    return ast;
  }

  /// <summary>
  ///   `Ast.Extent` does not have a setter so in order to replace
  ///   it's value we need to get the backing field of the property
  ///   and set that.
  /// </summary>
  private static FieldInfo GetExtentBackingField() {
    var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
    var propertyName = nameof(Ast.Extent);
    var backingFieldName = $"<{propertyName}>k__BackingField";

    return typeof(Ast).GetField(backingFieldName, bindingFlags)!;
  }
}