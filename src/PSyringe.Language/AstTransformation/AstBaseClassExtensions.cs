using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation;

/// <summary>
///   Extension methods for base AST types. Uses reflection
///   to get the extension method for the concrete implementation
///   of the runtime AST type.
/// </summary>
public static class AstBaseClassExtensions {
  public static string GetAstAsString(this Ast ast) {
    return ast.InvokeExtensionMethodInAssemblyForConcreteType<string>(nameof(GetAstAsString));
  }

  public static string GetAstAsString(this ExpressionAst ast) {
    return ast.InvokeExtensionMethodInAssemblyForConcreteType<string>(nameof(GetAstAsString));
  }
}