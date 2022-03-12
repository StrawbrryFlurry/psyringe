using System.Linq;
using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing;
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

  public static IScriptElement ParseScriptUsingPSyringeParser(string script) {
    var factory = new ElementFactory();
    var visitor = new ScriptVisitor();
    var parser = new ScriptParser(factory);

    return parser.Parse(script, visitor);
  }

  // The caller will always prepend the assembly reference
  // because we directly create the ast, we need to prepend
  // the assembly reference manually.
  private static void PrepareScript(ref string script) {
    ScriptParser.PrependAssemblyReference(ref script);
  }
  
  public static AttributedExpressionAst GetAttributedExpressionAstFromScript(string script) {
    var ast = ParseScript(script);
    return  GetAttributedExpressionFromScriptBlock(ast);
  }
  
  private static AttributedExpressionAst GetAttributedExpressionFromScriptBlock(ScriptBlockAst ast) {
    var statementAst = ast.EndBlock.Statements.FirstOrDefault();
    AttributedExpressionAst attributedExpressionAst = null;

    if (statementAst is AssignmentStatementAst assignmentStatementAst) {
      attributedExpressionAst = (assignmentStatementAst.Left as AttributedExpressionAst)!;
    }

    if (statementAst is PipelineAst pipelineAst) {
      var commandAst = pipelineAst.PipelineElements.FirstOrDefault() as CommandExpressionAst;
      attributedExpressionAst = commandAst.Expression as AttributedExpressionAst;
    }

    return attributedExpressionAst;
  }
}