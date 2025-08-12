using NSwaggerMerge.Merge.Exceptions;
using NSwaggerMerge.Merge.Configuration;

namespace NSwaggerMerge.Merge;

public static partial class SwaggerMerger
{
    public static void ValidateConfiguration(SwaggerMergeConfiguration config)
    {
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
