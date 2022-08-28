// See https://aka.ms/new-console-template for more information

using Amazon.CloudFormation;
using CommandLine;
using YadaYada.BuildTools.Cli;



await Parser.Default.ParseArguments<GetConfigurationValueCommand>(args)
    .WithParsedAsync<GetConfigurationValueCommand>(async o =>
{
    await o.ApplyAsync();
});

