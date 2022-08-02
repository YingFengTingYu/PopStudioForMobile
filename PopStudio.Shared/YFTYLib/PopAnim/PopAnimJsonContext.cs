using System.Text.Json.Serialization;

namespace PopStudio.PopAnim
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(PopAnimInfo))]
    internal partial class PopAnimJsonContext : JsonSerializerContext
    {
    }
}
