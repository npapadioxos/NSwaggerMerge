using Newtonsoft.Json.Linq;

namespace NSwaggerMerge.Merge.Configuration.Input;

/// <summary>
/// Define path operation exclusions based on a defined property match.
/// </summary>
public class SwaggerInputPathOperationExclusionConfiguration : Dictionary<string, JToken>;
