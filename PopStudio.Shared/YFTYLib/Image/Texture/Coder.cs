using System.Collections.Generic;

namespace PopStudio.Image.Texture
{
    public unsafe static class Coder
    {
        static Dictionary<TextureFormat, ICoder> TexCoderMap = new Dictionary<TextureFormat, ICoder>
        {
            { TextureFormat.NONE, new TexCoder.NONE() },
            { TextureFormat.B8_G8_R8_A8, new TexCoder.B8_G8_R8_A8() },
            { TextureFormat.R8_G8_B8_A8, new TexCoder.R8_G8_B8_A8() },
            { TextureFormat.R4_G4_B4_A4, new TexCoder.R4_G4_B4_A4() },
            { TextureFormat.A4_R4_G4_B4, new TexCoder.A4_R4_G4_B4() },
            { TextureFormat.R5_G5_B5_A1, new TexCoder.R5_G5_B5_A1() },
            { TextureFormat.A1_R5_G5_B5, new TexCoder.A1_R5_G5_B5() },
            { TextureFormat.R5_G6_B5, new TexCoder.R5_G6_B5() },
            { TextureFormat.R8_G8_B8, new TexCoder.R8_G8_B8() },
            { TextureFormat.R4_G4_B4_A4_BLOCK, new TexCoder.R4_G4_B4_A4_BLOCK() },
            { TextureFormat.R5_G5_B5_A1_BLOCK, new TexCoder.R5_G5_B5_A1_BLOCK() },
            { TextureFormat.R5_G6_B5_BLOCK, new TexCoder.R5_G6_B5_BLOCK() },
            { TextureFormat.RGB_PVRTCI_4BPP, new TexCoder.RGB_PVRTCI_4BPP() },
            { TextureFormat.RGBA_PVRTCI_4BPP, new TexCoder.RGBA_PVRTCI_4BPP() },
            { TextureFormat.RGB_PVRTCI_2BPP, new TexCoder.RGB_PVRTCI_2BPP() },
            { TextureFormat.RGBA_PVRTCI_2BPP, new TexCoder.RGBA_PVRTCI_2BPP() },
            { TextureFormat.RGBA_PVRTCII_4BPP, new TexCoder.RGBA_PVRTCII_4BPP() },
            { TextureFormat.RGBA_PVRTCII_2BPP, new TexCoder.RGBA_PVRTCII_2BPP() },
            { TextureFormat.RGB_ATC, new TexCoder.RGB_ATC() },
            { TextureFormat.RGB_A_EXPLICIT_ATC, new TexCoder.RGB_A_EXPLICIT_ATC() },
            { TextureFormat.RGB_A_INTERPOLATED_ATC, new TexCoder.RGB_A_INTERPOLATED_ATC() },
            { TextureFormat.RGB_DXT1, new TexCoder.RGB_DXT1() },
            { TextureFormat.RGBA_DXT1, new TexCoder.RGBA_DXT1() },
            { TextureFormat.RGBA_DXT3, new TexCoder.RGBA_DXT3() },
            { TextureFormat.RGBA_DXT5, new TexCoder.RGBA_DXT5() },
            { TextureFormat.RGB_ETC1, new TexCoder.RGB_ETC1() },
            { TextureFormat.RGB_ETC1_ADD_A8, new TexCoder.RGB_ETC1_ADD_A8() },
            { TextureFormat.RGB_ETC1_ADD_A_PALETTE, new TexCoder.RGB_ETC1_ADD_A_PALETTE() },
            { TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8, new TexCoder.RGBA_PVRTCI_4BPP_ADD_A8() },
            { TextureFormat.B8_G8_R8_A8_ADD_A8, new TexCoder.B8_G8_R8_A8_ADD_A8() },
            { TextureFormat.R8_G8_B8_A8_ADD_A8, new TexCoder.R8_G8_B8_A8_ADD_A8() },
            { TextureFormat.RGBA_DXT5_BLOCK_MORTON, new TexCoder.RGBA_DXT5_BLOCK_MORTON() },
            { TextureFormat.A8_R8_G8_B8, new TexCoder.A8_R8_G8_B8() },
            { TextureFormat.A8_R8_G8_B8_PADDING, new TexCoder.A8_R8_G8_B8_PADDING() },
            { TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING, new TexCoder.RGBA_DXT5_BIGENDIAN_PADDING() },
            { TextureFormat.RGBA_DXT5_REFLECTEDMORTON, new TexCoder.RGBA_DXT5_REFLECTEDMORTON() },
            { TextureFormat.A4_R4_G4_B4_BIGENDIAN_PADDING, new TexCoder.A4_R4_G4_B4_BIGENDIAN_PADDING() },
            { TextureFormat.R5_G6_B5_BIGENDIAN_PADDING, new TexCoder.R5_G6_B5_BIGENDIAN_PADDING() },
        };

        static ICoder GetTexCoder(TextureFormat fmt)
        {
            if (TexCoderMap.TryGetValue(fmt, out ICoder coder)) return coder;
            return TexCoderMap[TextureFormat.NONE];
        }

        public static YFBitmap Decode(YFTexture2D tex)
        {
            ICoder coder = GetTexCoder(tex.TexFormat);
            if (coder.CheckWidth(tex.Width)
                && coder.CheckHeight(tex.Height)
                && coder.CheckWidthHeight(tex.Width, tex.Height))
            {
                YFBitmap bitmap = YFBitmap.Create(tex.Width, tex.Height);
                coder.Decode(tex, (YFColor*)bitmap.Pixels.ToPointer());
                return bitmap;
            }
            return null;
        }

        public static YFTexture2D Encode(YFBitmap bitmap, TextureFormat format)
        {
            YFTexture2D tex = new YFTexture2D();
            tex.Width = bitmap.Width;
            tex.Height = bitmap.Height;
            tex.TexFormat = format;
            ICoder coder = GetTexCoder(tex.TexFormat);
            if (coder.CheckWidth(tex.Width)
                && coder.CheckHeight(tex.Height)
                && coder.CheckWidthHeight(tex.Width, tex.Height))
            {
                tex.Check = coder.GetCheck(tex.Width);
                tex.TexData = new byte[coder.GetSize(tex.Width, tex.Height)];
                coder.Encode(tex, (YFColor*)bitmap.Pixels.ToPointer());
                return tex;
            }
            return null;
        }
    }
}
