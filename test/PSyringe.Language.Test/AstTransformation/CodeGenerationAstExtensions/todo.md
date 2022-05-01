# Class

- [ ] BaseCtorInvokeMemberExpressionAst : InvokeMemberExpressionAst
- [ ] PropertyMemberAst : MemberAst
- [ ] FunctionMemberAst : MemberAst,
- [ ] TypeDefinitionAst : StatementAst

---

# Pipeline

- [ ] PipelineAst : ChainableAst
- [ ] AssignmentStatementAst : PipelineBaseAst
- [ ] PipelineChainAst : ChainableAst
- [ ] ErrorStatementAst : PipelineBaseAst
- [ ] AssignmentStatementAst : PipelineBaseAst

---

# statements

- [ ] StatementBlockAst : Ast IParameterMetadataProvider
- [ ] IfStatementAst : StatementAst
- [ ] ForEachStatementAst : LoopStatementAst
- [ ] ForStatementAst : LoopStatementAst
- [ ] DoWhileStatementAst : LoopStatementAst
- [ ] DoUntilStatementAst : LoopStatementAst
- [ ] WhileStatementAst : LoopStatementAst
- [ ] SwitchStatementAst : LabeledStatementAst
- [ ] UsingStatementAst : StatementAst
- [ ] TryStatementAst : StatementAst
- [ ] CatchClauseAst : Ast
- [ ] TrapStatementAst : StatementAst
- [ ] BreakStatementAst : StatementAst
- [ ] ContinueStatementAst : StatementAst
- [ ] ReturnStatementAst : StatementAst
- [ ] ExitStatementAst : StatementAst
- [ ] ThrowStatementAst : StatementAst
- [ ] BlockStatementAst : StatementAst
- [ ] FunctionDefinitionAst : StatementAst, IParameterMetadataProvider
- [ ] DataStatementAst : StatementAst `Data Foo { }`
- [ ] ConfigurationDefinitionAst `Configuration Foo { }`
- [ ] DynamicKeywordStatementAst : StatementAst

---

# Done

- [ ] NamedBlockAst : Ast
- [ ] AttributedExpressionAst : ExpressionAst, ISupportsAssignment, IAssignableValue
- [ ] ConvertExpressionAst : AttributedExpressionAst, ISupportsAssignment
- [ ] TypeExpressionAst : ExpressionAst
- [ ] VariableExpressionAst : ExpressionAst, ISupportsAssignment, IAssignableValue
- [ ] ConstantExpressionAst : ExpressionAst
- [ ] StringConstantExpressionAst : ConstantExpressionAst
- [ ] ExpandableStringExpressionAst : ExpressionAst
- [ ] ArrayLiteralAst : ExpressionAst, ISupportsAssignment
- [ ] UsingExpressionAst : ExpressionAst
- [ ] IndexExpressionAst : ExpressionAst, ISupportsAssignment
- [ ] TernaryExpressionAst : ExpressionAst
- [ ] BinaryExpressionAst : ExpressionAst
- [ ] UnaryExpressionAst : ExpressionAst
- [ ] NamedAttributeArgumentAst : Ast
- [ ] AttributeAst : AttributeBaseAst
- [ ] TypeConstraintAst : AttributeBaseAst
- [ ] ParameterAst : Ast
- [ ] ErrorExpressionAst : ExpressionAst
- [ ] MergingRedirectionAst : RedirectionAst
- [ ] FileRedirectionAst : RedirectionAst
- [ ] ParamBlockAst : Ast
- [ ] ScriptBlockAst : Ast, IParameterMetadataProvider
- [ ] HashtableAst : ExpressionAst
- [ ] ScriptBlockExpressionAst : ExpressionAst
- [ ] ArrayExpressionAst : ExpressionAst
- [ ] ParenExpressionAst : ExpressionAst, ISupportsAssignment
- [ ] SubExpressionAst : ExpressionAst
- [ ] MemberExpressionAst : ExpressionAst, ISupportsAssignment
- [ ] InvokeMemberExpressionAst : MemberExpressionAst, ISupportsAssignment

## Commands

- [ ] [After CommandAst] CommandExpressionAst : CommandBaseAst
- [ ] CommandParameterAst : CommandElementAst
- [ ] CommandAst : CommandBaseAst

---

# Compiler

- [ ] CompilerGeneratedAst
