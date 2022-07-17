using System.Net;
using StringManager.Application.Services.Infrastructure;
using StringManager.Domain.Messages;
using StringManager.Domain.Objects.Infrastructure;

namespace StringManager.Infrastructure.Services;

// TODO: Benchmark if there's a noticeable difference in saving problem detail texts and other internal application texts in a specific table or if they can be handled as strings in a folder?
public class ProblemDetailFactory : IProblemDetailFactory
{
    private const string GeneralDetailKey = "detail";
    private const string GeneralTitleKey = "title";

    public ProblemDetail CreateProblemDetail(ProblemType problemType) =>
        CreateProblemDetail(problemType, null);

    public ProblemDetail CreateProblemDetail(ProblemType problemType, HttpStatusCode? statusCode)
    {
        return new(
            Enum.GetName(problemType)
                ?? throw new NullReferenceException($"Unable to find a string name for the problem detail parameter"),
            ProblemDetailTexts.ResourceManager.GetString($"{GeneralTitleKey}{Enum.GetName(problemType)}")
                ?? throw new NullReferenceException($"Unable to find a title for problem type: {Enum.GetName(problemType)}"),
            ProblemDetailTexts.ResourceManager.GetString($"{GeneralDetailKey}{Enum.GetName(problemType)}")
                ?? throw new NullReferenceException($"Unable to find a detail for problem type: {Enum.GetName(problemType)}"),
            statusCode != null ? (int) statusCode : null);
    }
}