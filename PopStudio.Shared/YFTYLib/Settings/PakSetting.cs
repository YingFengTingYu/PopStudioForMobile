using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PopStudio.Settings
{
    public class PakSetting
    {
        public void Init()
        {
            CompressDictionary ??= new Dictionary<string, Package.Pak.PakCompressionFlags>
            {
                { ".png", Package.Pak.PakCompressionFlags.STORE }
            };
            DefaultFlags ??= Package.Pak.PakCompressionFlags.ZLIB;
        }

        [JsonPropertyName("decode_special_image_for_console")]
        public bool DecodeImage { get; set; }

        [JsonPropertyName("delete_special_image_after_decoding_for_console")]
        public bool DeleteDecodedImage { get; set; }

        [JsonPropertyName("compression_method_for_ps3_psv")]
        public Dictionary<string, Package.Pak.PakCompressionFlags> CompressDictionary { get; set; }

        [JsonPropertyName("compression_method_for_ps3_psv_default")]
        public Package.Pak.PakCompressionFlags? DefaultFlags { get; set; }

        public Package.Pak.PakCompressionFlags GetFlags(string format)
        {
            lock (CompressDictionary)
            {
                return CompressDictionary.TryGetValue(format.ToLower(), out Package.Pak.PakCompressionFlags v) ? v : (DefaultFlags ?? Package.Pak.PakCompressionFlags.STORE);
            }
        }
    }
}
