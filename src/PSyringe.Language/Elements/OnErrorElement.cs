using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class OnErrorElement : IOnErrorElement {
  public OnErrorElement(FunctionDefinitionAst ast) {
    Ast = ast;
  }

  public Ast Ast { get; }
}