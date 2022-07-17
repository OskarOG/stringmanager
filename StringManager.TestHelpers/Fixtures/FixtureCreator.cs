using AutoFixture;
using StringManager.TestHelpers.Fixtures.Customizations;

namespace StringManager.TestHelpers.Fixtures;

public static class FixtureCreator
{
    public static IFixture CreateInMemoryDbFixture() =>
        new Fixture()
            .Customize(new DomainWithInMemoryDbCustomization());
}