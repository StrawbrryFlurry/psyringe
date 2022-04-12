using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class OnErrorCallbackElement : ScriptElement {
  public OnErrorCallbackElement(Ast ast) : base(ast) {
  }
}