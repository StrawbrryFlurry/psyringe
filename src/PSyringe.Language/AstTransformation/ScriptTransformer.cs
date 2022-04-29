using System.Management.Automation.Language;
using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Elements;
using PSyringe.Language.AstTransformation.SyntheticAsts;
using static PSyringe.Language.AstTransformation.CodeGenConstants;

namespace PSyringe.Language.AstTransformation;

public abstract class ScriptTransformer : IScriptTransformer {
  public abstract void Transform(ref IScriptDefinition script);

  protected VariableExpressionAst MakeProvideVariable(string target, string provider, string scope = "GLOBAL") {
    var variableName = $"$script:{VariablePrefix}prov_{target}_inj_{provider}_{scope}";

    return new SyntheticVariableExpression(ScriptExtent(), variableName);
  }

  protected ScriptExtent ScriptExtent() {
    // TODO: Replace with SyntheticScriptExtent once issue is fixed.
    return new ScriptExtent(null, null);
  }
}