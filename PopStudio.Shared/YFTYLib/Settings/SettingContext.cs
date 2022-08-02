using System.Text.Json.Serialization;

namespace PopStudio.Settings
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(GlobalSetting))]
    internal partial class SettingContext : JsonSerializerContext
    {
    }
}
