using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;
using PSyringe.Language.Elements.Properties;

namespace PSyringe.Language.Elements;

public class InjectCredentialElement : VariableInjectionTarget, IInjectCredentialElement {
  public AttributedExpressionAst Ast { get; }

  public InjectCredentialElement(AttributedExpressionAst ast) : base(ast) {
    Ast = ast;
  }
}