namespace NSwaggerMerge.Merge.Configuration.Input;

/// <summary>
/// Modify a Swagger doc description.
/// </summary>
public class SwaggerInputInfoConfiguration
{
    /// <summary>
    /// Append document title option.
    /// </summary>
    public bool Append { get; set; }

    /// <summary>
    /// Doc Title to append.
    /// </summary>
    /// <remarks>
    /// If not set, the Swagger document's title will be used.
    /// </remarks>
    public string? Title { get; set; }
}
