using StringManager.Application.Services.Infrastructure;

namespace StringManager.Infrastructure.Services;

public class ProblemDetailTextService : IProblemDetailTextService
{
    public string? GetText(string key) => ProblemDetailTexts.ResourceManager.GetString(key);
}