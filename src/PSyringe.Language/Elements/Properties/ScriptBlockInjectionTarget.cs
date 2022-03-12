using System.Management.Automation.Language;

namespace PSyringe.Language.Elements.Properties; 

public class ScriptBlockInjectionTarget {
  private ScriptBlockExpressionAst? GetAttributedScriptBlockExpression() {
    return default;
    // return "ChildExpression" as ScriptBlockExpressionAst;
  }
}