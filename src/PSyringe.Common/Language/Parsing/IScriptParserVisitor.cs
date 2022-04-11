using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Parsing;

public interface IAttributedScriptElement<T> where T : Ast {
  public Type Attribute { init; get; }
  public T Ast { init; get; }
}

public interface IScriptParserVisitor {
  public ScriptBlockAst Ast { get; }
  public bool HasVisited { get; }

  public List<IAttributedScriptElement<FunctionDefinitionAst>> FunctionDefinitions { get; }
  public List<IAttributedScriptElement<AttributedExpressionAst>> AttributedVariableExpressions { get; }

  public void Visit(ScriptBlockAst ast);
}