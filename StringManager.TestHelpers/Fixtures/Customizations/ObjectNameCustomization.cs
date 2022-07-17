using AutoFixture;
using StringManager.Domain.Objects.Value;

namespace StringManager.TestHelpers.Fixtures.Customizations;

public class ObjectNameCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => ObjectName.Create(fixture.Create<string>()).Value);
    }
}