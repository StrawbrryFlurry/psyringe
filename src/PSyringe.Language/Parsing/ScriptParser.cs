using System.Management.Automation.Language;
using System.Text;
using PSyringe.Common.Language.Parsing;
using PSyringe.Language.Attributes;

namespace PSyringe.Language.Parsing;

public class ScriptParser : IScriptParser {
  public ScriptParser(IScriptVisitor visitor) {
    Visitor = visitor;
  }

  internal string ScriptBeforeParsing { get; private set; } = "";
  internal ScriptBlockAst ScriptAst { get; private set; }
  internal IScriptVisitor Visitor { get; }

  internal IScriptElement Script { get; private set; }

  public IScriptElement Parse(string script) {
    PrepareAndParseScript(script);
    VisitScriptAst();
    return CreateScriptElement();
  }

  private IScriptElement CreateScriptElement() {
    Script = CreateScriptElementFromVisitor();

    return default;
  }

  private IScriptElement CreateScriptElementFromVisitor() {
    var builder = new ElementBuilder(new ElementFactory());
    return builder.Build();
  }

  private void VisitScriptAst() {
    Visitor.Visit(ScriptAst);
  }

  private void PrepareAndParseScript(string script) {
    PrependAssemblyReference(ref script);
    ScriptAst = ParseScriptToAst(script);
  }

  private ScriptBlockAst ParseScriptToAst(string script) {
    ScriptBeforeParsing = script;
    var ast = Parser.ParseInput(script, out var tokens, out var errors);
    return ast;
  }

  internal static void PrependAssemblyReference(ref string script) {
    var assemblyName = GetAttributeAssemblyNamespace();
    var sb = new StringBuilder();
    sb.AppendLine($"using namespace {assemblyName};");
    sb.AppendLine(script);
    script = sb.ToString();
  }

  private static string GetAttributeAssemblyNamespace() {
    var type = typeof(InjectionSiteAttribute);
    return type.Namespace!;
  }
}
/*
 *   private void AddInjectionSite(FunctionDefinitionAst ast) {
    var site = ElementFactory.CreateInjectionSite(ast);
    InjectionSites.Add(site);
    AddInjectionSiteParameters(ast, site);
  }

  private void AddInjectionSiteParameters(FunctionDefinitionAst ast, InjectionSiteElement site) {
    var parameterBlock = GetFunctionParameterBlock(ast)!;
    
    foreach (var parameter in parameterBlock.Parameters) {
      ElementFactory.AddParameterToInjectionSite(site, parameter);
    }
  }

  private bool IsInjectTemplateExpression(AttributedExpressionAst ast) {
    return ast.Attribute.IsOfType<InjectTemplateAttribute>();
  }
  
  private bool IsInjectionSite(FunctionDefinitionAst ast) {
    if(!FunctionHasParameterBlock(ast))  {
      return false;
    }
    
    var functionAttributes = GetFunctionAttributes(ast);
    return functionAttributes.Any(IsInjectionSiteAttribute);
  }

  private bool FunctionHasParameterBlock(FunctionDefinitionAst ast) {
    return GetFunctionParameterBlock(ast) is not null;
  }

  private bool IsInjectVariableExpression(AttributedExpressionAst ast) {
    if (!IsVariableExpression(ast)) {
      return false;
    }

    return IsInjectAttribute(ast.Attribute);
  }

  private void AddInjectVariable(AttributedExpressionAst ast) {
    var variable = ElementFactory.CreateInjectionVariable(ast);
    InjectionVariables.Add(variable);
  }

  private void AddInjectTemplate(AttributedExpressionAst ast) {
    var template = ElementFactory.CreateInjectTemplate(ast);
    InjectionTemplates.Add(template);
  }
  
  private bool IsVariableExpression(AttributedExpressionAst ast) {
    return ast.Child is VariableExpressionAst;
  }
  
  private IReadOnlyCollection<AttributeBaseAst> GetFunctionAttributes(FunctionDefinitionAst ast) {
    var parameterBlock = GetFunctionParameterBlock(ast);
    return parameterBlock?.Attributes ?? MakeEmptyReadOnlyCollection<AttributeBaseAst>();
  }

  private IReadOnlyCollection<T> MakeEmptyReadOnlyCollection<T>() {
    return new ReadOnlyCollection<T>(new List<T>());
  }

  private ParamBlockAst? GetFunctionParameterBlock(FunctionDefinitionAst ast) {
    return ast.Body.ParamBlock;
  }

  private bool IsInjectionSiteAttribute(AttributeBaseAst ast) {
    return ast.IsOfType<InjectionSiteAttribute>();
  }

  private bool IsInjectAttribute(AttributeBaseAst ast) {
    return ast.IsOfType<InjectAttribute>();
  }
 */