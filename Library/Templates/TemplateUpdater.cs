using System.Text.Json.Nodes;

namespace YadaYada.BuildTools.Templates;

public static class TemplateUpdater
{
    // ReSharper disable once UnusedMember.Global
    public static async Task UpdateParameterValue(FileInfo templateFile, string logicalId, string newValue)
    {
        if (templateFile == null) throw new ArgumentNullException(nameof(templateFile));
        if (logicalId == null) throw new ArgumentNullException(nameof(logicalId));
        if (newValue == null) throw new ArgumentNullException(nameof(newValue));
        var templateNode = await GetTemplateNode(templateFile);
        var parametersNode = templateNode["Parameters"];
        ArgumentNullException.ThrowIfNull(parametersNode,nameof(parametersNode));

        var parameterToUpdate = parametersNode[logicalId];
        ArgumentNullException.ThrowIfNull(parameterToUpdate,nameof(parameterToUpdate));

        UpdateNode(parameterToUpdate, "Default", newValue);

        await File.WriteAllTextAsync(templateFile.FullName, templateNode.ToJsonString());


    }
    public static async Task UpdatePropertyValue(FileInfo templateFile, string logicalId, string path, string newValue)
    {
        if (templateFile == null) throw new ArgumentNullException(nameof(templateFile));
        if (logicalId == null) throw new ArgumentNullException(nameof(logicalId));
        if (path == null) throw new ArgumentNullException(nameof(path));
        if (newValue == null) throw new ArgumentNullException(nameof(newValue));
        if (templateFile == null) throw new ArgumentNullException(nameof(templateFile));
        if (logicalId == null) throw new ArgumentNullException(nameof(logicalId));
        if (path == null) throw new ArgumentNullException(nameof(path));
        if (newValue == null) throw new ArgumentNullException(nameof(newValue));
        if (string.IsNullOrEmpty(logicalId)) throw new ArgumentException("Value cannot be null or empty.", nameof(logicalId));
        if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

        var templateNode = await GetTemplateNode(templateFile);

        var resources = templateNode["Resources"];
        ArgumentNullException.ThrowIfNull(resources, nameof(resources));

        JsonNode? resource = default ;//= resources[logicalId];

        foreach (var (key, value) in resources.AsObject())
        {
            if (string.Equals(key, logicalId, StringComparison.CurrentCulture))
            {
                resource = value;
                break;
            }
        }

        ArgumentNullException.ThrowIfNull(resource, nameof(resource));

        var properties = resource["Properties"];
        ArgumentNullException.ThrowIfNull(properties, nameof(properties));

        var effectedProperty = properties[path];
        ArgumentNullException.ThrowIfNull(effectedProperty, nameof(effectedProperty));

        UpdateNode(properties, path, newValue);

        await File.WriteAllTextAsync(templateFile.FullName, templateNode.ToJsonString());
    }

    private static void UpdateNode(JsonNode parentNode, string path, string newValue)
    {
        Console.WriteLine("UpdateNode: {0}, {1}, {2}", parentNode.GetPath(), path, newValue);
        var propertiesObject = parentNode.AsObject();
        propertiesObject.Remove(path);
        propertiesObject.Add(path, newValue);
    }

    private static async Task<JsonNode> GetTemplateNode(FileInfo templateFile)
    {
        if (!templateFile.Exists) throw new FileNotFoundException(templateFile.FullName);

        var json = await File.ReadAllTextAsync(templateFile.FullName);

        var templateNode = JsonNode.Parse(json);
        ArgumentNullException.ThrowIfNull(templateNode, nameof(templateNode));
        return templateNode;
    }
}