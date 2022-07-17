namespace StringManager.Domain.Objects.Infrastructure;

public class Result<T>
{
    private readonly Error? _error;
    private readonly T? _value;

    protected Result(T value)
    {
        _value = value;
        _error = null;
    }

    public Result(Error error)
    {
        _error = error;
    }

    public Error Error => _error ?? throw new InvalidOperationException("No error is set for this result");

    public bool IsFailure => !IsSuccess;
    
    public bool IsSuccess => _error == null;

    public T Value => _value ?? throw new InvalidOperationException("No value is set for this result");

    public static Result<T> ErrorResult(Error error) => new(error);

    public static Result<T> SuccessResult(T value) => new(value);
}