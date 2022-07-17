using AutoFixture;
using StringManager.TestHelpers.Fixtures.Builders;

namespace StringManager.TestHelpers.Fixtures.Customizations;

public class EmailCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new EmailBuilder());
    }
}