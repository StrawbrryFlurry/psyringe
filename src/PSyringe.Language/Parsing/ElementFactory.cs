using System.Management.Automation.Language;
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
    // All elements require get access to their AST node through the constructor.
    // The constructor of an element should only consist of one parameter, the ast node.
    // TODO: Determine if IElement should be an abstract class that has a constructor for the Ast node.
    var elementCtorArgs = new object[] {ast};

    var instance = (ScriptElement) Activator.CreateInstance(elementType, elementCtorArgs)!;
    return instance;
  }
}