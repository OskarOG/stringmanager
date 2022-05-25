using AutoFixture;
using AutoFixture.Xunit2;
using EntityFrameworkCore.AutoFixture.InMemory;
using StringManager.TestHelpers.Fixtures;

namespace StringManager.TestHelpers.Attributes;

public class AutoDataWithInMemoryDbAttribute : AutoDataAttribute
{
    public AutoDataWithInMemoryDbAttribute()
        : base(() => new Fixture()
            .Customize(new DomainCustomizations())
            .Customize(new InMemoryContextCustomization
            {
                AutoCreateDatabase = true,
                OmitDbSets = true
            }))
    {
    }
}