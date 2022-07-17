using System.Net;
using StringManager.Domain.Messages;
using StringManager.Domain.Objects.Infrastructure;

namespace StringManager.Application.Services.Infrastructure;

public interface IProblemDetailFactory
{
    ProblemDetail CreateProblemDetail(ProblemType problemType);

    ProblemDetail CreateProblemDetail(ProblemType problemType, HttpStatusCode? statusCode);
}