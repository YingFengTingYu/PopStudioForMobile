using System.Text.Json.Serialization;

namespace PopStudio.PlatformAPI
{
    [JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = true)]
    [JsonSerializable(typeof(YFFileSystem.YFDirectory))]
    public partial class YFDirectoryJsonContext : JsonSerializerContext
    {
    }
}
