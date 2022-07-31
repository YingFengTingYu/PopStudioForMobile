using PopStudio.Image.Texture;
using PopStudio.Plugin;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PopStudio.Settings
{
    public class PtxRsbSetting
    {
        public void Init()
        {
            FormatMapSmallEndian ??= new List<FormatPair>
            {
                new FormatPair
                {
                    Index = 0,
                    Format = TextureFormat.B8_G8_R8_A8
                },
                new FormatPair
                {
                    Index = 1,
                    Format = TextureFormat.R4_G4_B4_A4
                },
                new FormatPair
                {
                    Index = 2,
                    Format = TextureFormat.R5_G6_B5
                },
                new FormatPair
                {
                    Index = 3,
                    Format = TextureFormat.R5_G5_B5_A1
                },
                new FormatPair
                {
                    Index = 5,
                    Format = TextureFormat.RGBA_DXT5_BLOCK_MORTON
                },
                new FormatPair
                {
                    Index = 21,
                    Format = TextureFormat.R4_G4_B4_A4_BLOCK
                },
                new FormatPair
                {
                    Index = 22,
                    Format = TextureFormat.R5_G6_B5_BLOCK
                },
                new FormatPair
                {
                    Index = 23,
                    Format = TextureFormat.R5_G5_B5_A1_BLOCK
                },
                new FormatPair
                {
                    Index = 30,
                    Format = TextureFormat.RGBA_PVRTCI_4BPP
                },
                new FormatPair
                {
                    Index = 31,
                    Format = TextureFormat.RGBA_PVRTCI_2BPP
                },
                new FormatPair
                {
                    Index = 32,
                    Format = TextureFormat.RGB_ETC1
                },
                new FormatPair
                {
                    Index = 35,
                    Format = TextureFormat.RGB_DXT1
                },
                new FormatPair
                {
                    Index = 36,
                    Format = TextureFormat.RGBA_DXT3
                },
                new FormatPair
                {
                    Index = 37,
                    Format = TextureFormat.RGBA_DXT5
                },
                new FormatPair
                {
                    Index = 38,
                    Format = TextureFormat.RGB_ATC
                },
                new FormatPair
                {
                    Index = 39,
                    Format = TextureFormat.RGB_A_EXPLICIT_ATC
                },
                new FormatPair
                {
                    Index = 147,
                    Format = TextureFormat.RGB_ETC1_ADD_A8
                },
                new FormatPair
                {
                    Index = 148,
                    Format = TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8
                },
                new FormatPair
                {
                    Index = 149,
                    Format = TextureFormat.B8_G8_R8_A8_ADD_A8
                }
            };
            FormatMapBigEndian ??= new List<FormatPair>
            {
                new FormatPair
                {
                    Index = 0,
                    Format = TextureFormat.A8_R8_G8_B8
                },
                new FormatPair
                {
                    Index = 5,
                    Format = TextureFormat.RGBA_DXT5
                }
            };
            DefaultFormatSmallEndian ??= TextureFormat.R4_G4_B4_A4;
            DefaultFormatBigEndian ??= TextureFormat.NONE;
        }

        [JsonPropertyName("format_little_endian")]
        public List<FormatPair> FormatMapSmallEndian { get; set; }

        [JsonPropertyName("format_big_endian")]
        public List<FormatPair> FormatMapBigEndian { get; set; }

        [JsonPropertyName("format_little_endian_default")]
        public TextureFormat? DefaultFormatSmallEndian { get; set; }

        [JsonPropertyName("format_big_endian_default")]
        public TextureFormat? DefaultFormatBigEndian { get; set; }

        public List<(string, int, Endian)> GetStringList()
        {
            List<(string, int, Endian)> ans = new List<(string, int, Endian)>();
            lock (FormatMapSmallEndian)
            {
                lock (FormatMapBigEndian)
                {
                    foreach (FormatPair pair in FormatMapSmallEndian)
                    {
                        ans.Add((pair.Format.ToString(), pair.Index, Endian.Small));
                    }
                    foreach (FormatPair pair in FormatMapBigEndian)
                    {
                        ans.Add((pair.Format.ToString(), pair.Index, Endian.Big));
                    }
                }
            }
            return ans;
        }

        public TextureFormat GetTextureFormat(int index, Endian endian)
        {
            lock (FormatMapSmallEndian)
            {
                lock (FormatMapBigEndian)
                {
                    (List<FormatPair> map, TextureFormat? defaultFormat) = endian switch
                    {
                        Endian.Small => (FormatMapSmallEndian, DefaultFormatSmallEndian),
                        Endian.Big => (FormatMapBigEndian, DefaultFormatBigEndian),
                        _ => (null, TextureFormat.NONE)
                    };
                    return map?.Find(value => value.Index == index)?.Format ?? defaultFormat ?? TextureFormat.NONE;
                }
            }
        }
    }
}
