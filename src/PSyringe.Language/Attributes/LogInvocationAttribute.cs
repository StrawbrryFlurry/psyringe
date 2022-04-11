using System.Management.Automation.Language;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing.Elements.Base;
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
public class LogInvocationAttribute : Attribute, IPSyringeAttribute<FunctionDefinitionAst> {
  public LogInvocationAttribute(bool IncludeParameters = false) {
  }

  public IElement<FunctionDefinitionAst> CreateElement(FunctionDefinitionAst ast) {
    throw new NotImplementedException();
  }
}