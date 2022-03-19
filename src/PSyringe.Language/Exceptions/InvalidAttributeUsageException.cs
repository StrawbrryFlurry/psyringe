using System.Management.Automation.Language;
using PSyringe.Language.Extensions;

namespace PSyringe.Language.Exceptions;

public class InvalidAttributeUsageException : Exception {
  public InvalidAttributeUsageException(string message) : base(message) {
  }

  public static InvalidAttributeUsageException NonStaticParameter(AttributeAst attributeAst, string parameterName) {
    var type = GetAttributeName(attributeAst);
    return new InvalidAttributeUsageException($"Non-static parameters are not supported for attribute '{type}' " +
                                              $"on line {attributeAst.Extent.StartLineNumber}" +
                                              $"for parameter '{parameterName}'");
  }

  public static InvalidAttributeUsageException NonStaticParameter(AttributeAst attributeAst, int parameterIdx) {
    var type = GetAttributeName(attributeAst);
    return new InvalidAttributeUsageException($"Non-static parameters are not supported for attribute '{type}' " +
                                              $"on line {attributeAst.Extent.StartLineNumber}" +
                                              $"for parameter at index {parameterIdx}");
  }

  private static string GetAttributeName(AttributeAst attributeAst) {
    return attributeAst.GetAttributeType().Name;
  }
}