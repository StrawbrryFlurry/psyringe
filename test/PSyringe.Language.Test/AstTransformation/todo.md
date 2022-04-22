- [ ] SequencePointAst : Ast
- [ ] NamedBlockAst : Ast
- [ ] ScriptBlockExpressionAst : ExpressionAst
- [ ] ArrayExpressionAst : ExpressionAst
- [ ] ParenExpressionAst : ExpressionAst, ISupportsAssignment
- [ ] SubExpressionAst : ExpressionAst

# Weird

- [ ] DataStatementAst : StatementAst `Data Foo { }`
- [ ] ConfigurationDefinitionAst `Configuration Foo { }`
- [ ] DynamicKeywordStatementAst : StatementAst

# Class

- [ ] MemberExpressionAst : ExpressionAst, ISupportsAssignment
- [ ] InvokeMemberExpressionAst : MemberExpressionAst, ISupportsAssignment
- [ ] BaseCtorInvokeMemberExpressionAst : InvokeMemberExpressionAst
- [ ] PropertyMemberAst : MemberAst
- [ ] FunctionMemberAst : MemberAst,
- [ ] TypeDefinitionAst : StatementAst

---

# Pipeline

- [x] PipelineAst : ChainableAst
- [x] AssignmentStatementAst : PipelineBaseAst
- [x] PipelineChainAst : ChainableAst
- [x] ErrorStatementAst : PipelineBaseAst
- [x] AssignmentStatementAst : PipelineBaseAst

---

# statements

- [x] StatementBlockAst : Ast IParameterMetadataProvider
- [x] IfStatementAst : StatementAst
- [x] ForEachStatementAst : LoopStatementAst
- [x] ForStatementAst : LoopStatementAst
- [x] DoWhileStatementAst : LoopStatementAst
- [x] DoUntilStatementAst : LoopStatementAst
- [x] WhileStatementAst : LoopStatementAst
- [x] SwitchStatementAst : LabeledStatementAst
- [x] UsingStatementAst : StatementAst
- [x] TryStatementAst : StatementAst
- [x] CatchClauseAst : Ast
- [x] TrapStatementAst : StatementAst
- [x] BreakStatementAst : StatementAst
- [x] ContinueStatementAst : StatementAst
- [x] ReturnStatementAst : StatementAst
- [x] ExitStatementAst : StatementAst
- [x] ThrowStatementAst : StatementAst
- [x] BlockStatementAst : StatementAst
- [ ] FunctionDefinitionAst : StatementAst, IParameterMetadataProvider

---

# Done

- [x] AttributedExpressionAst : ExpressionAst, ISupportsAssignment, IAssignableValue
- [x] ConvertExpressionAst : AttributedExpressionAst, ISupportsAssignment
- [x] TypeExpressionAst : ExpressionAst
- [x] VariableExpressionAst : ExpressionAst, ISupportsAssignment, IAssignableValue
- [x] ConstantExpressionAst : ExpressionAst
- [x] StringConstantExpressionAst : ConstantExpressionAst
- [x] ExpandableStringExpressionAst : ExpressionAst
- [x] ArrayLiteralAst : ExpressionAst, ISupportsAssignment
- [x] UsingExpressionAst : ExpressionAst
- [x] IndexExpressionAst : ExpressionAst, ISupportsAssignment
- [x] TernaryExpressionAst : ExpressionAst
- [x] BinaryExpressionAst : ExpressionAst
- [x] UnaryExpressionAst : ExpressionAst
- [x] NamedAttributeArgumentAst : Ast
- [x] AttributeAst : AttributeBaseAst
- [x] TypeConstraintAst : AttributeBaseAst
- [x] ParameterAst : Ast
- [x] ErrorExpressionAst : ExpressionAst
- [x] MergingRedirectionAst : RedirectionAst
- [x] FileRedirectionAst : RedirectionAst
- [x] ParamBlockAst : Ast
- [x] ScriptBlockAst : Ast, IParameterMetadataProvider
- [x] HashtableAst : ExpressionAst

## Commands

- [x] [After CommandAst] CommandExpressionAst : CommandBaseAst
- [x] CommandParameterAst : CommandElementAst
- [x] CommandAst : CommandBaseAst

---

# Compiler

- [ ] CompilerGeneratedAst
