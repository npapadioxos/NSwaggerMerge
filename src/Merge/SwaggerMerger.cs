namespace NSwaggerMerge.Merge;

using NSwaggerMerge.Merge.Configuration;
using NSwaggerMerge.Serialization;
using NSwaggerMerge.Swagger;

public static partial class SwaggerMerger
{
    public static async Task<string> MergeAsync(SwaggerMergeConfiguration config)
    {
        var output = new SwaggerDocument { Host = config.Output.Host, BasePath = config.Output.BasePath };

        var outputTitle = config.Output.Info?.Title ?? string.Empty;

        foreach (var inputConfig in config.Inputs)
        {
            SwaggerDocument input = await JsonFile.LoadRemoteFileAsync<SwaggerDocument>(inputConfig.File);

            outputTitle = UpdateOutputTitleFromInput(outputTitle, inputConfig, input);
            UpdateOutputPathsFromInput(output, inputConfig, input);
            UpdateOutputDefinitionsFromInput(output, input);
        }

        FinalizeOutput(output, outputTitle, config);

        return JsonFile.OutJsonAsync(output);
    }

    private static void FinalizeOutput(SwaggerDocument? output, string outputTitle, SwaggerMergeConfiguration config)
    {
        if (output == null)
            return;

        output.Info.Title = outputTitle;
        output.Schemes = config.Output.Schemes ?? [];
        output.Security = config.Output.Security ?? [];
        output.Info.Version = config.Output.Info?.Version ?? "1.0";
        output.SecurityDefinitions = config.Output.SecurityDefinitions ?? [];
    }
}