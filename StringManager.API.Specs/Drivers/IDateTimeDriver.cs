namespace StringManager.API.Specs.Drivers;

public interface IDateTimeDriver
{
    DateTime CurrentTime { get; }
    
    void SetCurrentTime(string dateTimeString);
}