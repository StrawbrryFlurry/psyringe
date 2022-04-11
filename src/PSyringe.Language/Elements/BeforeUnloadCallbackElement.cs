using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class BeforeUnloadCallbackElement : IBeforeUnloadCallbackElement {
  public BeforeUnloadCallbackElement(Ast ast) {
    Ast = ast;
  }

  public Ast Ast { get; }
}