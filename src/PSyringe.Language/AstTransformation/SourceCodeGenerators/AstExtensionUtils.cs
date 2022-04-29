using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class AstExtensionUtils {
  public static string GetLabel(this LabeledStatementAst labeledStatement) {
    var label = labeledStatement.Label;

    if (label is null) {
      return "";
    }

    return $":{label} ";
  }

  public static List<string> GetAllSetEnumFlags<T>(this T @enum, T excludeFlags) where T : struct, Enum {
    var flags = new List<string>();
    var values = Enum.GetValues<T>();

    foreach (var value in values) {
      if (excludeFlags.HasFlag(value)) {
        continue;
      }

      if (@enum.HasFlag(value)) {
        flags.Add(value.ToString());
      }
    }

    return flags;
  }
}