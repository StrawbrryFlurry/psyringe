using System.Management.Automation.Language;

namespace PSyringe.Language.Compiler;

public class CompilerScriptExtent : IScriptExtent {
  public CompilerScriptExtent(string text) {
    Text = text;
  }

  public string Text { get; }

  public IScriptPosition EndScriptPosition { init; get; }

  public int EndColumnNumber => EndScriptPosition.ColumnNumber;
  public int EndLineNumber => EndScriptPosition.LineNumber;
  public int EndOffset => EndScriptPosition.Offset;

  public IScriptPosition StartScriptPosition { init; get; }

  public int StartColumnNumber => StartScriptPosition.ColumnNumber;
  public int StartLineNumber => StartScriptPosition.LineNumber;
  public int StartOffset => StartScriptPosition.Offset;
}