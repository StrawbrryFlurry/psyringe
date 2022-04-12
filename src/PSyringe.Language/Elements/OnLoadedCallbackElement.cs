using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class OnLoadedCallbackElement : ScriptElement {
  public OnLoadedCallbackElement(Ast ast) : base(ast) {
  }
}