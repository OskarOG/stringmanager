using StringManager.Application.Services.Infrastructure;
using StringManager.Domain.Messages;
using StringManager.Domain.Objects.Infrastructure;

namespace StringManager.Application.Services.Domain;

public class ProblemDetailFactory : IProblemDetailFactory
{
    private const string GeneralDetailKey = "detail";
    private const string GeneralTitleKey = "title";

    private readonly IProblemDetailTextService _problemDetailTextService;

    public ProblemDetailFactory(IProblemDetailTextService problemDetailTextService)
    {
        _problemDetailTextService = problemDetailTextService;
    }

    public ProblemDetail CreateProblemDetail(ProblemType problemType) =>
        new(
            Enum.GetName(problemType)
            ?? throw new NullReferenceException($"Unable to find a string name for the problem detail parameter"),
            _problemDetailTextService.GetText($"{GeneralTitleKey}-{Enum.GetName(problemType)}")
            ?? throw new NullReferenceException(
                $"Unable to find a title for problem type: {Enum.GetName(problemType)}"),
            _problemDetailTextService.GetText($"{GeneralDetailKey}-{Enum.GetName(problemType)}")
            ?? throw new NullReferenceException(
                $"Unable to find a detail for problem type: {Enum.GetName(problemType)}"));
}