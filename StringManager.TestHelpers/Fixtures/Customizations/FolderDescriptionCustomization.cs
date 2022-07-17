using AutoFixture;
using StringManager.Domain.Objects.Value;

namespace StringManager.TestHelpers.Fixtures.Customizations;

public class FolderDescriptionCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => FolderDescription.Create(fixture.Create<string>()).Value);
    }
}