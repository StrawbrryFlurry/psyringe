# Class

- [x] BaseCtorInvokeMemberExpressionAst : InvokeMemberExpressionAst
- [x] PropertyMemberAst : MemberAst
- [x] FunctionMemberAst : MemberAst,
- [x] TypeDefinitionAst : StatementAst

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
- [x] FunctionDefinitionAst : StatementAst, IParameterMetadataProvider
- [x] DataStatementAst : StatementAst `Data Foo { }`
- [x] ConfigurationDefinitionAst `Configuration Foo { }`
- [x] DynamicKeywordStatementAst : StatementAst

---

# Done

- [x] NamedBlockAst : Ast
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
- [x] ScriptBlockExpressionAst : ExpressionAst
- [x] ArrayExpressionAst : ExpressionAst
- [x] ParenExpressionAst : ExpressionAst, ISupportsAssignment
- [x] SubExpressionAst : ExpressionAst
- [x] MemberExpressionAst : ExpressionAst, ISupportsAssignment
- [x] InvokeMemberExpressionAst : MemberExpressionAst, ISupportsAssignment

## Commands

- [x] [After CommandAst] CommandExpressionAst : CommandBaseAst
- [x] CommandParameterAst : CommandElementAst
- [x] CommandAst : CommandBaseAst

---

# Compiler

- [ ] CompilerGeneratedAst
