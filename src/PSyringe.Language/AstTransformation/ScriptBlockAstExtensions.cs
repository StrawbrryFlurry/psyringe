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
      scriptBlock.AppendLine("{");
    }

    AppendLineIfNotNull(scriptBlock, usingStatements);
    AppendLineIfNotNull(scriptBlock, paramBlock);

    if (!HasNoNamedBlocks(ast)) {
      AppendBlock(scriptBlock, ast.DynamicParamBlock);
      AppendBlock(scriptBlock, ast.BeginBlock);
      AppendBlock(scriptBlock, ast.ProcessBlock);
      AppendBlock(scriptBlock, ast.EndBlock);
      AppendBlock(scriptBlock, ast.CleanBlock);
    }
    else {
      var statements = ast.EndBlock.Statements?.ToStringFromAstJoinBy(NewLine);
      var traps = ast.EndBlock.Traps?.ToStringFromAstJoinBy(NewLine);
      AppendLineIfNotNull(scriptBlock, statements);
      AppendLineIfNotNull(scriptBlock, traps);
    }

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
    var psEditions = requirements.RequiredPSEditions.Select(e => $"#requires -PSEdition {e};").JoinBy(NewLine);
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

  /// <summary>
  ///   When a ScriptBlock has no defined blocks
  ///   e.g. is defined like so:
  ///   <code>
  /// {
  ///   "Do Something!"
  /// }
  /// </code>
  ///   The parser puts the statements as a named block into
  ///   the EndBlock of the AST.
  /// </summary>
  /// <param name="ast"></param>
  /// <returns></returns>
  private static bool HasNoNamedBlocks(ScriptBlockAst ast) {
    return ast.BeginBlock is null &&
           ast.ProcessBlock is null &&
           ast.CleanBlock is null &&
           ast.DynamicParamBlock is null &&
           ast.EndBlock is not null;
  }
}