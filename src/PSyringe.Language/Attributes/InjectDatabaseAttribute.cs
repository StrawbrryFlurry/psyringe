using System.Management.Automation.Language;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing.Elements.Base;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

/// <summary>
///   TODO:
/// </summary>
[PSAttributeUsage(Variable | Parameter)]
public class InjectDatabaseAttribute : Attribute, IPSyringeAttribute<AttributedExpressionAst> {
  public InjectDatabaseAttribute(string ConnectionStrting, bool IsConnectionString = false) {
    IsProviderConnectionString = IsConnectionString;
  }

  public bool IsProviderConnectionString { get; }

  public IElement<AttributedExpressionAst> CreateElement(AttributedExpressionAst ast) {
    return new InjectDatabaseElement(ast);
  }
}