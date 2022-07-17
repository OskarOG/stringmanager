namespace StringManager.TestHelpers.Exceptions;

public class InvalidTestSetupException : Exception
{
    public InvalidTestSetupException(string message)
        : base(message)
    {
    }
}