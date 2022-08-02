using System.Text.Json.Serialization;

namespace PopStudio.Settings
{
    public class CdatSetting
    {
        [JsonPropertyName("cipher")]
        public string Cipher { get; set; }

        public void Init()
        {
            Cipher ??= "AS23DSREPLKL335KO4439032N8345NF";
        }
    }
}
