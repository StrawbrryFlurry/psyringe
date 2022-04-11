using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Attributes;

public interface IPSyringeAttribute<T> : ICanCreateAssociatedElement<T> where T : Ast {
}