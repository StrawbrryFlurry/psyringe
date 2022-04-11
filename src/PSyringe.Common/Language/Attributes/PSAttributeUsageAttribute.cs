namespace PSyringe.Common.Language.Attributes;

// ReSharper disable once InconsistentNaming
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class PSAttributeUsageAttribute : Attribute {
  public PSAttributeUsageAttribute(PSAttributeTargets target) {
    Target = target;
  }

  public PSAttributeTargets Target { get; }
}

[Flags]
public enum PSAttributeTargets {
  Function,
  Parameter,
  ScriptBlock,
  Variable
}