using PSyringe.Common.Language.Attributes;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

[PSAttributeUsage(Variable | Parameter)]
public class InjectLoggerAttribute : Attribute {
  // TODO:
}