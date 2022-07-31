using System.Reflection;
using AutoFixture;
using AutoFixture.Xunit2;
using StringManager.TestHelpers.Fixtures.Customizations;

namespace StringManager.TestHelpers.Fixtures;

public class PasswordStringAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter) =>
        new PasswordStringCustomization();
}