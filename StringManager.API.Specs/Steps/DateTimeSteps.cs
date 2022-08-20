using StringManager.API.Specs.Drivers;

namespace StringManager.API.Specs.Steps;

[Binding]
public class DateTimeSteps
{
    private readonly IDateTimeDriver _dateTimeDriver;

    public DateTimeSteps(IDateTimeDriver dateTimeDriver)
    {
        _dateTimeDriver = dateTimeDriver;
    }
    
    [Given(@"that the current date and time is ""(.*)""")]
    public void GivenThatTheCurrentDateAndTimeIs(string dateTimeString)
    {
        _dateTimeDriver.SetCurrentTime(dateTimeString);
    }
}