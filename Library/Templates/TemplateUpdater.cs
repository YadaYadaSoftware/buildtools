using System.Text.Json.Nodes;

namespace YadaYada.BuildTools.Templates;

public static class TemplateUpdater
{
    public static async Task UpdateTemplateAsync(FileInfo templateFile, string logicalId, string propertyPath, string newValue)
    {
        if (templateFile == null) throw new ArgumentNullException(nameof(templateFile));
        if (logicalId == null) throw new ArgumentNullException(nameof(logicalId));
        if (propertyPath == null) throw new ArgumentNullException(nameof(propertyPath));
        if (newValue == null) throw new ArgumentNullException(nameof(newValue));
        if (templateFile == null) throw new ArgumentNullException(nameof(templateFile));
        if (logicalId == null) throw new ArgumentNullException(nameof(logicalId));
        if (propertyPath == null) throw new ArgumentNullException(nameof(propertyPath));
        if (newValue == null) throw new ArgumentNullException(nameof(newValue));
        if (string.IsNullOrEmpty(logicalId)) throw new ArgumentException("Value cannot be null or empty.", nameof(logicalId));
        if (string.IsNullOrEmpty(propertyPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyPath));

        if (!templateFile.Exists) throw new FileNotFoundException(templateFile.FullName);

        var json = await File.ReadAllTextAsync(templateFile.FullName);

        var templateNode = JsonNode.Parse(json);
        ArgumentNullException.ThrowIfNull(templateNode, nameof(templateNode));

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

        var effectedProperty = properties[propertyPath];
        ArgumentNullException.ThrowIfNull(effectedProperty, nameof(effectedProperty));

        var propertiesObject = properties.AsObject();
        propertiesObject.Remove(propertyPath);
        propertiesObject.Add(propertyPath,newValue);

        await File.WriteAllTextAsync(templateFile.FullName, templateNode.ToJsonString());
    }
}