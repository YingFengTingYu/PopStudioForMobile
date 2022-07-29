using System.Text.Json.Serialization;

namespace PopStudio.PlatformAPI
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(YFFileSystem.YFDirectory))]
    public partial class YFDirectoryJsonContext : JsonSerializerContext
    {
    }
}
