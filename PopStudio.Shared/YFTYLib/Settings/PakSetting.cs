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

        public bool SetFlags(string format, Package.Pak.PakCompressionFlags flags, bool special)
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

        public bool AddFlags(string format, Package.Pak.PakCompressionFlags flags)
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
