namespace StringManager.Domain.Messages;

public class ProblemDetail
{
    public ProblemDetail(
        string problemType,
        string title,
        string detail)
    {
        ProblemType = problemType;
        Title = title;
        Detail = detail;
    }

    public string ProblemType { get; }

    public string Title { get; }

    public string Detail { get; }
}