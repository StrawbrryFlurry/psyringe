using System.Management.Automation.Language;
using PSyringe.Common.Compiler;
using PSyringe.Common.Language;
using PSyringe.Common.Language.Elements;
using PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;
using PSyringe.Language.AstTransformation.SyntheticAsts;
using PSyringe.Language.Extensions;
using static PSyringe.Language.AstTransformation.CodeGenConstants;

namespace PSyringe.Language.AstTransformation;

public abstract class ScriptTransformer : IScriptTransformer {
  internal readonly Dictionary<VariableExpressionAst, IProviderResolvable> Providers = new();

  public abstract void Transform(ref IScriptDefinition script);

  public VariableExpressionAst MakeVariable(string name, string? scope = null, bool splatted = false) {
    var variableScope = string.IsNullOrWhiteSpace(scope) ? "script" : scope;
    var variablePath = $"{variableScope}:{name}";

    return SyntheticVariableExpression.Create(variablePath, splatted);
  }

  public VariableExpressionAst AddProvider(string target, IProviderResolvable provider) {
    var variableName = GetProviderVariableName(target, provider);
    var variable = MakeVariable(variableName);
    Providers.Add(variable, provider);

    return variable;
  }

  public IEnumerable<IScriptVariableDependency> GetVariableDependencies() {
    // return Providers.Select((variable, provider) => new(variable, provider));
    throw new NotImplementedException();
  }

  /// <summary>
  ///   Removes an attribute of type <see cref="attributeType" /> from the AttributedExpressionTree.
  ///   [Foo()][Bar()]]Baz()]$Foo;
  /// </summary>
  /// <param name="ast"></param>
  /// <param name="attributeType"></param>
  public void ReplaceAttributeInNestedExpression(AttributedExpressionAst ast, Type attributeType) {
    if (ast is not AttributedExpressionAst) {
      return;
    }

    if (ast.IsAttributeOfExactType(attributeType)) {
      ast.ReplaceChild(ast, ast.Child);
      return;
    }

    if (ast.Child is AttributedExpressionAst child) {
      ReplaceAttributeInNestedExpression(child, attributeType);
    }

    if (ast.Parent is AttributedExpressionAst parent) {
      ReplaceAttributeInNestedExpression(parent, attributeType);
    }
  }

  internal string GetProviderVariableName(string target, IProviderResolvable provider) {
    var variableName = $"{VariablePrefix}prov_{provider.GetScope()}_{target}_inj_{provider}";
    return variableName;
  }
}