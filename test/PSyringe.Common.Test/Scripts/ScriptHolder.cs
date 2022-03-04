namespace PSyringe.Core.Test.Scripts;

public class ScriptHolder {
  public const string ExampleScript = @"
class Foo {
}

[string]$JustSomeString; 
[string]$JustSomeString = """"; 
[Inject([string])][string]$GloballyInjectedString; 
[Inject([Foo])][Foo]$GloballyInjectedFoo; 

function __main__ {
[Startup()]
[InjectionTarget(Scope = ""Script"")]
param(
    [Inject(""Local.Foo"")]
  $Foo,
[Inject(Target = ""Bar"")]
[string]
$Bar = ""DEFAULT""
)

[string]$Some = """";
}";
}