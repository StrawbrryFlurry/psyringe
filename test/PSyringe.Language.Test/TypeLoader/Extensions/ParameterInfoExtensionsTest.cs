using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using PSyringe.Language.TypeLoader.Extensions;
using PSyringe.Language.TypeLoader.Parameters;
using Xunit;

namespace PSyringe.Language.Test.TypeLoader.Extensions;

public class ParameterInfoExtensionsTest {
  [Fact]
  public void IsEquivalentTo_ReturnsTrue_WhenNamedParameterMatchesTheParameterInfo() {
    var parameters = GetFooParameters();
    var testParameter = new NamedParameter {
      Name = "foo",
      Type = typeof(string)
    };

    var fooParameter = parameters.First();
    fooParameter.IsEquivalentTo(testParameter).Should().BeTrue();
  }

  [Fact]
  public void IsEquivalentTo_ReturnsTrue_WhenNamedParameterWithBadNameCasingMatchesTheParameterInfo() {
    var parameters = GetFooParameters();
    var testParameter = new NamedParameter {
      Name = "FoO",
      Type = typeof(string)
    };

    var fooParameter = parameters.First();
    fooParameter.IsEquivalentTo(testParameter).Should().BeTrue();
  }

  [Fact]
  public void IsEquivalentTo_ReturnsTrue_WhenPositionalParameterMatchesTheParameterInfo() {
    var parameters = GetFooParameters();
    var testParameter = new PositionalParameter {
      Position = 0,
      Type = typeof(string)
    };

    var fooParameter = parameters.First();
    fooParameter.IsEquivalentTo(testParameter).Should().BeTrue();
  }

  [Fact]
  public void IsEquivalentTo_ReturnsFalse_WhenPositionalParameterDoesNotMatchTheParameterInfo() {
    var parameters = GetFooParameters();
    var testParameter = new PositionalParameter {
      Position = 2,
      Type = typeof(string)
    };

    var fooParameter = parameters.First();
    fooParameter.IsEquivalentTo(testParameter).Should().BeFalse();
  }

  [Fact]
  public void IsEquivalentTo_ReturnsFalse_WhenNamedParameterDoesNotMatchTheParameterInfo() {
    var parameters = GetFooParameters();
    var testParameter = new NamedParameter {
      Name = "bar",
      Type = typeof(string)
    };

    var fooParameter = parameters.First();
    fooParameter.IsEquivalentTo(testParameter).Should().BeFalse();
  }

  private IEnumerable<ConstructorInfo> GetTestConstructors() {
    return typeof(TestClassWithConstructors).GetConstructors();
  }

  private ParameterInfo[] GetFooParameters() {
    return GetTestConstructors().First().GetParameters();
  }
}