using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PopStudio.Settings
{
    public class DzSetting
    {
        public void Init()
        {
            CompressDictionary ??= new Dictionary<string, Package.Dz.DzCompressionFlags>
            {
                { ".png", Package.Dz.DzCompressionFlags.STORE }
            };
            DefaultFlags ??= Package.Dz.DzCompressionFlags.LZMA;
        }

        [JsonPropertyName("decode_special_image")]
        public bool DecodeImage { get; set; }

        [JsonPropertyName("delete_special_image_after_decoding")]
        public bool DeleteDecodedImage { get; set; }

        [JsonPropertyName("compression_method")]
        public Dictionary<string, Package.Dz.DzCompressionFlags> CompressDictionary { get; set; }

        [JsonPropertyName("compression_method_default")]
        public Package.Dz.DzCompressionFlags? DefaultFlags { get; set; }

        public Package.Dz.DzCompressionFlags GetFlags(string format)
        {
            lock (CompressDictionary)
            {
                return CompressDictionary.TryGetValue(format.ToLower(), out Package.Dz.DzCompressionFlags v) ? v : (DefaultFlags ?? Package.Dz.DzCompressionFlags.STORE);
            }
        }
    }
}
