using NSwaggerMerge.Merge.Exceptions;
using NSwaggerMerge.Merge.Configuration;

namespace NSwaggerMerge.Merge;

public static partial class SwaggerMerger
{
    /// <summary>
    /// Validates the provided Swagger merge configuration.
    /// </summary>
    /// <param name="config"></param>
    /// <exception cref="SwaggerMergeException"></exception>
    public static void ValidateConfiguration(SwaggerMergeConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(config, nameof(config));

        if (!config.Inputs.Any())
        {
            throw new SwaggerMergeException("At least 1 input file must be specified");
        }

        if (config.Inputs.Any(input => string.IsNullOrWhiteSpace(input.File)))
        {
            throw new SwaggerMergeException("All input file paths must be specified");
        }
    }
}
