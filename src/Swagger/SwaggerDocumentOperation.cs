using Newtonsoft.Json;

namespace NSwaggerMerge.Swagger;

/// <summary>
/// Defines the detail of a single API operation on a path.
/// </summary>
public class SwaggerDocumentOperation : SwaggerDocumentBase
{
    /// <summary>
    /// List of tags for API documentation control.
    /// </summary>
    [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
    public List<string> Tags { get; set; } = [];

    /// <summary>
    /// Short summary of what the operation does.
    /// </summary>
    [JsonProperty("summary", NullValueHandling = NullValueHandling.Ignore)]
    public string? Summary { get; set; }

    /// <summary>
    /// Verbose explanation of the operation behavior.
    /// </summary>
    [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
    public string? Description { get; set; }

    /// <summary>
    /// Unique string used to identify the operation.
    /// </summary>
    [JsonProperty("operationId", NullValueHandling = NullValueHandling.Ignore)]
    public string? OperationId { get; set; }

    /// <summary>
    /// List of MIME types the APIs can produce.
    /// </summary>
    [JsonProperty("produces", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Produces { get; set; }

    /// <summary>
    /// List of parameters that are applicable for this operation.
    /// </summary>
    [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
    public List<SwaggerDocumentProperty>? Parameters { get; set; }

    /// <summary>
    /// List of possible responses as they are returned from executing this operation.
    /// </summary>
    [JsonProperty("responses", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, SwaggerDocumentProperty>? Responses { get; set; }

    /// <summary>
    /// Transfer protocol of the API.
    /// </summary>
    [JsonProperty("schemes", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Schemes { get; set; }

    /// <summary>
    /// Value indicating whether this operation is deprecated.
    /// </summary>
    [JsonProperty("deprecated", NullValueHandling = NullValueHandling.Ignore)]
    public bool Deprecated { get; set; }
}
