using System.Management.Automation.Language;
using System.Text;
using static PSyringe.Language.Compiler.CompilerScriptText;

namespace PSyringe.Language.AstTransformation;

public static class ScriptBlockAstExtensions {
  public static string ToStringFromAst(this ScriptBlockAst ast) {
    var usingStatements = ast.UsingStatements?.ToStringFromAstJoinBy(NewLine);
    var requirements = ast.ScriptRequirements;
    var attributes = ast.Attributes.ToStringFromAstJoinBy("");
    var paramBlock = ast.ParamBlock?.ToStringFromAst();

    var isRootScriptBlock = ast.Parent is null;

    var scriptBlock = new StringBuilder();


    if (requirements is not null) {
      AppendScriptRequirements(scriptBlock, requirements);
    }

    if (!isRootScriptBlock) {
      scriptBlock.Append(attributes);
      scriptBlock.Append('{');
    }

    AppendLineIfNotNull(scriptBlock, usingStatements);
    AppendLineIfNotNull(scriptBlock, paramBlock);

    AppendBlock(scriptBlock, ast.DynamicParamBlock);
    AppendBlock(scriptBlock, ast.BeginBlock);
    AppendBlock(scriptBlock, ast.ProcessBlock);
    AppendBlock(scriptBlock, ast.EndBlock);
    AppendBlock(scriptBlock, ast.CleanBlock);

    if (!isRootScriptBlock) {
      scriptBlock.Append('}');
    }

    return scriptBlock.ToString();
  }

  private static void AppendBlock(StringBuilder sb, NamedBlockAst? blockAst) {
    var block = blockAst?.ToStringFromAst();
    AppendLineIfNotNull(sb, block);
  }

  private static void AppendLineIfNotNull(StringBuilder sb, string? line) {
    if (!string.IsNullOrWhiteSpace(line)) {
      sb.AppendLine(line);
    }
  }

  private static void AppendScriptRequirements(StringBuilder sb, ScriptRequirements requirements) {
    var assemblies = requirements.RequiredAssemblies.Select(e => $"#requires -Assembly {e};").JoinBy(NewLine);
    var modules = requirements.RequiredModules.Select(e => $"#requires -Module {e};").JoinBy(NewLine);
    var psEditions = requirements.RequiredPSEditions.Select(e => $"#requires -PSEdition {e}").JoinBy(NewLine);
    var psSnapins = requirements.RequiresPSSnapIns
                                .Select(e => $"#requires -PSSnapin {e.Name} -Version {e.Version};")
                                .JoinBy(NewLine);

    AppendLineIfNotNull(sb, assemblies);
    AppendLineIfNotNull(sb, modules);
    AppendLineIfNotNull(sb, psEditions);
    AppendLineIfNotNull(sb, psSnapins);

    if (requirements.IsElevationRequired) {
      sb.AppendLine("#requires -RunAsAdministrator;");
    }

    if (requirements.RequiredPSVersion is not null) {
      sb.AppendLine($"#requires -Version {requirements.RequiredPSVersion};");
    }
  }
}