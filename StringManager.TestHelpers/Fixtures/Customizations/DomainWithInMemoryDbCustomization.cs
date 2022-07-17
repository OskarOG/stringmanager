using AutoFixture;
using EntityFrameworkCore.AutoFixture.InMemory;

namespace StringManager.TestHelpers.Fixtures.Customizations;

public class DomainWithInMemoryDbCustomization : CompositeCustomization
{
    public DomainWithInMemoryDbCustomization()
        : base(
            new DomainCustomizations(),
            new InMemoryContextCustomization
            {
                AutoCreateDatabase = true,
                OmitDbSets = true
            })
    {
    }
}