using System.Management.Automation.Language;
using System.Text;
using static PSyringe.Language.AstTransformation.CodeGenConstants;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class FunctionMemberAstExtensions {
  public static string ToStringFromAst(this FunctionMemberAst ast) {
    var name = ast.Name;
    var parameters = ast.Parameters?.ToStringFromAstJoinBy(", ");
    var attributes = ast.Attributes?.ToStringFromAstJoinBy(NewLine);
    var accessModifiers = ast.MethodAttributes.GetAllSetEnumFlags(MethodAttributes.None | MethodAttributes.Public)
                             .Select(a => a.ToLower())
                             .JoinBy(" ");
    var returnType = ast.ReturnType?.ToStringFromAst();

    var method = new StringBuilder();

    if (attributes is not null) {
      method.AppendLine(attributes);
    }

    if (!string.IsNullOrWhiteSpace(accessModifiers)) {
      method.Append(accessModifiers);
      method.Append(' ');
    }

    if (returnType is not null) {
      method.Append(returnType);
      method.Append(' ');
    }

    method.Append(name);

    method.Append('(');
    method.Append(parameters);
    method.Append(')');
    method.Append(' ');

    if (HasBaseCtorCall(ast, out var baseCtorCallAst)) {
      var statements = ast.Body.EndBlock.Statements;
      var baseCtorCall = baseCtorCallAst!.ToStringFromAst();
      var blockStatements = statements.Skip(1).ToStringFromAstJoinBy(NewLine);

      method.Append(": ");
      method.Append(baseCtorCall);
      method.Append(' ');

      method.AppendLine("{");
      method.Append(blockStatements);
      method.Append('}');
    }
    else {
      var body = ast.Body.ToStringFromAst();
      method.Append(body);
    }

    return method.ToString();
  }

  /// <summary>
  ///   Constructor methods may have a base constructor call
  ///   which, in PowerShell, is hidden in the method body.
  ///   <code>
  ///     class Tiger : Animal {
  ///       public Tiger() : base() ← Base call {
  ///     }
  ///   </code>
  ///   The `BaseCtorInvokeMemberExpressionAst` that represents this
  ///   base call is contained in the first statement of the method body,
  ///   as part of a `CommandExpressionAst`.
  /// </summary>
  private static bool HasBaseCtorCall(FunctionMemberAst ast, out BaseCtorInvokeMemberExpressionAst? baseCtorCallAst) {
    // Class methods cannot have named blocks so
    // the body is always in the `EndBlock` 
    var statements = ast.Body.EndBlock?.Statements;
    baseCtorCallAst = null;

    if (statements is null) {
      return false;
    }

    var baseCtorCall = statements.First();

    if (baseCtorCall is not CommandExpressionAst {Expression: BaseCtorInvokeMemberExpressionAst baseCtorExpression}) {
      return false;
    }

    baseCtorCallAst = baseCtorExpression;
    return true;
  }
}