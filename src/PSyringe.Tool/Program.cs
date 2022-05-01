using System.Management.Automation.Language;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using PSyringe.Common.Language.Elements;
using PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;
using PSyringe.Language.Parsing;

namespace PSyringe.Tool;

/*

*/
public static class Program {
  public static void Main(string[] args) {
    BenchmarkRunner.Run<Benchy>();
    // var code = File.ReadAllText("C:/Temp/master.ps1");
    // var parser = new ScriptParser(new ElementFactory());
    // var script = parser.Parse(code);
    // var text = script.ScriptBlock.ToStringFromAst();
  }

  /*
   Pre Optimization:
   |          Method |    Mean |    Error |   StdDev |      Gen 0 |     Gen 1 | Allocated |
   |---------------- |--------:|---------:|---------:|-----------:|----------:|----------:|
   | ToStringFromAst | 1.222 s | 0.0145 s | 0.0121 s | 39000.0000 | 1000.0000 |    318 MB |
   ---------------------------------------------------------------------------------------
   Post Optimization:
   |                   Method |      Mean |     Error |    StdDev |    Median |     Gen 0 |    Gen 1 |    Gen 2 | Allocated |
   |------------------------- |----------:|----------:|----------:|----------:|----------:|---------:|---------:|----------:|
   | ToStringFromAstWithParse | 52.439 ms | 1.0369 ms | 2.5631 ms | 51.424 ms | 1000.0000 |        - |        - |     16 MB |
   |          ToStringFromAst |  8.760 ms | 0.1665 ms | 0.1557 ms |  8.831 ms | 1109.3750 | 484.3750 | 187.5000 |      8 MB |
   |                ParseOnly | 43.216 ms | 0.5671 ms | 0.4735 ms | 43.287 ms | 1000.0000 | 416.6667 | 166.6667 |      7 MB |
  */
  [MemoryDiagnoser]
  public class Benchy {
    public static readonly string Code = File.ReadAllText("C:/Temp/master.ps1");
    public static readonly IScriptDefinition ScriptDefinition = GetScriptDefinition();

    [Benchmark]
    public void ToStringFromAstWithParse() {
      var parser = new ScriptParser(new ElementFactory());
      var script = parser.Parse(Code);
      var result = script.ScriptBlock.ToStringFromAst();
    }

    [Benchmark]
    public void ToStringFromAst() {
      var result = ScriptDefinition.ScriptBlock.ToStringFromAst();
    }

    [Benchmark]
    public void ParseOnly() {
      var result = Parser.ParseInput(Code, out _, out _);
    }

    private static IScriptDefinition GetScriptDefinition() {
      var parser = new ScriptParser(new ElementFactory());
      var script = parser.Parse(Code);
      return script;
    }
  }
}