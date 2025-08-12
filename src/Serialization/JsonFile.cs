using Newtonsoft.Json;

namespace NSwaggerMerge.Serialization;

internal static class JsonFile
{
    internal static readonly JsonSerializerSettings Settings = new()
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore
    };

    public static async Task<T> LoadFileAsync<T>(string filePath) where T : class
    {
        var content = await File.ReadAllTextAsync(filePath);
        var deserializedContent = JsonConvert.DeserializeObject<T>(content, Settings);

        return deserializedContent ?? throw new InvalidOperationException($"File '{filePath}' could not be loaded correctly as it is not in the correct format.");
    }

    public static async Task<T> LoadRemoteFileAsync<T>(string httpPath) where T : class
    {
        using HttpClient client = new();
        var response = client.GetAsync(httpPath).Result.Content.ReadAsStringAsync().Result;
        var deserializedContent = JsonConvert.DeserializeObject<T>(response, Settings);

        return deserializedContent ?? throw new InvalidOperationException($"HttpFile '{httpPath}' could not be loaded correctly as it is not in the correct format.");
    }

    public static async Task SaveFileAsync<T>(string filePath, T data) where T : class
    {
        var content = JsonConvert.SerializeObject(data, Settings);
        await File.WriteAllTextAsync(filePath, content);
    }

    public static string OutJsonAsync<T>(T data) where T : class =>
        JsonConvert.SerializeObject(data, Settings);
}
