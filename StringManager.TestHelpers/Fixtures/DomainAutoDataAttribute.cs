using AutoFixture;
using AutoFixture.Xunit2;
using StringManager.TestHelpers.Fixtures.Customizations;

namespace StringManager.TestHelpers.Fixtures;

public class DomainAutoDataAttribute : AutoDataAttribute
{
    public DomainAutoDataAttribute()
        : base(() => new Fixture()
            .Customize(new DomainCustomizations()))
    {
    }
}