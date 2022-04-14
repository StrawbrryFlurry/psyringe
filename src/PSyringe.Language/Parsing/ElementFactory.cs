using System.Management.Automation.Language;
using System.Reflection;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Elements;
using PSyringe.Common.Language.Parsing;
using PSyringe.Language.Elements;
using PSyringe.Language.Extensions;

namespace PSyringe.Language.Parsing;

public class ElementFactory : IElementFactory {
  private readonly string _canCreateAssociatedElementInterfaceName;

  public ElementFactory() {
    // We store the interface name in this property such that
    // we don't need to do reflection every time we need
    // to create an element.
    _canCreateAssociatedElementInterfaceName = typeof(ICanCreateAssociatedElement<>).Name;
  }

  public IScriptDefinition CreateScript(ScriptBlockAst ast) {
    return new ScriptDefinition(ast);
  }

  public ScriptElement CreateElement<T>(IAttributedScriptElement<T> element) where T : Ast {
    // All PSyringe attributes must derive from `IPSyringeAttribute`
    // which in itself requires a type parameter for `ICanCreateAssociatedElement<>`.
    // Using the type parameter defined for that interface, we can determine,
    // which element the attribute needs to create.
    var attribute = element.Attribute.GetAttributeType();
    // Get the generic interface implemented by the attribute
    var genericCreateElementInterface = attribute?.GetInterface(_canCreateAssociatedElementInterfaceName);

    if (genericCreateElementInterface is null) {
      throw new Exception(
        $"Attribute {element.Attribute.TypeName.Name} does not implement {_canCreateAssociatedElementInterfaceName} and is therefore not a valid PSyringe Attribute");
    }

    var ast = element.Ast;
    // The `ICanCreateAssociatedElement<>` only takes one type parameter,
    // which is the type of the element that this attribute creates.
    // e.g. InjectAttribute implements IPSyringeAttribute<InjectElement>
    var elementType = genericCreateElementInterface.GetGenericArguments().First();

    // All elements extend `ScriptElement` whose constructor only takes 
    // an Ast. That Ast being the attributed script element the parser
    // associated the element with.
    var ctorWithAstAndAttribute = elementType.GetConstructor(new[] {typeof(Ast), typeof(AttributeAst)});

    if (ctorWithAstAndAttribute is not null) {
      return CreateElementInstance(ctorWithAstAndAttribute, ast, element.Attribute);
    }

    var ctorWithAst = elementType.GetConstructor(new[] {typeof(Ast)});

    if (ctorWithAst is not null) {
      return CreateElementInstance(ctorWithAst, ast);
    }

    throw new Exception(
      "Could not find a constructor overload that matches the parameters required by ScriptElement. " +
      "Descendants of ScriptElement must not have a different constructor signature than the ones supported by " +
      "ScriptElement.");
  }

  private ScriptElement CreateElementInstance(ConstructorInfo ctor, params object[] ctorArgs) {
    return (ScriptElement) ctor.Invoke(ctorArgs);
  }
}