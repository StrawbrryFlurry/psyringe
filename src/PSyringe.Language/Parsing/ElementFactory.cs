using System.Management.Automation.Language;
using System.Reflection;
using System.Runtime.Serialization;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements.Base;
using PSyringe.Language.Elements;

namespace PSyringe.Language.Parsing;

public class ElementFactory : IElementFactory {
  private readonly MethodInfo _createElementMethodInfo;

  public ElementFactory() {
    // The `ICanCreateAssociatedElement` interface is
    // the base type for all PowerShell attributes that
    // are provided with PSyringe. It has a single method
    // that all these attributes inherit to create the
    // element for it's associated script element.
    //
    // E.g. InjectAttribute.CreateElement => InjectElement
    //
    // We store the MethodInfo in this property such that
    // we don't need to do reflection every time we need
    // to create an element.
    _createElementMethodInfo = typeof(ICanCreateAssociatedElement<>).GetMethods().First();
  }

  public IScriptElement CreateScript(ScriptBlockAst ast) {
    return new ScriptElement(ast);
  }

  public T CreateElement<T, TA>(TA ast, Type attribute) where T : IElement<TA> where TA : Ast {
    var instance = MakeDummyMethodTarget(attribute);
    var method = GetCreateElementMethod(attribute);

    var createElementArgs = new object[] {ast};

    return (T) method.Invoke(instance, createElementArgs)!;
  }

  private object MakeDummyMethodTarget(Type type) {
    return FormatterServices.GetUninitializedObject(type);
  }

  private MethodInfo GetCreateElementMethod(Type attribute) {
    return attribute.GetMethod(_createElementMethodInfo.Name)!;
  }
}