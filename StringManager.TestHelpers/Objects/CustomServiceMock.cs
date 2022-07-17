using Microsoft.Extensions.DependencyInjection;

namespace StringManager.TestHelpers.Objects;

public class CustomServiceMock
{
    public CustomServiceMock(ServiceLifetime lifetime, object serviceMock)
    {
        Lifetime = lifetime;
        ServiceMock = serviceMock;
    }

    public ServiceLifetime Lifetime { get; }

    public object ServiceMock { get; }
}