using System.Text.Json.Serialization;

namespace PopStudio.SourceGen
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(PlatformAPI.YFFileSystem.YFDirectory))]
    public partial class YFFileSystemSourceGenerationContext : JsonSerializerContext
    {
    }
}
