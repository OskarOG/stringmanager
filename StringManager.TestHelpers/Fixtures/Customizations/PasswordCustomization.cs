using AutoFixture;
using StringManager.TestHelpers.Fixtures.Builders;

namespace StringManager.TestHelpers.Fixtures.Customizations;

public class PasswordCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Insert(0, new PasswordBuilder());
    }
}