using AutoFixture;

namespace StringManager.TestHelpers.Fixtures.Customizations;

public class LimitRecursionCustomization : ICustomization
{
    private readonly int _maxDepth;
    
    public LimitRecursionCustomization(int depth = 1)
    {
        _maxDepth = depth;
    }
    
    public void Customize(IFixture fixture)
    {
        fixture
            .Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        
        fixture.Behaviors.Add(new NullRecursionBehavior(_maxDepth));
    }
}