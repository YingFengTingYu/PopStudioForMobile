using System.Text.Json.Serialization;

namespace PopStudio.Settings
{
    public class RtonSetting
    {
        [JsonPropertyName("cipher")]
        public string Cipher { get; set; }

        public void Init()
        {
            Cipher ??= string.Empty;
        }
    }
}
