using System.Management.Automation.Language;

namespace PSyringe.Language.Test.AstTransformation.Utils;

public static class AstConstants {
  public static IScriptExtent EmptyExtent => new ScriptExtent(null, null);
}