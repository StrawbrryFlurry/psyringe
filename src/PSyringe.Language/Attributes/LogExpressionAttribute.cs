using PSyringe.Common.Language.Attributes;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

[PSAttributeUsage(Variable)]
public class LogExpressionAttribute : Attribute, IPSyringeAttribute<LogExpressionElement> {
  // TODO:
}