// See https://aka.ms/new-console-template for more information

using Amazon.CloudFormation;
using CommandLine;
using Microsoft.Extensions.Configuration;
using YadaYada.BuildTools.Cli;



await Parser.Default.ParseArguments<GetConfigurationValueCommand>(args)
    .WithParsedAsync<GetConfigurationValueCommand>(async o =>
{
    await o.ApplyAsync();
});

public abstract class CommandBase
{
    public abstract Task ApplyAsync();
}

[Verb("get-config-value")]
public class GetConfigurationValueCommand : CommandBase
{
    [Option('f',"settings-file", Required = true, HelpText = "Path to appsettings.json")]
    public FileInfo SettingsFile { get; set; } = null!;

    [Option('b', "branch", Required = true, HelpText = "Branch name")]
    public string Branch { get; set; } = null!;

    [Option('s', "setting-name", Required = true, HelpText = "Setting name")]
    public string SettingName { get; set; } = null!;



    public override async Task ApplyAsync()
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile(this.SettingsFile.FullName, true);
        var branchFile = new FileInfo(this.SettingsFile.FullName.Replace(".json", this.Branch + ".json"));
        if (branchFile.Exists) configurationBuilder.AddJsonFile(branchFile.FullName, false);

        var configurationRoot = configurationBuilder.Build();
        string setting = configurationRoot[this.SettingName];
        Console.Write(setting);
    }
}