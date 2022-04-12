using PSyringe.Common.Language.Elements;

namespace PSyringe.Common.Language.Attributes;

public interface ICanCreateAssociatedElement<out T> where T : ScriptElement {
}