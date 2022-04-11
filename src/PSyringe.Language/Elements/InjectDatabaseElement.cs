using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;
using PSyringe.Language.Elements.Properties;

namespace PSyringe.Language.Elements;

public class InjectDatabaseElement : VariableInjectionTarget, IInjectDatabaseElement {
  public InjectDatabaseElement(AttributedExpressionAst ast) : base(ast) {
    Ast = ast;
  }

  public Ast Ast { get; }
}