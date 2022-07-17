using AutoFixture;
using AutoFixture.Xunit2;
using StringManager.TestHelpers.Fixtures.Customizations;

namespace StringManager.TestHelpers.Fixtures;

public class AutoDataWithInMemoryDbAttribute : AutoDataAttribute
{
    public AutoDataWithInMemoryDbAttribute()
        : base(() => new Fixture()
            .Customize(new DomainWithInMemoryDbCustomization()))
    {
    }
}