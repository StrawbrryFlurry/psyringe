using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class OnLoadedCallbackElement : IOnLoadedCallbackElement {
  public OnLoadedCallbackElement(FunctionDefinitionAst ast) {
    Ast = ast;
  }

  public FunctionDefinitionAst Ast { get; }
}