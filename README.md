# 💉 PSyringe

PSyringe is a pre-processor, management tool and "runtime" for PowerShell scripts.

## Warning!!
This framework, by design, enables **Arbitrary (Remote) Code Execution** if you expose it to an external layer! Please make sure you take the necessary precautions to protect your system. Only allow authenticated users restricted access to manage and call scripts loaded by PSyringe.

# Goals

Psyringe allows you to write less repetitive, secure PowerShell scripts by passing these concerns to the runtime.

## Stop putting clear text secrets in your scripts
```powershell
[InjectCredential("SomeApiKey")][SecureString]$RemoteApiKey;
[InjectCredential("SomeCredential")][PsCredential]$UserCredential;
```

## Enable more dynamic logging
Configure dynamic logging to any target through the framework and re-use it across all your scripts.
```powershell
[Inject([IPsLogger])][IPsLogger]$Logger;
$Logger.Info("Hi!");
[LogExpression(Type = "Info")]$SomeVariable = "Hi!";
# Logs: [21:05-15.03.2022] Value "Hi!" was assigned to variable "SomeVariable"

function SomeFunction {
  [LogInvocation(WithParams = $false)]
  param([string]$a)
}
# Logs: [21:05-15.03.2022] Function "SomeFunction" was invoked.
# WithParams: [21:05-15.03.2022] Function "SomeFunction" was invoked with parameters: [$a = "Foo"];
```

## Hook into your script's lifetime
```powershell
function HandleError {
  [OnError()]
  param([Exception]$Error)
  # HandleError
}

function OnLoaded {
  [OnLoaded()]
  param()
  # Do whatever when script is ready to be run
}

function BeforeUnload {
  [BeforeUnload()]
  param()
  # Is called after the script was run. Do any cleanup work here
}
```

## Inject any dependencies directly into your script
Like we've seen before with in the logging example, you are able to inject any kind of dependency from the runtime into your script.
```powershell
[Inject([IPsLogger])][IPsLogger]$Logger;
[Inject(Target = "SomeCustomProvider")][string]$SomeProvider;
```

## Make connecting to databases easier
```powershell
[InjectDatabase]("KnownDatabase")[DbContext]$DbConnection;
[InjectDatabase(ConnectionString = "...")][DbContext]$DbConnection;
```

## Invoke your scripts with dynamic parameters
```powershell
function StartFunction {
  [StartupFunction()]
  param(
    [InjectParameter("SomeParameter")]
    [string]$SomeParameter,
    [InjectParameter("SomeOtherParameter")]
    [int]$SomeOtherParameter = 10 # Default if the parameter is not provided
  )
}
```

## Inject providers directly into your functions as defaults
Injection sites, like the startup function, allow us to inject dynamic dependencies to functions as well as calling them like we're used to. We can also specify a scope in which the runtime will look for the dependency.
```powershell
function Get-Function {
  [InjectionSite(Scope = "Global")]
  param(
    [Inject("SomeProvider")]
    [string]$SomeProvider
    [string]$SomeParameter
  )
}

Get-Function -SomeParameter "Foo" -SomeProvider "Bar" # Works
Get-Function -SomeParameter "Foo" # Uses the default provided by the framework.
```

## ... And there is much more to come

# Usage
Example usage. The API is likely subject to change.
```cs
using PSyringe.Core;
using PSyringe.Core.DI;
using PSyringe.Language.Parsing;

var parser = new ScriptParser();
var loader = new ScriptLoader();
var repository = new ScriptRepository();

var sm = new ScriptManager(parser, loader, repository);
var script = @"
  function Startup {
    [StartupFunction()]
    param(
      [Inject([ILogger])]
      [ILogger]$Logger
      [InjectParameter(""Foo"")]
      [string]$Foo
    )
    $Logger.Info($Foo);
  }
"

var scriptId = sm.Load(script);
IInvokableScript scriptRef = sm.GetScriptById(scriptId);
IScriptContext context = new ScriptContext {
  Params = {
    Foo = "Foo"
  }
};

var scriptInvocationResult = await scriptRef.Invoke(context);
```