using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Parsing.Elements.Base;

public interface IElement {
  public Ast Ast { get; }
}