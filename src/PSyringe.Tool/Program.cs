using System.Management.Automation;
using PSyringe.Language.AstTransformation;
using PSyringe.Language.Compiler;
using PSyringe.Language.Parsing;

namespace PSyringe.Tool;

public static class Program {
  public static void Main(string[] args) {
    var filepath = "C:/Temp/master.ps1";
    var parser = new ScriptParser(new ElementFactory());
    var file = File.ReadAllText(filepath);
    
    var script = parser.Parse(file);
    var text = script.ScriptBlock.ToStringFromAst();
    Console.WriteLine(text);
  }
}