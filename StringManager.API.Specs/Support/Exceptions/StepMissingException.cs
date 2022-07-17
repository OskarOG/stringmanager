namespace StringManager.API.Specs.Support.Exceptions;

public class StepMissingException : Exception
{
    public StepMissingException(string message)
        : base(message)
    {
    }
}