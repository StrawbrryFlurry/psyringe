using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

//  TODO: Nested AttributedExpressions
// [ParameterDescription("Foo Parameter")][InjectParameter("Foo")]$Foo;
public class InjectParameterElement : IInjectParameterElement {
  public InjectParameterElement(ParameterAst ast) {
    Ast = ast;
    Target ??= ast.Name.VariablePath.ToString();
  }

  public string Target { get; }

  public ParameterAst Ast { get; }
}