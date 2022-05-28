using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace StringManager.TestHelpers.Fixtures.Customizations;

public class DomainCustomizations : CompositeCustomization
{
    public DomainCustomizations()
        : base(
            new AutoNSubstituteCustomization())
    {
    }
}