using System.Management.Automation.Language;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing;
using PSyringe.Language.Extensions;

namespace PSyringe.Language.Parsing;

public struct AttributedScriptElement<T> : IAttributedScriptElement<T> where T : Ast {
  public Type Attribute { init; get; }
  public T Ast { init; get; }
}

public class ScriptParserVisitor : AstVisitor2, IScriptParserVisitor {
  private readonly AstVisitAction _continue = AstVisitAction.Continue;
  public readonly List<UsingStatementAst> UsingStatements = new();

  public ScriptBlockAst? Ast { get; private set; }
  public bool HasVisited { get; private set; }

  public List<IAttributedScriptElement<FunctionDefinitionAst>> FunctionDefinitions { get; } = new();
  public List<IAttributedScriptElement<AttributedExpressionAst>> AttributedVariableExpressions { get; } = new();

  public void Visit(ScriptBlockAst scriptBlockAst) {
    Ast = scriptBlockAst;
    scriptBlockAst.Visit(this);
    HasVisited = true;
  }

  public override AstVisitAction VisitUsingStatement(UsingStatementAst usingStatementAst) {
    UsingStatements.Add(usingStatementAst);
    return _continue;
  }

  public override AstVisitAction VisitAttributedExpression(AttributedExpressionAst ast) {
    if (IsUsableAttributeOnVariableExpression(ast, out var attribute)) {
      return AddAttributedVariableExpression(ast, attribute);
    }

    return _continue;
  }

  private bool IsUsableAttributeOnVariableExpression(AttributedExpressionAst ast, out Type attribute) {
    var isAttributedVariableExpression = ast.GetNestedChildAssignableToType<VariableExpressionAst>() is not null;
    var isPSyringeAttribute = ast.Attribute.CanBeUsedForType(PSAttributeTargets.Variable);
    attribute = ast.Attribute.GetAttributeType();

    return isPSyringeAttribute && isAttributedVariableExpression;
  }

  private AstVisitAction AddAttributedVariableExpression(AttributedExpressionAst ast, Type attribute) {
    AttributedVariableExpressions.Add(new AttributedScriptElement<AttributedExpressionAst> {
      Ast = ast,
      Attribute = attribute
    });
    return _continue;
  }

  public override AstVisitAction VisitFunctionDefinition(FunctionDefinitionAst ast) {
    if (HasUsableAttributeOnFunctionDefinition(ast, out var attribute)) {
      return AddFunctionDefinition(ast, attribute);
    }

    return _continue;
  }

  private bool HasUsableAttributeOnFunctionDefinition(FunctionDefinitionAst ast, out Type? attribute) {
    var attributes = ast.GetAttributes();
    var usableAttribute = attributes.FirstOrDefault(a => a.CanBeUsedForType(PSAttributeTargets.Function));

    if (usableAttribute is null) {
      attribute = null;
      return false;
    }

    attribute = usableAttribute.GetAttributeType();
    return true;
  }

  private AstVisitAction AddFunctionDefinition(FunctionDefinitionAst ast, Type attribute) {
    FunctionDefinitions.Add(new AttributedScriptElement<FunctionDefinitionAst> {
      Ast = ast,
      Attribute = attribute
    });
    return _continue;
  }
}