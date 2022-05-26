using AutoFixture;

namespace StringManager.TestHelpers.Fixtures.Customizations;

public class OmitOnRecursionCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior(3));
    }
}