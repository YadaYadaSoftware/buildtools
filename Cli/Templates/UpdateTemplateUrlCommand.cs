using CommandLine;
using YadaYada.BuildTools.Templates;

namespace YadaYada.BuildTools.Cli.Templates;

[Verb("update-template-url", HelpText = "Updates a TemplateUrl property in a template")]
public class UpdateTemplateUrlCommand : CommandBase
{
    [Option('t',"template", Required = true)]
    public FileInfo Template { get; set; }

    [Option('r', "resource", Required = true)]
    public string Resource { get; set; }

    [Option('u', "template-url", Required = true)]
    public Uri TemplateUrl { get; set; }
    public override Task ApplyAsync()
    {
        Console.WriteLine($"{nameof(ApplyAsync)}:{nameof(Template)}={Template}, {nameof(Resource)}={Resource}, {nameof(TemplateUrl)}={TemplateUrl}");
        return TemplateUpdater.UpdatePropertyValue(this.Template, this.Resource, "TemplateURL", this.TemplateUrl.ToString());
    }
}