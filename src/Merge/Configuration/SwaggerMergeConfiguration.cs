using NSwaggerMerge.Merge.Configuration.Input;
using NSwaggerMerge.Merge.Configuration.Output;

namespace NSwaggerMerge.Merge.Configuration;

/// <summary>
/// Defines the configuration for merging Swagger documents.
/// </summary>
public class SwaggerMergeConfiguration
{
    /// <summary>
    /// Inputs for merging.
    /// </summary>
    public IEnumerable<SwaggerInputConfiguration> Inputs { get; set; } = [];

    /// <summary>
    /// Output merged Swagger document.
    /// </summary>
    public SwaggerOutputConfiguration Output { get; set; } = new();
}
