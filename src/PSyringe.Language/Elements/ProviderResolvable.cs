using PSyringe.Common.Language;
using static PSyringe.Language.AstTransformation.CodeGenConstants;

namespace PSyringe.Language.Elements;

public class ProviderResolvable : IProviderResolvable {
  public bool Optional { get; }
  public string? Scope { get; }
  public string? Name { get; }
  public Type? Type { get; }


  public ProviderResolvable(string name, string? scope = null, bool optional = false) {
    Name = name;
    Optional = optional;
    Scope = scope;
  }

  public ProviderResolvable(Type type, string? scope = null, bool optional = false) {
    Type = type;
    Optional = optional;
    Scope = scope;
  }

  public bool IsOptional() {
    return Optional;
  }

  public string GetScope() {
    return Scope ?? GlobalScope;
  }

  public override string ToString() {
    return Name ?? GetTypeName();
  }

  private string GetTypeName() {
    var name = Type!.FullName!;
    return name.Replace(".", "_");
  }
}