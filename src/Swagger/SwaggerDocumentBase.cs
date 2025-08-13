using MADE.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace NSwaggerMerge.Swagger;

public class SwaggerDocumentBase
{
    /// <summary>
    /// Properties that are not covered by the defined Swagger properties.
    /// </summary>
    [JsonIgnore]
    public Dictionary<string, SwaggerDocumentProperty>? AdditionalProperties { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JToken>? JTokenProperties { get; set; }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
        var objectTokens = JTokenProperties?.Where(x => x.Value is JObject).ToList();

        if (objectTokens == null || objectTokens.Count == 0)
            return;

        AdditionalProperties = objectTokens.ToDictionary(
            x => x.Key,
            x => Helper.ToSwaggerDocumentProperty(x.Value as JObject));

        JTokenProperties.RemoveRange(objectTokens);
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext context)
    {
        var additionalProperties = AdditionalProperties?.ToDictionary(
            x => x.Key,
            x => JToken.FromObject(x.Value));

        if (additionalProperties == null)
            return;

        JTokenProperties.AddRange(additionalProperties);
    }
}
