using Amazon.CloudFormation;
using CommandLine;

[Verb("update-template-url", HelpText = "Updates a TemplateUrl property in a template")]
public class UpdateTemplateUrlCommand : CommandBase
{
    [Option('t',"template")]
    public FileInfo Template { get; set; }

    [Option('r', "resource")]
    public string Resource { get; set; }

    [Option('u', "template-url")]
    public Uri TemplateUrl { get; set; }
    public override Task ApplyAsync()
    {
        Console.WriteLine($"{nameof(ApplyAsync)}:{nameof(Template)}={Template}, {nameof(Resource)}={Resource}, {nameof(TemplateUrl)}={TemplateUrl}");
        TemplateUpdater.UpdateTemplateAsync(this.Template, this.Resource, "TemplateURL", this.TemplateUrl.ToString());
        return Task.CompletedTask;
    }
}