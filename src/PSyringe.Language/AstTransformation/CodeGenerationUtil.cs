using System.Reflection;

namespace PSyringe.Language.AstTransformation;

public static class CodeGenerationUtil {
  /// <summary>
  ///   Sets the value of a property even if that property doesn't
  ///   have a setter.
  ///   <b>
  ///     Only use this in cases where there is no other option
  ///     to update the value!
  ///   </b>
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="propertyName"></param>
  /// <param name="value"></param>
  public static void SetProperty(object obj, string propertyName, object value) {
    var backingField = GetPropertyBackingField(obj.GetType(), propertyName);
    backingField.SetValue(obj, value);
  }

  public static FieldInfo GetPropertyBackingField(Type type, string propertyName) {
    var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
    var backingFieldName = $"<{propertyName}>k__BackingField";

    return type.GetField(backingFieldName, bindingFlags)!;
  }
}