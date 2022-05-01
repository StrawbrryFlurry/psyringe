namespace PSyringe.Common.Language.Attributes;

public class TransformActionAttribute {
}

[Flags]
public enum TransformAction {
  None,
  UpdateParent,
  UpdateSelf,
  UpdateChild,

  /// <summary>
  ///   The attribute inserts a statement below the attributed element.
  /// </summary>
  InsertBelow,

  /// <summary>
  ///   The attribute inserts a statement above the attributed element.
  /// </summary>
  InsertAbove
}