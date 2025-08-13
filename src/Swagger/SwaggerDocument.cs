using Newtonsoft.Json;

namespace NSwaggerMerge.Swagger;

/// <summary>
/// Defines the details of a Swagger document.
/// </summary>
public class SwaggerDocument : SwaggerDocumentBase
{
    /// <summary>
    /// OpenApi specification version.
    /// </summary>
    [JsonProperty("openapi", NullValueHandling = NullValueHandling.Ignore)]
    public string OpenApiVersion { get; set; } = "3.0.1";

    /// <summary>
    /// Metadata about the API.
    /// </summary>
    [JsonProperty("info", NullValueHandling = NullValueHandling.Ignore)]
    public SwaggerDocumentInfo Info { get; set; } = new();

    /// <summary>
    /// Host (name or IP) serving the API.
    /// </summary>
    [JsonProperty("host", NullValueHandling = NullValueHandling.Ignore)]
    public string? Host { get; set; }

    /// <summary>
    /// Base path, relative to the host, on which the API is served.
    /// </summary>
    [JsonProperty("basePath", NullValueHandling = NullValueHandling.Ignore)]
    public string? BasePath { get; set; }

    /// <summary>
    /// Transfer protocol of the API.
    /// </summary>
    [JsonProperty("schemas", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Schemes { get; set; }

    [JsonProperty("components", NullValueHandling = NullValueHandling.Ignore)]
    public SwaggerDocumentComponents? Components { get; set; } = new();

    /// <summary>
    /// MIME types the APIs can produce.
    /// </summary>
    [JsonProperty("produces", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Produces { get; set; }

    /// <summary>
    /// Available paths and operations for the API.
    /// </summary>
    [JsonProperty("paths", NullValueHandling = NullValueHandling.Ignore)]
    public SwaggerDocumentPaths? Paths { get; set; } = [];

    /// <summary>
    /// Parameters to be reused across operations.
    /// </summary>
    [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, SwaggerDocumentProperty>? Parameters { get; set; } = [];

    /// <summary>
    /// Security options available in the Swagger document.
    /// </summary>
    [JsonProperty("security", NullValueHandling = NullValueHandling.Ignore)]
    public List<SwaggerDocumentSecurityRequirement>? Security { get; set; } = [];
}

/// <summary>
/// Defines the required security schemes to execute an operation.
/// </summary>
public class SwaggerDocumentSecurityRequirement : Dictionary<string, List<string>>;

/// <summary>
/// Defines the metadata about the API.
/// </summary>
public class SwaggerDocumentInfo
{
    /// <summary>
    /// API Title.
    /// </summary>
    [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
    public string Title { get; set; }

    /// <summary>
    /// API Version.
    /// </summary>
    [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
    public string Version { get; set; }
}

public class SwaggerDocumentComponents
{
    [JsonProperty("schemas", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, SwaggerDocumentSchemeItem>? Schemes { get; set; } = [];
}

public class SwaggerDocumentSchemeItem
{
    [JsonProperty("enum", NullValueHandling = NullValueHandling.Ignore)]
    public int[]? Enum { get; set; }

    [JsonProperty("format", NullValueHandling = NullValueHandling.Ignore)]
    public string? Format { get; set; }

    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public string? Type { get; set; }

    [JsonProperty("additionalProperties", NullValueHandling = NullValueHandling.Ignore)]
    public bool? AdditionalProperties { get; set; }

    [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, SwaggerDocumentProperty>? Properties { get; set; }

    [JsonProperty("required", NullValueHandling = NullValueHandling.Ignore)]
    public string[]? Required { get; set; }
}

/// <summary>
/// Defines the relative paths to the individual endpoints.
/// </summary>
public class SwaggerDocumentPaths : Dictionary<string, SwaggerDocumentPathItem>;

/// <summary>
/// Defines the operations available on a single path.
/// </summary>
public class SwaggerDocumentPathItem : Dictionary<string, SwaggerDocumentOperation>;
