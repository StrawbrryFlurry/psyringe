using System.Management.Automation.Language;

namespace PSyringe.Language.Compiler;

/// <summary>
///   A custom ScriptExtent whose Text property shows the
///   whole script text, unlike the public `ScriptExtent` of
///   the PowerShell sdk that is restricted to one line.
///   Use the "real" script positions of the extent gathered
///   from the ast to init this extent such that error messages
///   match up with the expected line numbers in the users script.
/// </summary>
// public class CompilerScriptExtent : IScriptExtent {
//   public CompilerScriptExtent(string text) {
//     Text = text;
//   }
// 
//   public string? File => null;
// 
//   /// <summary>
//   ///   Compiled script text that is going to be executed
//   ///   by the PowerShell engine.
//   /// </summary>
//   public string Text { get; }
// 
//   /// <summary>
//   ///   The EndScriptPosition of the actual script extent.
//   /// </summary>
//   public IScriptPosition EndScriptPosition { init; get; }
// 
//   public int EndColumnNumber => EndScriptPosition.ColumnNumber;
//   public int EndLineNumber => EndScriptPosition.LineNumber;
//   public int EndOffset => EndScriptPosition.Offset;
// 
//   /// <summary>
//   ///   The StartScriptPosition of the actual script extent.
//   /// </summary>
//   public IScriptPosition StartScriptPosition { init; get; }
// 
//   public int StartColumnNumber => StartScriptPosition.ColumnNumber;
//   public int StartLineNumber => StartScriptPosition.LineNumber;
//   public int StartOffset => StartScriptPosition.Offset;
// }