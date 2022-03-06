using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class OnLoadElement : IOnLoadElement {
  public OnLoadElement(FunctionDefinitionAst ast) {
    Ast = ast;
  }

  public Ast Ast { get; }
}