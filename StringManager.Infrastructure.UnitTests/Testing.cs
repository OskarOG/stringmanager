using System;
using System.Collections.Generic;
using System.Linq;
using StringManager.Domain.Objects.Value;
using Xunit;

namespace StringManager.Infrastructure.UnitTests;

public class Testing
{
    enum TeEnum
    {
        Se = 1,
        Se2 = 2,
        Se3 = 3,
        Se4 = 4
    }
    
    [Fact]
    public static void TestingTest()
    {
        var list = new List<TeEnum>()
        {
            TeEnum.Se,
            TeEnum.Se3,
            TeEnum.Se4
        };

        var result = string.Join(";", list);

        var splittedStringlist = result.Split(";");
        var resultlist = splittedStringlist.Select(Enum.Parse<TeEnum>).ToList();
    }
}