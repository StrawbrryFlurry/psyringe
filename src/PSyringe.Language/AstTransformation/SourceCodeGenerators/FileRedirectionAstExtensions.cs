using System.Management.Automation.Language;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class FileRedirectionAstExtensions {
  public static string ToStringFromAst(this FileRedirectionAst ast) {
    var append = ast.Append;
    var location = ast.Location.ToStringFromAst();
    var stream = GetRedirectionStream(ast.FromStream);

    var redirectionChar = append ? ">>" : ">";

    // "Something" > file.txt
    // "Something" 2> file.txt
    return $"{stream}{redirectionChar} {location}";
  }

  /// <summary>
  ///   Returns the redirection steam value
  ///   or an empty string if the stream is
  ///   the default output stream.
  /// </summary>
  private static string GetRedirectionStream(RedirectionStream stream) {
    if (stream == RedirectionStream.Output) {
      return string.Empty;
    }

    var streamEnumValue = (int) stream;
    return streamEnumValue.ToString();
  }
}