using System.Management.Automation;
using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class InjectVariableElement : IInjectVariableElement {
  /// <summary>
  ///   AttributedExpressionChild is either a VariableExpressionAst
  ///   or a VariableAssignmentExpression
  /// </summary>
  /// <param name="ast"></param>
  public InjectVariableElement(AttributedExpressionAst ast) {
    Ast = ast;
  }

  public PSObject? DefaultValue { get; set; }

  // [Fact]
  // public void AddInjectVariable_CreatesInjectVariable_WhenEncountered() {
  //   // Expression => [Attribute] ([Inject()]), [Child] => [Attribute] ([ILogger])
  //   var sut = MakeVisitorAndVisitScript(ScriptTemplates.WithInjectVariableExpression_ImplicitTarget);
  // }
  public Ast Ast { get; }
}