using System.Text;
using AutoFixture.Kernel;
using StringManager.Domain.Objects.Value;

namespace StringManager.TestHelpers.Fixtures.Builders;

public class EmailBuilder : ISpecimenBuilder
{
    private readonly Tuple<int, int>[] _availableCharSpans =
    {
        new('a', 'z'),
        new('A', 'Z'),
        new('0', '9')
    };

    private readonly Random _rand;

    public EmailBuilder()
    {
        _rand = new Random();
    }
    
    public object Create(object request, ISpecimenContext context)
    {
        if (request is SeededRequest {Request: Type type} && type == typeof(Email))
        {
            return Email.Create(CreateEmailString()).Value;
        }

        return new NoSpecimen();
    }

    private string CreateEmailString()
    {
        var firstPart = GenerateRandomString();
        var splitter = firstPart.Length % 2 == 0 ? "-" : ".";
        var secondPart = GenerateRandomString();
        var domainPart = GenerateRandomString();
        var topLevelDomainPart = GenerateRandomString(2, 4);

        return $"{firstPart}{splitter}{secondPart}@{domainPart}.{topLevelDomainPart}";
    }

    private string GenerateRandomString() => GenerateRandomString(4, 10);

    private string GenerateRandomString(int minLength, int maxLength)
    {
        var builder = new StringBuilder();

        var maxCharSpanIndex = _availableCharSpans.Length - 1;
        var stringLength = _rand.Next(minLength, maxLength);
        for (var i = 0; i < stringLength; i++)
        {
            var (charSpanMin, charSpanMax) = _availableCharSpans[_rand.Next(0, maxCharSpanIndex)];
            builder.Append((char) _rand.Next(charSpanMin, charSpanMax));
        }

        return builder.ToString();
    }
}