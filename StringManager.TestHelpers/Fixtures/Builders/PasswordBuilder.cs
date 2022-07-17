using AutoFixture.Kernel;
using StringManager.Domain.Objects.Value;

namespace StringManager.TestHelpers.Fixtures.Builders;

public class PasswordBuilder : ISpecimenBuilder
{
    private readonly Random _rand;

    public PasswordBuilder()
    {
        _rand = new Random();
    }
    
    public object Create(object request, ISpecimenContext context)
    {
        if (request is SeededRequest {Request: Type type} && type == typeof(Password))
        {
            return Password.NewPassword(NewValidPasswordString()).Value;
        }

        return new NoSpecimen();
    }

    private string NewValidPasswordString()
    {
        var chars = new List<char>();
        chars.AddRange(GenerateStringFromCharRange('a', 'z'));
        chars.AddRange(GenerateStringFromCharRange('A', 'Z'));
        chars.AddRange(GenerateStringFromCharRange('0', '9'));
        
        return new string(chars.OrderBy(a => Guid.NewGuid()).ToArray());
    }

    private IEnumerable<char> GenerateStringFromCharRange(int low, int high)
    {
        var stringLength = _rand.Next(4, 10);
        for (var i = 0; i < stringLength; i++)
        {
            yield return (char)_rand.Next(low, high);
        }
    }
}