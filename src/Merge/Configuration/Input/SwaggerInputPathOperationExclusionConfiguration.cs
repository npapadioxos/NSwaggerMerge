using Newtonsoft.Json.Linq;

namespace NSwaggerMerge.Merge.Configuration.Input;

/// <summary>
/// Defines the configuration for defining path operation exclusions based on a defined property match.
/// </summary>
public class SwaggerInputPathOperationExclusionConfiguration : Dictionary<string, JToken>;
