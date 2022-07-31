using System.Text.Json.Serialization;

namespace PopStudio.Settings
{
    public class RsbSetting
    {
        public void Init()
        {
        }

        [JsonPropertyName("decode_special_image")]
        public bool DecodeImage { get; set; }

        [JsonPropertyName("delete_special_image_after_decoding")]
        public bool DeleteDecodedImage { get; set; }
    }
}
