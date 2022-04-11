using System.Management.Automation.Language;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing.Elements.Base;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

[PSAttributeUsage(Variable)]
public class LogExpressionAttribute : Attribute, IPSyringeAttribute<AttributedExpressionAst> {
  public IElement<AttributedExpressionAst> CreateElement(AttributedExpressionAst ast) {
    throw new NotImplementedException();
  }
}