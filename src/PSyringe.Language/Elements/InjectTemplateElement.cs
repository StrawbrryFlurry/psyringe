using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class InjectTemplateElement : IInjectTemplateElement {
  public InjectTemplateElement(AttributedExpressionAst ast) {
    Ast = ast;
  }

  public AttributedExpressionAst Ast { get; }
}