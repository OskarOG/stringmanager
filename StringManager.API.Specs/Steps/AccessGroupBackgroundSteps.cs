using StringManager.API.Specs.Drivers;
using StringManager.API.Specs.Drivers.RowObjects;
using TechTalk.SpecFlow.Assist;

namespace StringManager.API.Specs.Steps;

[Binding]
public class AccessGroupBackgroundSteps
{
    private readonly DatabaseDriver _databaseDriver;

    public AccessGroupBackgroundSteps(DatabaseDriver databaseDriver)
    {
        _databaseDriver = databaseDriver;
    }
    
    [Given(@"that the following access groups exists")]
    public async Task GivenThatTheFollowingAccessGroupsExists(Table table)
    {
        await _databaseDriver.AddExistingAccessGroupsAsync(
            table.CreateSet<AccessGroupRow>());
    }
}