using System.Management.Automation;
using System.Management.Automation.Language;

namespace PSyringe.Core.Language.Parsing.Elements;

public class InjectionVariableElement {
  // [Fact]
  // public void AddInjectVariable_CreatesInjectVariable_WhenEncountered() {
  //   // Expression => [Attribute] ([Inject()]), [Child] => [Attribute] ([ILogger])
  //   var sut = MakeVisitorAndVisitScript(ScriptTemplates.WithInjectVariableExpression_ImplicitTarget);
  // }

  /// <summary>
  ///   AttributedExpressionChild is either a VariableExpressionAst
  ///   or a VariableAssignmentExpression
  /// </summary>
  /// <param name="ast"></param>
  public InjectionVariableElement(AttributedExpressionAst ast) {
  }

  public PSObject? DefaultValue { get; set; }
}