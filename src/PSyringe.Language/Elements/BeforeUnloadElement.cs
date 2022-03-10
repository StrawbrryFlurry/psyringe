using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class BeforeUnloadElement : IBeforeUnloadElement {
  public BeforeUnloadElement(FunctionDefinitionAst ast) {
    Ast = ast;
  }

  public Ast Ast { get; }
}