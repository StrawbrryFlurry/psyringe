using PSyringe.Common.Language.Attributes;

namespace PSyringe.Core.Language.Attributes;

public class ProvideAttribute : IProvideTargetAttribute {
  public string ProviderName { get; set; }
}