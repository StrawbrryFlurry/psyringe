using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;
using PSyringe.Language.Elements.Properties;

namespace PSyringe.Language.Elements;

public class InjectDatabaseElement : VariableInjectionTarget, IInjectDatabaseElement {
  public AttributedExpressionAst Ast { get; }

  public InjectDatabaseElement(AttributedExpressionAst ast) : base(ast) {
    Ast = ast;
  }
}