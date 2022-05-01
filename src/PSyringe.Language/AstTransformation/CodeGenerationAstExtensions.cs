using System.Management.Automation.Language;
using System.Reflection;

namespace PSyringe.Language.AstTransformation;

public static class CodeGenerationReflectionAstExtensions {
  private static readonly MethodInfo _astParentSetter = GetPrivateAstMethod("set_Parent");

  public static bool Is(this Ast ast, Ast comparisonAst) {
    return ReferenceEquals(ast, comparisonAst);
  }

  public static void SetParent(this Ast ast, Ast? newParent = null) {
    _astParentSetter.Invoke(ast, new object?[] {newParent});
  }

  public static void SetPrivateProperty(this Ast ast, string fieldName, object value) {
    CodeGenerationUtil.SetProperty(ast, fieldName, value);
  }

  /// <summary>
  ///   Returns a private method from the <see cref="Ast" /> class.
  /// </summary>
  /// <param name="methodName"></param>
  /// <returns></returns>
  private static MethodInfo GetPrivateAstMethod(string methodName) {
    var method = typeof(Ast).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic)!;
    return method;
  }
}