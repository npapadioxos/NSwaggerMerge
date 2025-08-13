using MADE.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSwaggerMerge.Serialization;
using System.Globalization;
using System.Runtime.Serialization;

namespace NSwaggerMerge.Swagger;

/// <summary>
/// Defines the details of a Swagger document.
/// </summary>
public class SwaggerDocument
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

    /// <summary>
    /// Properties that are not covered by the defined Swagger properties.
    /// </summary>
    [JsonIgnore]
    public Dictionary<string, SwaggerDocumentProperty>? AdditionalProperties { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JToken>? JTokenProperties { get; set; }

    private static SwaggerDocumentProperty ToSwaggerDocumentProperty(JToken? jsonObject)
    {
        if (jsonObject == null)
            return new();

        using StringWriter sw = new(CultureInfo.InvariantCulture);
        JsonTextWriter jw = new(sw);
        jsonObject.WriteTo(jw);
        var json = sw.ToString();
        return JsonConvert.DeserializeObject<SwaggerDocumentProperty>(json, JsonFile.Settings) ?? new();
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
        var objectTokens = JTokenProperties?.Where(x => x.Value is JObject).ToList();

        if (objectTokens == null || objectTokens.Count == 0)
            return;

        AdditionalProperties = objectTokens.ToDictionary(
            x => x.Key,
            x => ToSwaggerDocumentProperty(x.Value as JObject));

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

/// <summary>
/// Defines the required security schemes to execute an operation.
/// </summary>
public class SwaggerDocumentSecurityRequirement : Dictionary<string, List<string>>;

/// <summary>
/// Defines the details of an object to hold security schemes to be reused across the specification.
/// </summary>
public class SwaggerDocumentSecurityDefinitions : Dictionary<string, SwaggerDocumentSecurityScheme>;

/// <summary>
/// Defines the security scheme that can be used by the operations.
/// </summary>
public class SwaggerDocumentSecurityScheme
{
    /// <summary>
    /// Type of security scheme.
    /// </summary>
    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public string? Type { get; set; }

    /// <summary>
    /// Short description of the security scheme.
    /// </summary>
    [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
    public string? Description { get; set; }

    /// <summary>
    /// Name of the header or query parameter to be used.
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string? Name { get; set; }

    /// <summary>
    /// Location of the API key.
    /// </summary>
    [JsonProperty("in", NullValueHandling = NullValueHandling.Ignore)]
    public string? In { get; set; }

    /// <summary>
    /// OAuth2 flow.
    /// </summary>
    [JsonProperty("flow", NullValueHandling = NullValueHandling.Ignore)]
    public string? Flow { get; set; }

    /// <summary>
    /// Authorization URL to be used for this flow.
    /// </summary>
    [JsonProperty("authorizationUrl", NullValueHandling = NullValueHandling.Ignore)]
    public string? AuthorizationUrl { get; set; }

    /// <summary>
    /// Token URL to be used for this flow.
    /// </summary>
    [JsonProperty("tokenUrl", NullValueHandling = NullValueHandling.Ignore)]
    public string? TokenUrl { get; set; }

    /// <summary>
    /// Available scopes for the OAuth2 security scheme.
    /// </summary>
    [JsonProperty("scopes", NullValueHandling = NullValueHandling.Ignore)]
    public SwaggerDocumentScopes? Scopes { get; set; }

    /// <summary>
    /// Properties that are not covered by the defined Swagger properties.
    /// </summary>
    [JsonIgnore]
    public Dictionary<string, SwaggerDocumentProperty>? AdditionalProperties { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JToken>? JTokenProperties { get; set; }

    private static SwaggerDocumentProperty ToSwaggerDocumentProperty(JToken? jsonObject)
    {
        if (jsonObject == null)
            return new();

        using StringWriter sw = new(CultureInfo.InvariantCulture);
        JsonTextWriter jw = new(sw);
        jsonObject.WriteTo(jw);
        var json = sw.ToString();
        return JsonConvert.DeserializeObject<SwaggerDocumentProperty>(json, JsonFile.Settings) ?? new();
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
        var objectTokens = JTokenProperties?.Where(x => x.Value is JObject).ToList();

        if (objectTokens == null || objectTokens.Count == 0)
            return;

        AdditionalProperties = objectTokens.ToDictionary(
            x => x.Key,
            x => ToSwaggerDocumentProperty(x.Value as JObject));

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

/// <summary>
/// Defines the list of available scopes for an OAuth2 security scheme.
/// </summary>
public class SwaggerDocumentScopes : Dictionary<string, object>;

/// <summary>
/// Defines the details of an object to hold data types that can be consumed and produced by operations.
/// </summary>
public class SwaggerDocumentDefinitions : Dictionary<string, SwaggerDocumentProperty>
{
    public SwaggerDocumentDefinitions()
    {
    }

    public SwaggerDocumentDefinitions(IDictionary<string, SwaggerDocumentProperty> dictionary)
        : base(dictionary)
    {
    }
}

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
    public SwaggerDocumentSchemes? Schemes { get; set; } = [];
}

public class SwaggerDocumentSchemes : Dictionary<string, SwaggerDocumentSchemeItem>;

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

/// <summary>
/// Defines the detail of a single API operation on a path.
/// </summary>
public class SwaggerDocumentOperation
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

    /// <summary>
    /// Properties that are not covered by the defined Swagger properties.
    /// </summary>
    [JsonIgnore]
    public Dictionary<string, SwaggerDocumentProperty>? AdditionalProperties { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JToken>? JTokenProperties { get; set; }

    private static SwaggerDocumentProperty ToSwaggerDocumentProperty(JToken? jsonObject)
    {
        if (jsonObject == null)
            return new();

        using StringWriter sw = new(CultureInfo.InvariantCulture);
        JsonTextWriter jw = new(sw);
        jsonObject.WriteTo(jw);
        var json = sw.ToString();
        return JsonConvert.DeserializeObject<SwaggerDocumentProperty>(json, JsonFile.Settings) ?? new();
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
        var objectTokens = JTokenProperties?.Where(x => x.Value is JObject).ToList();

        if (objectTokens == null || objectTokens.Count == 0)
            return;

        AdditionalProperties = objectTokens.ToDictionary(
            x => x.Key,
            x => ToSwaggerDocumentProperty(x.Value as JObject));

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

/// <summary>
/// Defines the detail of a generic Swagger document property.
/// </summary>
public class SwaggerDocumentProperty
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

    /// <summary>
    /// Properties that are not covered by the defined Swagger properties.
    /// </summary>
    [JsonIgnore]
    public Dictionary<string, SwaggerDocumentProperty>? AdditionalProperties { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JToken>? JTokenProperties { get; set; }

    private static SwaggerDocumentProperty ToSwaggerDocumentProperty(JToken? jsonObject)
    {
        if (jsonObject == null)
            return new();

        using StringWriter sw = new(CultureInfo.InvariantCulture);
        JsonTextWriter jw = new(sw);
        jsonObject.WriteTo(jw);
        var json = sw.ToString();
        return JsonConvert.DeserializeObject<SwaggerDocumentProperty>(json, JsonFile.Settings) ?? new();
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
        var objectTokens = JTokenProperties?.Where(x => x.Value is JObject).ToList();

        if (objectTokens == null || objectTokens.Count == 0)
            return;

        AdditionalProperties = objectTokens.ToDictionary(
            x => x.Key,
            x => ToSwaggerDocumentProperty(x.Value as JObject));

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
