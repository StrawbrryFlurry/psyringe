using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using PSyringe.Language.TypeLoader.Parameters;
using Xunit;

namespace PSyringe.Language.Test.TypeLoader.Parameters;

public class ParameterCollectionTest {
  [Fact]
  public void TryGetMethodOverloadArguments_ReturnsTrue_WhenOverloadsMatch() {
    var parameters = GetParameterListStringFoo();
    var parameterArguments = new List<PositionalParameter> {
      new() {
        Position = 0,
        Value = "",
        Type = typeof(string)
      }
    };

    var valid = ParameterCollection.TryValidateParameterOverload(
      parameters,
      parameterArguments,
      out _
    );

    valid.Should().BeTrue();
  }

  [Fact]
  public void TryGetMethodOverloadArguments_ReturnsFalse_WhenOverloadDoesNotMatch() {
    var parameters = GetParameterListStringFoo();
    var parameterArguments = new List<PositionalParameter> {
      new() {
        Position = 0,
        Value = 1,
        Type = typeof(int)
      }
    };

    var valid = ParameterCollection.TryValidateParameterOverload(
      parameters,
      parameterArguments,
      out _
    );

    valid.Should().BeFalse();
  }

  [Fact]
  public void TryGetMethodOverloadArguments_ReturnsTrue_WhenOptionalParameterIsMissing() {
    var parameters = GetParameterListStringFooOptionalDecimalFrank();
    var parameterArguments = new List<PositionalParameter> {
      new() {
        Position = 0,
        Value = "Foo",
        Type = typeof(string)
      }
    };

    var valid = ParameterCollection.TryValidateParameterOverload(
      parameters,
      parameterArguments,
      out _
    );

    valid.Should().BeTrue();
  }

  [Fact]
  public void
    TryGetMethodOverloadArguments_FillsRemainingOptionalArgumentsWithZeros_WhenOptionalArgumentsHaveNoValue() {
    var parameters = GetParameterListStringFooOptionalDecimalFrank();
    var parameterArguments = new List<PositionalParameter> {
      new() {
        Position = 0,
        Value = "Foo",
        Type = typeof(string)
      }
    };

    ParameterCollection.TryValidateParameterOverload(
      parameters,
      parameterArguments,
      out var arguments
    );

    arguments.Last().Should().BeNull();
  }

  [Fact]
  public void TryGetMethodOverloadArguments_ReturnsTrue_WhenParameterCollectionCanBeAsAnOverloadForMethod() {
    var parameters = GetParameterListStringFooIntBar();
  }

  private IEnumerable<ConstructorInfo> GetTestConstructors() {
    return typeof(TestClassWithConstructors).GetConstructors();
  }

  private ParameterInfo[] GetParameterListStringFoo() {
    return GetTestConstructors().First().GetParameters();
  }

  private ParameterInfo[] GetParameterListStringFooIntBar() {
    return GetTestConstructors().Skip(1).First().GetParameters();
  }

  private ParameterInfo[] GetParameterListStringFooOptionalDecimalFrank() {
    return GetTestConstructors().Skip(2).First().GetParameters();
  }

  private ParameterInfo[] GetParameterListDecimalBaz() {
    return GetTestConstructors().Last().GetParameters();
  }
}