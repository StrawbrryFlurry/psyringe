using PSyringe.Common.DI;

namespace PSyringe.Common.Runtime;

public static class ScriptConstants {
  public const string VariablePrefix = "ɵɵ";
  public const string GlobalScopeName = "GLOBAL";

  public static string MakeInjectVariable(string variableName, IScriptProvider provider, string? variableScope) {
    variableScope ??= GlobalScopeName;
    var providerName = provider.GetProviderName();
    return $"{VariablePrefix}prov_{variableScope}_{variableName}_inj_{providerName}";
  }
}