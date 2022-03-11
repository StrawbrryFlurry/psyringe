using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class InjectCredentialElement : IInjectCredentialElement {
  public InjectCredentialElement(AttributedExpressionAst ast) {
    Ast = ast;
  }

  public AttributedExpressionAst Ast { get; }
}