using System.Management.Automation.Language;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing.Elements.Base;
using PSyringe.Language.DI;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

// ReSharper disable all InconsistentNaming
[PSAttributeUsage(Variable | Parameter)]
public class InjectAttribute : Attribute, IPSyringeAttribute<AttributedExpressionAst> {
  public InjectAttribute(string? Target = null, bool Optional = false) {
    Provider = new ScriptProvider {
      Name = Target,
      Optional = Optional
    };
  }

  public InjectAttribute(Type Target, bool Optional = false) {
    Provider = new ScriptProvider {
      Type = Target,
      Optional = Optional
    };
  }

  public ScriptProvider Provider { get; set; }

  public IElement<AttributedExpressionAst> CreateElement(AttributedExpressionAst ast) {
    return new InjectElement(ast);
  }
}