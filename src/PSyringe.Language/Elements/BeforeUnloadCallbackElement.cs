using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class BeforeUnloadCallbackElement : IBeforeUnloadCallbackElement {
  public BeforeUnloadCallbackElement(FunctionDefinitionAst ast) {
    Ast = ast;
  }

  public FunctionDefinitionAst Ast { get; }
}