using System.Reflection;
using AutoFixture;
using AutoFixture.Xunit2;
using StringManager.TestHelpers.Fixtures.Customizations;

namespace StringManager.TestHelpers.Fixtures;

public class LimitRecursionAttribute : CustomizeAttribute
{
    private readonly int _maxDepth;
    
    public LimitRecursionAttribute(int depth = 1)
    {
        _maxDepth = depth;
    }
    
    public override ICustomization GetCustomization(ParameterInfo parameter)
    {
        return new LimitRecursionCustomization(_maxDepth);
    }
}