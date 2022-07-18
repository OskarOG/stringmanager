using StringManager.Domain.Messages;
using StringManager.Domain.Objects.Infrastructure;

namespace StringManager.Application.Services.Domain;

public interface IProblemDetailFactory
{
    ProblemDetail CreateProblemDetail(ProblemType problemType);
}