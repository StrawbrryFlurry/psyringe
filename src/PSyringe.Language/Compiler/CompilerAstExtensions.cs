using System.Management.Automation.Language;

namespace PSyringe.Language.Compiler;

public static class CompilerAstExtensions {
  public static TR ReplaceAst<T, TR>(this TR ast, T astToReplace, T replacementAst) where T : Ast where TR : Ast {
    var visitor = new ReplaceAstRefVisitor(astToReplace, replacementAst);
    return (TR) ast.Visit(visitor);
  }

  public static T? FindOfType<T>(this Ast ast) where T : Ast {
    return ast.Find(a => a is T, true) as T;
  }

  public static T CopyAs<T>(this Ast ast) where T : Ast {
    return (T) ast.Copy();
  }

  public static IEnumerable<T> FindAllOfType<T>(this T ast) where T : Ast {
    return (IEnumerable<T>) ast.FindAll(a => a is T, true);
  }

  internal class ReplaceAstRefVisitor : ICustomAstVisitor2 {
    private readonly Ast _astToReplace;
    private readonly Ast _replacementAst;

    public ReplaceAstRefVisitor(Ast astToReplace, Ast replacementAst) {
      _astToReplace = astToReplace;
      _replacementAst = replacementAst;
    }

    public object? VisitAttribute(AttributeAst attributeAst) {
      return null;
      ;
    }

    public object? VisitCommand(CommandAst commandAst) {
      return null;
      ;
    }

    public object? VisitPipeline(PipelineAst pipelineAst) {
      return null;
      ;
    }

    public object? VisitTrap(TrapStatementAst trapStatementAst) {
      return null;
      ;
    }

    public object? VisitHashtable(HashtableAst hashtableAst) {
      return null;
      ;
    }

    public object? VisitParameter(ParameterAst parameterAst) {
      return null;
      ;
    }

    public object? VisitNamedBlock(NamedBlockAst namedBlockAst) {
      var newTraps = VisitElements(namedBlockAst.Traps);
      var newStatements = VisitStatements(namedBlockAst.Statements);
      var statementBlock = new StatementBlockAst(namedBlockAst.Extent, newStatements, newTraps);
      return new NamedBlockAst(namedBlockAst.Extent, namedBlockAst.BlockKind, statementBlock, namedBlockAst.Unnamed);
    }

    public object? VisitParamBlock(ParamBlockAst paramBlockAst) {
      return null;
    }

    public object? VisitFunctionDefinition(FunctionDefinitionAst functionDefinitionAst) {
      return null;
    }

    public object? VisitCatchClause(CatchClauseAst catchClauseAst) {
      return null;
    }

    public object? VisitIfStatement(IfStatementAst ifStmtAst) {
      return null;
    }

    public object? VisitScriptBlock(ScriptBlockAst scriptBlockAst) {
      var newParamBlock = VisitElement(scriptBlockAst.ParamBlock);
      var newBeginBlock = VisitElement(scriptBlockAst.BeginBlock);
      var newProcessBlock = VisitElement(scriptBlockAst.ProcessBlock);
      var newEndBlock = VisitElement(scriptBlockAst.EndBlock);
      var newDynamicParamBlock = VisitElement(scriptBlockAst.DynamicParamBlock);

      return new ScriptBlockAst(
        scriptBlockAst.Extent,
        newParamBlock,
        newBeginBlock,
        newProcessBlock,
        newEndBlock,
        newDynamicParamBlock
      );
    }

    public object? VisitArrayLiteral(ArrayLiteralAst arrayLiteralAst) {
      return null;
    }

    public object? VisitForStatement(ForStatementAst forStatementAst) {
      return null;
    }

    public object? VisitTryStatement(TryStatementAst tryStatementAst) {
      return null;
    }

    public object? VisitBreakStatement(BreakStatementAst breakStatementAst) {
      return null;
    }

    public object? VisitDataStatement(DataStatementAst dataStatementAst) {
      return null;
    }

    public object? VisitExitStatement(ExitStatementAst exitStatementAst) {
      return null;
    }

    public object? VisitSubExpression(SubExpressionAst subExpressionAst) {
      return null;
    }

    public object? VisitPipelineChain(PipelineChainAst statementChainAst) {
      return null;
    }

    public object? VisitBlockStatement(BlockStatementAst blockStatementAst) {
      return null;
    }

    public object? VisitErrorStatement(ErrorStatementAst errorStatementAst) {
      return null;
    }

    public object? VisitStatementBlock(StatementBlockAst statementBlockAst) {
      return null;
    }

    public object? VisitThrowStatement(ThrowStatementAst throwStatementAst) {
      return null;
    }

    public object? VisitTypeConstraint(TypeConstraintAst typeConstraintAst) {
      return null;
    }

    public object? VisitTypeExpression(TypeExpressionAst typeExpressionAst) {
      return null;
    }

    public object? DefaultVisit(Ast ast) {
      return null;
    }

    public object? VisitArrayExpression(ArrayExpressionAst arrayExpressionAst) {
      return null;
    }

    public object? VisitAssignmentStatement(AssignmentStatementAst assignmentStatementAst) {
      var newLeft = VisitElement(assignmentStatementAst.Left);
      var newRight = VisitElement(assignmentStatementAst.Right);

      return new AssignmentStatementAst(
        assignmentStatementAst.Extent,
        newLeft,
        assignmentStatementAst.Operator,
        newRight,
        assignmentStatementAst.ErrorPosition
      );
    }

