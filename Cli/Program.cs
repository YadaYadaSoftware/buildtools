// See https://aka.ms/new-console-template for more information

using Amazon.CloudFormation;
using CommandLine;
using YadaYada.BuildTools.Cli;


Parser.Default.ParseArguments<UpdateProjectReferencesCommand>(args)
    .WithParsed<UpdateProjectReferencesCommand>(o =>
    {
    });

await (await Parser.Default.ParseArguments<UpdateProjectReferencesCommand, GetConfigurationValueCommand>(args)
        .WithParsedAsync<UpdateProjectReferencesCommand>(async o =>
        {
        })
    ).WithParsedAsync<GetConfigurationValueCommand>(async o =>
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
    public override async Task ApplyAsync()
    {
        Console.WriteLine("So you want to get a configuration value");
    }
}