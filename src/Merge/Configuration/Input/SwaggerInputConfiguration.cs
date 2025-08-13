namespace NSwaggerMerge.Merge.Configuration.Input;

/// <summary>
/// Defines the configuration for Swagger document input.
/// </summary>
public class SwaggerInputConfiguration
{
    /// <summary>
    /// File path to Swagger document.
    /// </summary>
    public string File { get; set; } = string.Empty;

    /// <summary>
    /// Configuration for modifying the document's paths.
    /// </summary>
    public SwaggerInputPathConfiguration? Path { get; set; }

    /// <summary>
    /// Configuration for modifying the document's description.
    /// </summary>
    public SwaggerInputInfoConfiguration? Info { get; set; }
}
