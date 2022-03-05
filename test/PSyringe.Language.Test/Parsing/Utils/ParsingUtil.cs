using System.Management.Automation.Language;
using PSyringe.Language.Parsing;

namespace PSyringe.Language.Test.Parsing.Utils;

public class ParsingUtil {
  public static ScriptBlockAst ParseScript(string script) {
    PrepareScript(ref script);
    return ParseScriptBlock(script);
  }

  private static ScriptBlockAst ParseScriptBlock(string script) {
    var ast = Parser.ParseInput(script, out var tokens, out var errors);
    return ast;
  }

  // The caller will always prepend the assembly reference
  // because we directly create the ast, we need to prepend
  // the assembly reference manually.
  private static void PrepareScript(ref string script) {
    ScriptParser.PrependAssemblyReference(ref script);
  }
}