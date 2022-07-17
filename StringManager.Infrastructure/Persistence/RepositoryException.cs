namespace StringManager.Infrastructure.Persistence;

public class RepositoryException : Exception
{
    public RepositoryException(string message)
        : base(message)
    {
    }
}