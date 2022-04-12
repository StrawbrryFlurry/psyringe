using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class InjectConstantElement : ScriptElement {
  public InjectConstantElement(AttributedExpressionAst ast) : base(ast) {
  }
}