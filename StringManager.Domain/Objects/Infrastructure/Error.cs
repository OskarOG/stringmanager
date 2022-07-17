namespace StringManager.Domain.Objects.Infrastructure;

public class Error
{
    private readonly Exception? _exception;

    public Error(ProblemType problemType)
    {
        ProblemType = problemType;
    }
    
    public Error(ProblemType problemType, Exception? exception)
    {
        ProblemType = problemType;
        _exception = exception;
    }

    public bool IsException => _exception != null;

    public ProblemType ProblemType { get; }

    public Exception Exception => _exception
        ?? throw new InvalidOperationException("No exception is set for this error");
}