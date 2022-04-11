using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements.Base;

namespace PSyringe.Language.Elements;

public class ScriptElement : IScriptElement {
  private readonly List<IElement<Ast>> _elements = new();

  public ScriptElement(ScriptBlockAst ast) {
    ScriptBlockAst = ast;
  }

  public ScriptBlockAst ScriptBlockAst { get; }

  public IEnumerable<IElement<Ast>> Elements => _elements;

  public void AddElement(IElement<Ast> element) {
    _elements.Add(element);
  }
}