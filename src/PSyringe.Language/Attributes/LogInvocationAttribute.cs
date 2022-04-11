using PSyringe.Common.Language.Attributes;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

/// <summary>
///   Logs in the ILogger provider of the DI Container that the
///   attributed method was called.
/// </summary>
/// TODO:
/// function Logged {
/// [LogInvocation()]
/// param()
/// // Compiler generated:
/// // $__PS_INJECTED__LOGGER__PROVIDER__.LogMethodInvocation($MyInvocation.MyCommand.Name);
/// }
[PSAttributeUsage(Function)]
public class LogInvocationAttribute : Attribute {
  // TODO:
  public LogInvocationAttribute(bool IncludeParameters = false) {
  }
}