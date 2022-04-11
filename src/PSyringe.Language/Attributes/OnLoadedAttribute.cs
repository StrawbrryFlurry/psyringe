using PSyringe.Common.Language.Attributes;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

[PSAttributeUsage(Function)]
public class OnLoadedAttribute : Attribute, IPSyringeAttribute<OnLoadedCallbackElement> {
}