using System.Management.Automation.Language;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing.Elements.Base;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

[PSAttributeUsage(Function)]
public class StartupAttribute : Attribute, IPSyringeAttribute<FunctionDefinitionAst> {
  public IElement<FunctionDefinitionAst> CreateElement(FunctionDefinitionAst ast) {
    return new StartupFunctionElement(ast);
  }
}