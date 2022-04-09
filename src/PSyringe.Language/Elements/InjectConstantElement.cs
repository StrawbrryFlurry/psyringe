using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;
using PSyringe.Language.Elements.Properties;

namespace PSyringe.Language.Elements; 

public class InjectConstantElement : VariableInjectionTarget, IInjectConstantElement {
  public AttributedExpressionAst Ast { get; }

  public InjectConstantElement(AttributedExpressionAst ast) : base(ast) {
    Ast = ast;
  }
}