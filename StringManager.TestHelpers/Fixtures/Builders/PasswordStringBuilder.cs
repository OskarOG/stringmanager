using System.Reflection;
using AutoFixture.Kernel;

namespace StringManager.TestHelpers.Fixtures.Builders;

public class PasswordStringBuilder : ISpecimenBuilder
{
    private readonly Random _rand;

    public PasswordStringBuilder()
    {
        _rand = new Random();
    }
    
    public object Create(object request, ISpecimenContext context)
    {
        if (request is ParameterInfo info
            && info.ParameterType == typeof(string) 
            && (info.Name?.Contains("password", StringComparison.InvariantCultureIgnoreCase) ?? false))
        {
            return NewValidPasswordString();
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