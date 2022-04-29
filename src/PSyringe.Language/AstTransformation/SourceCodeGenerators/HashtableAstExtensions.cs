using System.Management.Automation.Language;
using System.Text;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class HashtableAstExtensions {
  public static string ToStringFromAst(this HashtableAst ast) {
    var keyValuePairs = ast.KeyValuePairs;
    var hashtable = new StringBuilder();

    hashtable.AppendLine("@{");

    foreach (var (keyAst, valueAst) in keyValuePairs) {
      var key = keyAst.ToStringFromAst();
      var value = valueAst.ToStringFromAst();
      hashtable.AppendLine($"{key} = {value};");
    }

    hashtable.Append('}');

    return hashtable.ToString();
  }
}