using System.Management.Automation.Language;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using PSyringe.Language.AstTransformation;
using PSyringe.Language.Parsing;

namespace PSyringe.Tool;

/// <summary>
///   |          Method |    Mean |    Error |   StdDev |      Gen 0 |     Gen 1 | Allocated |
///   |---------------- |--------:|---------:|---------:|-----------:|----------:|----------:|
///   | ToStringFromAst | 1.222 s | 0.0145 s | 0.0121 s | 39000.0000 | 1000.0000 |    318 MB |
/// </summary>
public static class Program {
  public static void Main(string[] args) {
    BenchmarkRunner.Run<Benchy>();
  }

  [MemoryDiagnoser]
  public class Benchy {
    public static ScriptExtent Extent = new(null, null);

    public static string Code = File.ReadAllText("C:/Temp/master.ps1");

    [Benchmark]
    public string ToStringFromAst() {
      var parser = new ScriptParser(new ElementFactory());
      var script = parser.Parse(Code);
      var text = script.ScriptBlock.ToStringFromAst();
      return text;
    }
  }
}