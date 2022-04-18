- [ ] SequencePointAst : Ast
- [ ] ScriptBlockAst : Ast, IParameterMetadataProvider
- [ ] [After Statement] NamedBlockAst : Ast
- [ ] CatchClauseAst : Ast
- [ ] [After SBAst] ScriptBlockExpressionAst : ExpressionAst
- [ ] HashtableAst : ExpressionAst
- [ ] [After StatementAst] ArrayExpressionAst : ExpressionAst
- [ ] [After PipelineAst] ParenExpressionAst : ExpressionAst, ISupportsAssignment
- [ ] [After SB] SubExpressionAst : ExpressionAst

# Class

- [ ] [After CommandElementAst] MemberExpressionAst : ExpressionAst, ISupportsAssignment
- [ ] [After CommandElementAst] InvokeMemberExpressionAst : MemberExpressionAst, ISupportsAssignment
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
- [ ] ForStatementAst : LoopStatementAst
- [ ] DoWhileStatementAst : LoopStatementAst
- [ ] DoUntilStatementAst : LoopStatementAst
- [ ] WhileStatementAst : LoopStatementAst
- [ ] SwitchStatementAst : LabeledStatementAst
- [ ] UsingStatementAst : StatementAst
- [ ] TryStatementAst : StatementAst
- [x] TrapStatementAst : StatementAst
- [x] BreakStatementAst : StatementAst
- [x] ContinueStatementAst : StatementAst
- [x] ReturnStatementAst : StatementAst
- [x] ExitStatementAst : StatementAst
- [x] ThrowStatementAst : StatementAst
- [ ] DataStatementAst : StatementAst

- [ ] ConfigurationDefinitionAst : StatementAst
- [ ] DynamicKeywordStatementAst : StatementAst
- [ ] BlockStatementAst : StatementAst
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

## Commands

- [x] [After CommandAst] CommandExpressionAst : CommandBaseAst
- [x] CommandParameterAst : CommandElementAst
- [x] CommandAst : CommandBaseAst

---

# Compiler

- [ ] CompilerGeneratedAst
