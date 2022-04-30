namespace PSyringe.Common.Language.Attributes;

// ReSharper disable once InconsistentNaming
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class PSAttributeUsageAttribute : Attribute {
  public PSAttributeTargets Target { get; }

  public PSAttributeUsageAttribute(PSAttributeTargets target) {
    Target = target;
  }
}

[Flags]
public enum PSAttributeTargets {
  Function,
  Parameter,
  ScriptBlock,
  Variable
}