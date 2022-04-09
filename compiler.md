# Compiler Notes
Notes about the script compiler. This is not part of the documentation.

## Injecting Providers

Attributes that inject data into the script require providers that are unknown at the compile time of the script. To avoid re-compiling the script every time it's run, we use a place-holder variable at the point where the provider should be injected. These variables are then set in the PowerShell runspace by the framework before the script is executed.

To make sure that the these variables are unique and not overwritten by the user, they follow the schema:
```PowerShell
$script:ɵɵprov_<Scope>_<Target Name>_inj_<Provider Name>
$script:ɵɵprov_GLOBAL_Logger_inj_ILogger
```
Where:
- `<SCOPE>` is either the name of the function or `GLOBAL` if the variable is part of the global scope.
- `<Target Name>` is the name of the variable / parameter into which the provider will be injected.
- `<Provider Name>` is the name of the provider.

## Provider Scoping

By default all providers are resolved under the script specific configuration scope (Global providers registered in the manager are automatically added to this scope). To get access to different scopes that are either defined in the global scope or restrict the lookup to script specific providers, the `Scope` parameter on injection attributes can be used.

## Injection Sites

Certain functions might require specific providers from a different configuration scope. Rather than having to add the `Scope` option to all providers, an `InjectionSite` can be used in order restrict access only to a specific scope that is defined in the injection site. Additionally, this allows for writing functions that have a default value, set by the framework and can be overridden by the user.

An injection site could look something like this:

```PowerShell
# Input Script
function InjectionSiteExample {
  [InjectionSite(Scope = "SomeScope")]
  param(
    [Inject()][ILogger]$Logger,
    [Inject()][Foo]$Foo = "Foo"
  )
}
```

Like with the Injecting Providers section, the compiler will generate a placeholder variable as the default value for the parameter. If the parameter has a default value, the provider will be marked as optional. If it is available, the provider value will be used. 

```PowerShell
# Compiler Generated
function InjectionSiteExample {
  param(
    [Inject([ILogger])]$Logger = $script:ɵɵprov_InjectionSiteExample_Logger_inj_ILogger,
    [Inject([Foo])]$Foo = "Foo"
  )
  # !! { COMPILER GENERATED CODE
  if($null -ne $script:ɵɵprov_InjectionSiteExample_Foo_inj_Foo) {
    $Foo = $script:ɵɵprov_InjectionSiteExample_Foo_inj_Foo
  }
  # !! } COMPILER GENERATED CODE
}
```

## Handling Errors
Using the `OnError` Attribute, the user can specify a function that will be called when an error occurs. The function will be called with the error message and the name of the function that caused the error. This works by adding a global `trap` to the script which will call the functions.

```PowerShell
# !! { COMPILER GENERATED CODE
trap {
  foreach($OnErrorFn in $script:ɵɵscriptctx.Callbacks.OnError) {
    $FunctionRef = Get-Command -Name $OnErrorFn.Name -Type "Function";
    $FunctionRef.ScriptBlock.Invoke($_);
    # $ɵɵScriptMethodInvocationHandler.Invoke($OnErrorFn.Name, @{ "Error" = $_ });
  }
}
# !! } COMPILER GENERATED CODE
```

## Handling "Variable not found"

It's not uncommon that we're trying to access a variable at runtime that is `null`. Because this could happen with compiler generated variables as well, we catch all `InvalidOperation` exceptions to potentially clean up error messages and provide a more meaningful error context.

```PowerShell
# !! { COMPILER GENERATED CODE
trap [System.Management.Automation.RuntimeException] {
  $Exception = $_;
  if($Exception.CategoryInfo.Category -eq [System.Management.Automation.ErrorCategory]::InvalidOperation) {
    # Todo
  }
}
# !! } COMPILER GENERATED CODE
```