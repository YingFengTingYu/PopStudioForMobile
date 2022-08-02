using System.Text.Json.Serialization;

namespace PopStudio.Reanim
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(Reanim))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(int))]
    internal partial class ReanimJsonContext : JsonSerializerContext
    {
    }
}
