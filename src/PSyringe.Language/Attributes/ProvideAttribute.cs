using PSyringe.Common.Language.Attributes;

namespace PSyringe.Language.Attributes;

public class ProvideAttribute : IProvideTargetAttribute {
  public string ProviderName { get; set; }
}