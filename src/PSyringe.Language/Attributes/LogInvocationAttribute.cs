using PSyringe.Language.Attributes.Base;

namespace PSyringe.Language.Attributes; 

/// <summary>
/// Logs in the ILogger provider of the DI Container that the
/// attributed method was called.
/// </summary>
/// TODO:
/// function Logged {
///   [LogInvocation()]
///   param()
///   // Compiler generated:
///   // $__PS_INJECTED__LOGGER__PROVIDER__.LogMethodInvocation($MyInvocation.MyCommand.Name);
/// }
public class LogInvocationAttribute : Attribute {
  public LogInvocationAttribute(bool IncludeParameters = false) {
  }
}