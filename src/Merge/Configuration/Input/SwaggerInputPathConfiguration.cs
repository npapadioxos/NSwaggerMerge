namespace NSwaggerMerge.Merge.Configuration.Input;

/// <summary>
/// Modify a Swagger document's paths.
/// </summary>
public class SwaggerInputPathConfiguration
{
    /// <summary>
    /// Strip from the start of paths.
    /// </summary>
    public string? StripStart { get; set; }

    /// <summary>
    /// Prepend to the start of paths.
    /// </summary>
    public string? Prepend { get; set; }

    /// <summary>
    /// Exclusions for the output for path operation implementations based on a key-value match.
    /// </summary>
    public SwaggerInputPathOperationExclusionConfiguration? OperationExclusions { get; set; }
}
