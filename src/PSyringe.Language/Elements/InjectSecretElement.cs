using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class InjectSecretElement : ScriptElement {
  public InjectSecretElement(Ast ast) : base(ast) {
  }
}