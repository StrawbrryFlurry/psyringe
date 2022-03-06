using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Parsing;

public interface IScriptVisitor {
  ScriptBlockAst Ast { get; }

  List<FunctionDefinitionAst> CallbackFunctions { get; }
  Dictionary<FunctionDefinitionAst, IEnumerable<ParameterAst>> FunctionParameters { get; }
  List<AttributedExpressionAst> InjectExpressions { get; }
  List<FunctionDefinitionAst> InjectionSites { get; }
  List<AttributedExpressionAst> ProvideExpressions { get; }

  void Visit(ScriptBlockAst ast);
}