using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Parsing;

public interface IScriptVisitor {
  public ScriptBlockAst Ast { get; }
  public bool HasVisited { get; }

  public List<FunctionDefinitionAst> CallbackFunctions { get; }
  public List<AttributedExpressionAst> InjectExpressions { get; }
  public List<FunctionDefinitionAst> InjectionSites { get; }
  public List<AttributedExpressionAst> ProvideExpressions { get; }

  public void Visit(ScriptBlockAst ast);
}