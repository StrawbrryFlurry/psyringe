using System.Management.Automation.Language;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing.Elements.Base;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

/// <summary>
///   Injects a constant value from the script environment into a variable.
/// </summary>
[PSAttributeUsage(Variable | Parameter)]
public class InjectConstantAttribute : Attribute, IPSyringeAttribute<AttributedExpressionAst> {
  public InjectConstantAttribute(string? Target, bool Optional = false) {
  }

  public IElement<AttributedExpressionAst> CreateElement(AttributedExpressionAst ast) {
    return new InjectConstantElement(ast);
  }
}