using System.Management.Automation;
using System.Management.Automation.Language;
using PSyringe.Language.Attributes;
using PSyringe.Language.Extensions;

namespace PSyringe.Core.Language.Parsing.Elements;

public abstract class AbstractInjectionTargetElement {
  public string Name { get; set; }
  public Type? InjectionTargetType { get; set; }
  public PSObject DefaultValue { get; set; }
  public string Scope { get; set; }

  public string ImplicitType { get; set; }
  public string ExplicitType { get; set; }
  public bool HasDefaultValue { get; set; }

  private void AssignInjectType(IReadOnlyCollection<AttributeBaseAst> attributes) {
    var implicitInjectType = attributes.FirstOrDefault(a => a is TypeConstraintAst);
    var explicitInjectAttribute = attributes.FirstOrDefault(a => a.IsAssignableToType<InjectAttribute>());
  }
}