    public object? VisitWhileStatement(WhileStatementAst whileStatementAst) {
      return null;
    }

    public object? VisitFunctionMember(FunctionMemberAst functionMemberAst) {
      return null;
    }

    public object? VisitPropertyMember(PropertyMemberAst propertyMemberAst) {
      return null;
    }

    public object? VisitTypeDefinition(TypeDefinitionAst typeDefinitionAst) {
      return null;
    }

    public object? VisitAttributedExpression(AttributedExpressionAst attributedExpressionAst) {
      var newAttribute = VisitElement(attributedExpressionAst.Attribute)!;
      var newChild = VisitElement(attributedExpressionAst.Child);

      return new AttributedExpressionAst(
        attributedExpressionAst.Extent,
        newAttribute,
        newChild
      );
    }

    public object? VisitUsingStatement(UsingStatementAst usingStatement) {
      return null;
    }

    public object? VisitBinaryExpression(BinaryExpressionAst binaryExpressionAst) {
      return null;
    }

    public object? VisitErrorExpression(ErrorExpressionAst errorExpressionAst) {
      return null;
    }

    public object? VisitFileRedirection(FileRedirectionAst fileRedirectionAst) {
      return null;
    }

    public object? VisitIndexExpression(IndexExpressionAst indexExpressionAst) {
      return null;
    }

    public object? VisitParenExpression(ParenExpressionAst parenExpressionAst) {
      return null;
    }

    public object? VisitReturnStatement(ReturnStatementAst returnStatementAst) {
      return null;
    }

    public object? VisitSwitchStatement(SwitchStatementAst switchStatementAst) {
      return null;
    }

    public object? VisitUnaryExpression(UnaryExpressionAst unaryExpressionAst) {
      return null;
    }

    public object? VisitUsingExpression(UsingExpressionAst usingExpressionAst) {
      return null;
    }

    public object? VisitCommandParameter(CommandParameterAst commandParameterAst) {
      return null;
    }

    public object? VisitMemberExpression(MemberExpressionAst memberExpressionAst) {
      return null;
    }

    public object? VisitCommandExpression(CommandExpressionAst commandExpressionAst) {
      return null;
    }

    public object? VisitContinueStatement(ContinueStatementAst continueStatementAst) {
      return null;
    }

    public object? VisitConvertExpression(ConvertExpressionAst convertExpressionAst) {
      return null;
    }

    public object? VisitConfigurationDefinition(ConfigurationDefinitionAst configurationDefinitionAst) {
      return null;
    }

    public object? VisitTernaryExpression(TernaryExpressionAst ternaryExpressionAst) {
      return null;
    }

    public object? VisitConstantExpression(ConstantExpressionAst constantExpressionAst) {
      return null;
    }

    public object? VisitMergingRedirection(MergingRedirectionAst mergingRedirectionAst) {
      return null;
    }

    public object? VisitVariableExpression(VariableExpressionAst variableExpressionAst) {
      return null;
    }

    public object? VisitDoUntilStatement(DoUntilStatementAst doUntilStatementAst) {
      return null;
    }

    public object? VisitDoWhileStatement(DoWhileStatementAst doWhileStatementAst) {
      return null;
    }

    public object? VisitDynamicKeywordStatement(DynamicKeywordStatementAst dynamicKeywordAst) {
      return null;
    }

    public object? VisitForEachStatement(ForEachStatementAst forEachStatementAst) {
      return null;
    }

    public object? VisitScriptBlockExpression(ScriptBlockExpressionAst scriptBlockExpressionAst) {
      return null;
    }

    public object? VisitInvokeMemberExpression(InvokeMemberExpressionAst invokeMemberExpressionAst) {
      return null;
    }

    public object? VisitNamedAttributeArgument(NamedAttributeArgumentAst namedAttributeArgumentAst) {
      return null;
    }

    public object? VisitStringConstantExpression(StringConstantExpressionAst stringConstantExpressionAst) {
      return null;
    }

    public object? VisitExpandableStringExpression(ExpandableStringExpressionAst expandableStringExpressionAst) {
      return null;
    }

    public object? VisitBaseCtorInvokeMemberExpression(
      BaseCtorInvokeMemberExpressionAst baseCtorInvokeMemberExpressionAst) {
      return null;
    }

    private IEnumerable<StatementAst> VisitStatements(IEnumerable<StatementAst> statements) {
      var newStatements = new List<StatementAst>();

      foreach (var statement in statements) {
        newStatements.Add(VisitElement(statement)!);
      }

      return newStatements;
    }

    private T? VisitElement<T>(T? element) where T : Ast {
      if (ReplaceIfSameRef(element, out var newElement)) {
        return newElement as T;
      }

      return element?.Visit(this) as T ?? (element?.Copy() as T)!;
    }

    private IEnumerable<T?>? VisitElements<T>(IEnumerable<T>? elements) where T : Ast {
      List<T?> newElements = new();

      if (elements is null) {
        return null;
      }

      foreach (var element in elements) {
        newElements.Add(VisitElement(element));
      }

      return newElements;
    }

    private bool ReplaceIfSameRef(Ast? ast, out Ast? replacementAst) {
      replacementAst = null;

      if (ReferenceEquals(ast, _astToReplace)) {
        replacementAst = _replacementAst;
        return true;
      }

      return false;
    }
  }
}