using System.ComponentModel;
using System.Management.Automation.Language;
using PSyringe.Language.Attributes.Base;
  
namespace PSyringe.Language.Attributes; 

/// <summary>
/// Injects a constant value from the script environment into a variable.
/// </summary>
public class InjectConstantAttribute : InjectionTargetAttribute {
  public InjectConstantAttribute(string? Target, bool Optional = false) : base(Target, Optional) {
  }
}