using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSwaggerMerge.Serialization;
using System.Globalization;

namespace NSwaggerMerge.Swagger;

internal static class Helper
{
    internal static SwaggerDocumentProperty ToSwaggerDocumentProperty(JToken? jsonObject)
    {
        if (jsonObject == null)
            return new();

        using StringWriter sw = new(CultureInfo.InvariantCulture);
        JsonTextWriter jw = new(sw);
        jsonObject.WriteTo(jw);
        var json = sw.ToString();
        return JsonConvert.DeserializeObject<SwaggerDocumentProperty>(json, JsonFile.Settings) ?? new();
    }
}
