using System.Management.Automation.Language;

namespace PSyringe.Common.Exceptions.Language;

public class ScriptParsingException : Exception {
  public ScriptParsingException(string message, ParseError[] errors) : base(message) {
    Errors = errors;
  }

  public ParseError[] Errors { get; }
}