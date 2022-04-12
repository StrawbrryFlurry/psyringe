using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class InjectElement : ScriptElement {
  /// <summary>
  ///   AttributedExpressionChild is either a VariableExpressionAst
  ///   or a VariableAssignmentExpression
  /// </summary>
  /// <param name="ast"></param>
  public InjectElement(Ast ast) : base(ast) {
  }

  public bool HasDefaultValue() {
    return false;
  }
}