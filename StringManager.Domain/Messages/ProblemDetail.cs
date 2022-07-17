namespace StringManager.Domain.Messages;

public class ProblemDetail
{
    public ProblemDetail(
        string problemType,
        string title,
        string detail,
        int? statusCode)
    {
        ProblemType = problemType;
        Title = title;
        Detail = detail;
        StatusCode = statusCode;
    }

    public string ProblemType { get; }

    public string Title { get; }

    public string Detail { get; }

    public int? StatusCode { get; }
}