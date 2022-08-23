namespace StringManager.Application.Services.Infrastructure;

public class DateTimeService : IDateTimeService
{
    public DateTime GetUniversalTime() => DateTime.UtcNow;
}