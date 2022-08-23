using NSubstitute;
using StringManager.API.Specs.Support.Exceptions;
using StringManager.Application.Services.Infrastructure;

namespace StringManager.API.Specs.Drivers;

public class DateTimeDriver : IDateTimeDriver
{
    private readonly IDateTimeService _dateTimeService;
    
    private DateTime? _currentTime;

    public DateTimeDriver(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }
    
    public DateTime CurrentTime => _currentTime
                                   ?? throw new StepMissingException("A step is missing to set the current time.");

    public void SetCurrentTime(string dateTimeString)
    {
        _currentTime = DateTime.Parse(dateTimeString);
        _dateTimeService.GetUniversalTime().Returns(DateTime.SpecifyKind(CurrentTime, DateTimeKind.Utc));
    }
}