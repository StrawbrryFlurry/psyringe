using System.Management.Automation.Language;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing.Elements.Base;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

[PSAttributeUsage(Parameter)]
public class InjectParameterAttribute : Attribute, IPSyringeAttribute<ParameterAst> {
  public InjectParameterAttribute(string? targetProviderName, bool optional = false) {
  }

  public IElement<ParameterAst> CreateElement(ParameterAst ast) {
    return new InjectParameterElement(ast);
  }
}