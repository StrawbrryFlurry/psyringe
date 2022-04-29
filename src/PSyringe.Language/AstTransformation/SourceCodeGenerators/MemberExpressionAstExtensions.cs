using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class MemberExpressionAstExtensions {
  public static string ToStringFromAst(this MemberExpressionAst ast) {
    var isStatic = ast.Static;

    var invocationOperator = isStatic ? "::" : ".";
    var expression = ast.Expression.ToStringFromAst();
    var memberName = ast.Member.ToStringFromAst();

    var nullCoalescingOperator = ast.NullConditional ? "?" : "";

    var genericArguments = ast.GenericTypeArguments?
                              .Select(t => t.ToStringFromAst())
                              .Select(s => s.TrimStart('[').TrimEnd(']'))
                              .JoinBy(", ");

    var genericArgumentString = genericArguments is null ? "" : $"[{genericArguments}]";

    // [array]::Empty[string]
    return $"{expression}{nullCoalescingOperator}{invocationOperator}{memberName}{genericArgumentString}";
  }
}