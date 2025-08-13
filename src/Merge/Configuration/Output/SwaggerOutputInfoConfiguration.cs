namespace NSwaggerMerge.Merge.Configuration.Output;

/// <summary>
/// Defines the configuration for modifying the output Swagger document's description.
/// </summary>
public class SwaggerOutputInfoConfiguration
{
    /// <summary>
    /// Merged document's title.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Merged document's version.
    /// </summary>
    public string? Version { get; set; }
}
