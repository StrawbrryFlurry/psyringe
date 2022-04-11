using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements.Base;
using PSyringe.Common.Language.Parsing;

namespace PSyringe.Language.Elements;

public class ScriptElement : IScriptElement {
  private readonly List<IElement> _elements = new();

  public ScriptElement(ScriptBlockAst ast) {
    ScriptBlockAst = ast;
  }

  public ScriptBlockAst ScriptBlockAst { get; }

  public IEnumerable<IElement> Elements => _elements;

  public void AddElement(IElement element) {
    _elements.Add(element);
  }
}