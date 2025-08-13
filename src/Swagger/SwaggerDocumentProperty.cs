using Newtonsoft.Json;

namespace NSwaggerMerge.Swagger;

/// <summary>
/// Defines the detail of a generic Swagger document property.
/// </summary>
public class SwaggerDocumentProperty : SwaggerDocumentBase
{
    /// <summary>
    /// Reference string to a definition.
    /// </summary>
    [JsonProperty("$ref", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }

    /// <summary>
    /// Definition of the parameter structure.
    /// </summary>
    [JsonProperty("schema", NullValueHandling = NullValueHandling.Ignore)]
    public SwaggerDocumentProperty? Schema { get; set; }

    /// <summary>
    /// Type of items in the array if the type is <b>array</b>.
    /// </summary>
    [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
    public SwaggerDocumentProperty? Items { get; set; }

    /// <summary>
    /// Properties of an item.
    /// </summary>
    [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, SwaggerDocumentProperty>? Properties { get; set; }
}
