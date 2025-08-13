using Newtonsoft.Json;

namespace NSwaggerMerge.Swagger;

/// <summary>
/// Defines the security scheme that can be used by the operations.
/// </summary>
public class SwaggerDocumentSecurityScheme : SwaggerDocumentBase
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
    public Dictionary<string, object>? Scopes { get; set; }
}
