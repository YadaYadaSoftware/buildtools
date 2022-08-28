using FluentAssertions;
using Xunit;

namespace Cli.Test;

public class GetConfigurationValueCommandTest
{
    [Fact]
    public async void Test()
    {
        var target = new GetConfigurationValueCommand
        {
            SettingName = "AddFtps"
        };
        var targetSettingsFile = new FileInfo("appsettings.json");
        targetSettingsFile.Exists.Should().BeTrue();
        target.SettingsFile = targetSettingsFile;

        await target.ApplyAsync();
    }
}