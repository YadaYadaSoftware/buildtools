// See https://aka.ms/new-console-template for more information

using CommandLine;
using YadaYada.BuildTools.Cli;
using YadaYada.BuildTools.Cli.Templates;

Console.WriteLine(string.Join(',', args));

ParserResult<object> parserResult = await (await (await Parser.Default.ParseArguments<UpdateProjectReferencesCommand, UpdateTemplateUrlCommand>(args)
        .WithParsedAsync<UpdateProjectReferencesCommand>(async o =>
        {
        }))
    .WithParsedAsync<UpdateTemplateUrlCommand>(async o =>
    {
        try
        {
            await o.ApplyAsync();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    })).WithNotParsedAsync(errors => throw new NotSupportedException());

Environment.Exit(parserResult.Errors.Count());