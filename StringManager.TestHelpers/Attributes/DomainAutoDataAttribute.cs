using AutoFixture;
using AutoFixture.Xunit2;
using StringManager.TestHelpers.Fixtures;

namespace StringManager.TestHelpers.Attributes;

public class DomainAutoDataAttribute : AutoDataAttribute
{
    public DomainAutoDataAttribute()
        : base(() => new Fixture()
            .Customize(new DomainCustomizations()))
    {
    }
}