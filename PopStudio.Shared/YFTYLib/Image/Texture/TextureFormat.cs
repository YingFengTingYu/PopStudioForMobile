namespace PopStudio.Image.Texture
{
    public enum TextureFormat : uint
    {
        NONE,
        B8_G8_R8_A8,
        B8_G8_R8_A8_ADD_A8,
        R8_G8_B8_A8,
        R8_G8_B8_A8_ADD_A8,
        A8_R8_G8_B8,
        A8_R8_G8_B8_PADDING,
        R4_G4_B4_A4,
        R4_G4_B4_A4_BLOCK,
        A4_R4_G4_B4,
        R5_G5_B5_A1,
        R5_G5_B5_A1_BLOCK,
        A1_R5_G5_B5,
        R5_G6_B5,
        R5_G6_B5_BLOCK,
        R8_G8_B8,
        RGB_DXT1,
        RGBA_DXT1,
        RGBA_DXT3,
        RGBA_DXT5,
        RGBA_DXT5_BLOCK_MORTON,
        RGBA_DXT5_REFLECTEDMORTON,
        RGBA_DXT5_BIGENDIAN_PADDING,
        RGB_ETC1,
        RGB_ETC1_ADD_A8,
        RGB_ETC1_ADD_A_PALETTE,
        RGB_PVRTCI_4BPP,
        RGBA_PVRTCI_4BPP,
        RGBA_PVRTCI_4BPP_ADD_A8,
        RGB_PVRTCI_2BPP,
        RGBA_PVRTCI_2BPP,
        RGBA_PVRTCII_4BPP,
        RGBA_PVRTCII_2BPP,
        RGB_ATC,
        RGB_A_EXPLICIT_ATC,
        RGB_A_INTERPOLATED_ATC,
        A4_R4_G4_B4_BIGENDIAN_PADDING,
        R5_G6_B5_BIGENDIAN_PADDING,
    }
}
