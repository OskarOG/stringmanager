using AutoFixture;
using BoDi;
using EntityFrameworkCore.AutoFixture.InMemory;
using StringManager.API.Specs.Drivers;
using StringManager.Infrastructure.Persistence;
using StringManager.TestHelpers.Fixtures.Customizations;

namespace StringManager.API.Specs.Hooks;

[Binding]
public class Hooks
{
    [BeforeScenario(Order = 1)]
    public static void RegisterDependencies(IObjectContainer objectContainer)
    {
        var fixture = CreateFixture();
        
        objectContainer.RegisterInstanceAs(fixture);
        objectContainer.RegisterInstanceAs(fixture.Create<StringManagerDbContext>());

        objectContainer.RegisterTypeAs<DatabaseDriver, IDatabaseDriver>();
        objectContainer.RegisterTypeAs<HttpClientDriver, IHttpClientDriver>();
        objectContainer.RegisterTypeAs<UserDriver, IUserDriver>();
    }
    
    private static IFixture CreateFixture() => 
        new Fixture()
            .Customize(new DomainCustomizations())
            .Customize(new InMemoryContextCustomization
            {
                AutoCreateDatabase = true,
                OmitDbSets = true
            });
}
