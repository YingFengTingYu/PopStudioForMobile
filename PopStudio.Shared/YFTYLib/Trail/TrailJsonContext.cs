using System.Text.Json.Serialization;

namespace PopStudio.Trail
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(Trail))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(int))]
    internal partial class TrailJsonContext : JsonSerializerContext
    {
    }
}
