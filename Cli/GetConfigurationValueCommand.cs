using CommandLine;
using Microsoft.Extensions.Configuration;
using YadaYada.BuildTools.Cli;

[Verb("get-config-value")]
public class GetConfigurationValueCommand : CommandBase
{
    [Option('f',"settings-file", Required = true, HelpText = "Path to appsettings.json")]
    public FileInfo SettingsFile { get; set; } = null!;

    [Option('b', "branch", Required = true, HelpText = "Branch name")]
    public string? Branch { get; set; } = null!;

    [Option('s', "setting-name", Required = true, HelpText = "Setting name")]
    public string SettingName { get; set; } = null!;



    public override async Task ApplyAsync()
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile(this.SettingsFile.FullName, false);
        if (this.Branch != null)
        {
            var branchFile = new FileInfo(this.SettingsFile.FullName.Replace(".json", this.Branch + ".json"));
            Console.WriteLine(branchFile);
            if (branchFile.Exists)
            {
                Console.WriteLine("Adding branch file");
                configurationBuilder.AddJsonFile(branchFile.FullName, false);
            }

        }

        var configurationRoot = configurationBuilder.Build();
        string setting = configurationRoot[this.SettingName];
        Console.Write(setting);
    }
}