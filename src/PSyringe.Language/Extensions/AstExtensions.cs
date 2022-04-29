using System.Management.Automation.Language;

namespace PSyringe.Language.Extensions;

public static class AstExtensions {
  public static T? FindOfType<T>(this Ast ast) where T : Ast {
    return ast.Find(a => a is T, true) as T;
  }

  public static IEnumerable<T> FindAllOfType<T>(this Ast ast) where T : Ast {
    return (IEnumerable<T>) ast.FindAll(a => a is T, true);
  }

  public static T CopyAs<T>(this Ast ast) where T : Ast {
    return (T) ast.Copy();
  }
}