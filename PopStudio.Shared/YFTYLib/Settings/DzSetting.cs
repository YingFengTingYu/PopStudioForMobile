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

        public bool SetFlags(string format, Package.Dz.DzCompressionFlags flags, bool special)
        {
            if (special)
            {
                DefaultFlags = flags;
                return true;
            }
            lock (CompressDictionary)
            {
                if (CompressDictionary.ContainsKey(format))
                {
                    CompressDictionary[format] = flags;
                    return true;
                }
                return false;
            }
        }

        public bool AddFlags(string format, Package.Dz.DzCompressionFlags flags)
        {
            lock (CompressDictionary)
            {
                if (CompressDictionary.ContainsKey(format))
                {
                    return false;
                }
                CompressDictionary.Add(format, flags);
                return true;
            }
        }

        public bool RemoveFormat(string format)
        {
            lock (CompressDictionary)
            {
                if (CompressDictionary.ContainsKey(format))
                {
                    return CompressDictionary.Remove(format);
                }
                return false;
            }
        }
    }
}
