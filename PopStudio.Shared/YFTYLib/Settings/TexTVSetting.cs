using PopStudio.Image.Texture;
using PopStudio.Plugin;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PopStudio.Settings
{
    public class TexTVSetting
    {
        public void Init()
        {
            FormatMap ??= new List<FormatPair>
            {
                new FormatPair
                {
                    Index = 2,
                    Format = TextureFormat.B8_G8_R8_A8
                },
                new FormatPair
                {
                    Index = 3,
                    Format = TextureFormat.A4_R4_G4_B4
                },
                new FormatPair
                {
                    Index = 4,
                    Format = TextureFormat.A1_R5_G5_B5
                },
                new FormatPair
                {
                    Index = 5,
                    Format = TextureFormat.R5_G6_B5
                },
                new FormatPair
                {
                    Index = 6,
                    Format = TextureFormat.R8_G8_B8_A8
                },
                new FormatPair
                {
                    Index = 7,
                    Format = TextureFormat.R4_G4_B4_A4
                },
                new FormatPair
                {
                    Index = 8,
                    Format = TextureFormat.R5_G5_B5_A1
                },
            };
        }

        [JsonPropertyName("format")]
        public List<FormatPair> FormatMap { get; set; }

        [JsonPropertyName("use_zlib_compression")]
        public bool UseZlib { get; set; } = true;

        public List<(string, int, Endian)> GetStringList()
        {
            List<(string, int, Endian)> ans = new List<(string, int, Endian)>();
            lock (FormatMap)
            {
                foreach (FormatPair pair in FormatMap)
                {
                    ans.Add((pair.Format.ToString(), pair.Index, Endian.Small));
                }
            }
            return ans;
        }

        public TextureFormat GetTextureFormat(int index)
        {
            lock (FormatMap)
            {
                return FormatMap?.Find(value => value.Index == index)?.Format ?? TextureFormat.NONE;
            }
        }

        public bool SetFlags(int index, TextureFormat format)
        {
            lock (FormatMap)
            {
                FormatPair pair = FormatMap.Find(value => value.Index == index);
                if (pair is not null)
                {
                    pair.Format = format;
                    return true;
                }
                return false;
            }
        }

        public bool AddFlags(int index, TextureFormat format)
        {
            lock (FormatMap)
            {
                FormatPair pair = FormatMap.Find(value => value.Index == index);
                if (pair is not null)
                {
                    return false;
                }
                FormatMap.Add(new FormatPair
                {
                    Index = index,
                    Format = format
                });
                return true;
            }
        }

        public bool RemoveFormat(int index)
        {
            lock (FormatMap)
            {
                FormatPair pair = FormatMap.Find(value => value.Index == index);
                if (pair is not null)
                {
                    FormatMap.Remove(pair);
                    return true;
                }
                return false;
            }
        }
    }
}
