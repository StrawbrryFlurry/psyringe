using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;
using PSyringe.Language.Elements.Properties;

namespace PSyringe.Language.Elements;

public class InjectSecretElement : VariableInjectionTarget, IInjectSecretElement {
  public InjectSecretElement(AttributedExpressionAst ast) : base(ast) {
    Ast = ast;
  }

  public AttributedExpressionAst Ast { get; }
}