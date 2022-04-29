using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class InjectDatabaseElement : ScriptElement {
  public InjectDatabaseElement(Ast ast, AttributeAst attribute) : base(ast, attribute) {
  }


  public override Ast? TransformAst<T>(T source) {
    throw new NotImplementedException();
  }
}