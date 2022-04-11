using System.Management.Automation.Language;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing.Elements.Base;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

[PSAttributeUsage(Function)]
public class InjectionSiteAttribute : Attribute, IPSyringeAttribute<FunctionDefinitionAst> {
  internal string Scope;

  public InjectionSiteAttribute(string? Scope = null) {
    this.Scope = Scope;
  }

  public IElement<FunctionDefinitionAst> CreateElement(FunctionDefinitionAst ast) {
    return new InjectionSiteElement(ast);
  }
}