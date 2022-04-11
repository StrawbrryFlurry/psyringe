using System.Management.Automation.Language;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing;
using PSyringe.Language.Extensions;

namespace PSyringe.Language.Parsing;

public class ScriptParserVisitor : AstVisitor2, IScriptParserVisitor {
  private readonly List<IAttributedScriptElement<AttributedExpressionAst>> _attributedVariableExpressions = new();
  private readonly List<IAttributedScriptElement<FunctionDefinitionAst>> _functionDefinitions = new();

  public readonly List<UsingStatementAst> UsingStatements = new();

  public IEnumerable<IAttributedScriptElement<AttributedExpressionAst>> AttributedVariableExpressions =>
    _attributedVariableExpressions;

  public IEnumerable<IAttributedScriptElement<FunctionDefinitionAst>> FunctionDefinitions => _functionDefinitions;

  public ScriptBlockAst? Ast { get; private set; }
  public bool HasVisited { get; private set; }

  public void Visit(ScriptBlockAst scriptBlockAst) {
    Ast = scriptBlockAst;
    scriptBlockAst.Visit(this);
    HasVisited = true;
  }

  public override AstVisitAction VisitUsingStatement(UsingStatementAst usingStatementAst) {
    UsingStatements.Add(usingStatementAst);
    return ContinueVisiting();
  }

  public override AstVisitAction VisitAttributedExpression(AttributedExpressionAst ast) {
    if (IsValidAttributeOnVariableExpression(ast, out var attribute)) {
      return AddAttributedVariableExpression(ast, attribute);
    }

    return ContinueVisiting();
  }

  private bool IsValidAttributeOnVariableExpression(AttributedExpressionAst ast, out AttributeBaseAst attribute) {
    var isAttributedVariableExpression = ast.GetNestedChildAssignableToType<VariableExpressionAst>() is not null;
    var isPSyringeAttribute = ast.Attribute.CanBeUsedForType(PSAttributeTargets.Variable);
    attribute = ast.Attribute;

    return isPSyringeAttribute && isAttributedVariableExpression;
  }

  private AstVisitAction AddAttributedVariableExpression(AttributedExpressionAst ast, AttributeBaseAst attribute) {
    _attributedVariableExpressions.Add(new AttributedScriptElement<AttributedExpressionAst> {
      Ast = ast,
      Attribute = attribute
    });
    return ContinueVisiting();
  }

  public override AstVisitAction VisitFunctionDefinition(FunctionDefinitionAst ast) {
    if (HasValidAttributeOnFunctionDefinition(ast, out var attribute)) {
      return AddFunctionDefinition(ast, attribute);
    }

    return ContinueVisiting();
  }

  private bool HasValidAttributeOnFunctionDefinition(FunctionDefinitionAst ast, out AttributeBaseAst? attribute) {
    var attributes = ast.GetAttributes();
    var usableAttribute = attributes.FirstOrDefault(a => a.CanBeUsedForType(PSAttributeTargets.Function));

    if (usableAttribute is null) {
      attribute = null;
      return false;
    }

    attribute = usableAttribute;
    return true;
  }

  private AstVisitAction AddFunctionDefinition(FunctionDefinitionAst ast, AttributeBaseAst attribute) {
    _functionDefinitions.Add(new AttributedScriptElement<FunctionDefinitionAst> {
      Ast = ast,
      Attribute = attribute
    });

    return ContinueVisiting();
  }

  private AstVisitAction ContinueVisiting() {
    return AstVisitAction.Continue;
  }
}