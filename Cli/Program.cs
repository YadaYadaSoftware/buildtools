// See https://aka.ms/new-console-template for more information

using CommandLine;
using YadaYada.BuildTools.Cli;

Console.WriteLine("Hello, World!");

ParserResult<object> parserResult = await (await (await Parser.Default.ParseArguments<UpdateProjectReferencesCommand, UpdateTemplateUrlCommand>(args)
        .WithParsedAsync<UpdateProjectReferencesCommand>(async o =>
        {
        }))
    .WithParsedAsync<UpdateTemplateUrlCommand>(async o =>
    {
        await o.ApplyAsync();
    })).WithNotParsedAsync(errors => throw new NotSupportedException());

Environment.Exit(parserResult.Errors.Count());