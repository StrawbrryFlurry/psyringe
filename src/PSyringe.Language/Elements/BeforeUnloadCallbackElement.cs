using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class BeforeUnloadCallbackElement : IBeforeUnloadCallbackElement {
  public FunctionDefinitionAst Ast { get; }
  
  public BeforeUnloadCallbackElement(FunctionDefinitionAst ast) {
    Ast = ast;
  }
}