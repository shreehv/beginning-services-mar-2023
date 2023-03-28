
using OnCallDeveloperApi;
using Alba;
using OnCallDeveloperApi.Models;

namespace OnCallDeveloperApi.AcceptanceTests;

public class GettingOnCallDeveloper
{
    [Fact]
    public async Task CanGetOnCallDeveloperDuringWeekdays()
    {
        // we need the web server up and running - we are doing "in memory" here,
        // it doesn't really start a publicly exposed http server.
        await using var host = await AlbaHost.For<Program>();

        var response = await host.Scenario(api =>
        {
            api.Get.Url("/oncalldeveloper");
            api.StatusCodeShouldBeOk();
        });

        var expected = new OnCallDeveloperModel
        {
            Name = "Bob Smith",
            Phone = "888-8888",
            Email = "bob@company.com"
        };

        var actualResponse = response.ReadAsJson<OnCallDeveloperModel>();

        Assert.Equal(expected, actualResponse);
    }
    [Fact]
    public async Task CanGetOnCallDeveloperDuringWeekEnds()
    {
        // we need the web server up and running - we are doing "in memory" here,
        // it doesn't really start a publicly exposed http server.
        await using var host = await AlbaHost.For<Program>();

        var response = await host.Scenario(api =>
        {
            api.Get.Url("/oncalldeveloper");
            api.StatusCodeShouldBeOk();
        });

        var expected = new OnCallDeveloperModel
        {
            Name = "House of Outsourced Support, Inc.",
            Phone = "800 111-1111",
            Email = "support@house-of-outsourced-support.com"
        };

        var actualResponse = response.ReadAsJson<OnCallDeveloperModel>();

        Assert.Equal(expected, actualResponse);
    }
}
