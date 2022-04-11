using PSyringe.Common.Language.Elements.Base;

namespace PSyringe.Common.Language.Attributes;

public interface ICanCreateAssociatedElement<out T> where T : IElement {
}