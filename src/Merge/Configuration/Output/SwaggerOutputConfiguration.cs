using NSwaggerMerge.Swagger;

namespace NSwaggerMerge.Merge.Configuration.Output;

/// <summary>
/// Configure Swagger document output.
/// </summary>
public class SwaggerOutputConfiguration
{
    /// <summary>
    /// Host (name or IP) service the API for the output.
    /// </summary>
    public string Host { get; set; } = string.Empty;

    /// <summary>
    /// Base path relative to <see cref="Host"/> on which the API is served.
    /// </summary>
    public string? BasePath { get; set; }

    /// <summary>
    /// Transfer protocol of the API.
    /// </summary>
    public List<string>? Schemes { get; set; }

    /// <summary>
    /// Security scheme to be defined for the output.
    /// </summary>
    public SwaggerDocumentSecurityDefinitions? SecurityDefinitions { get; set; } = [];

    /// <summary>
    /// Ssecurity options available in the output.
    /// </summary>
    public List<SwaggerDocumentSecurityRequirement>? Security { get; set; } = [];

    /// <summary>
    /// Configuration for the document's description.
    /// </summary>
    public SwaggerOutputInfoConfiguration? Info { get; set; }
}
