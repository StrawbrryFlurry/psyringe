using PSyringe.Common.Language.Attributes;

namespace PSyringe.Language.Attributes.Base; 

/// [Log("{TargetValue} was assigned to {TargetName}")]$SomeVariable = "SomeValue";
/// => $__PS_DEFAULT_LOGGER__.Log("""$('$SomeVariable')"" = ""$SomeValue""");
public class LoggingTargetAttribute : Attribute, ILoggingTarget  {
  
}