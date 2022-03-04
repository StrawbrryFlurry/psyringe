namespace PSyringe.Core;

/// <summary>
///   Load runner dependencies at the top of the script
///   Parse Script
///   - Find all Inject variable nodes
///   - Find the script entrypoint function
///   | Find all parameters to inject script arguments into
///   | Find all parameters to inject DI providers into
///   Validate all required providers exist [Take optional into account]
///   Replace injection targets in the script AST
///   Load the PowerShell process
///   - Add internal dependencies
///   - Load global injection target variables
///   - Load script templates
///   - Load the new Script AST
///   - Add Command that calls the entrypoint method with injection paramters
///   Invoke the script
/// </summary>
/// InjectionSiteExample
/// function InjectionSiteExample {
/// [InjectionSite()]
/// param(
/// [Inject([Foo])]$Foo
/// )
/// }
/// 
/// InjectionSiteGenerated
/// // Generated code
/// // Start of script
/// $PS_PROVIDE_InjectionSiteExample_INJECT_Foo = // Instance of Foo;
/// 
/// function InjectionSiteExample {
/// param(
/// $Foo = $PS_INJECT_InjectionSiteExample_INJECT_Foo
/// )
/// }
public class ScriptManager {
  public ScriptManager(
    IScriptLoader loader,
    IScriptParser parser,
    IScriptRepository repository
  ) {
  }

  public IReadOnlyCollection<T> GetScripts<T>() {
    return default;
  }

  public T GetInvokableScript<T>() {
    return default;
  }

  public void DisableScript<T>(T script) {
  }


  public interface IScriptRepository {
    IReadOnlyCollection<T> GetScripts<T>();
    void DisableScript<T>(T script);
  }

  public interface IScriptParser {
  }

  public interface IScriptLoader {
    public void Load(string script);
    public void LoadAll(IList<string> scripts);
  }
}