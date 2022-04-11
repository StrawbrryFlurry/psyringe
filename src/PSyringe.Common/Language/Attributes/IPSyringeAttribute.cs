using PSyringe.Common.Language.Elements.Base;

namespace PSyringe.Common.Language.Attributes;

public interface IPSyringeAttribute<out T> : ICanCreateAssociatedElement<T> where T : IElement {
}