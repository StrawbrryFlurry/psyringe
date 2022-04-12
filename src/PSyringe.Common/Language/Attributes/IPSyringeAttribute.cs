using PSyringe.Common.Language.Elements;

namespace PSyringe.Common.Language.Attributes;

public interface IPSyringeAttribute<out T> : ICanCreateAssociatedElement<T> where T : ScriptElement {
}