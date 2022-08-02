﻿namespace PopStudio.Image.Texture
{
    public enum TextureFormat : uint
    {
        NONE,
        B8_G8_R8_A8,
        R8_G8_B8_A8,
        R4_G4_B4_A4,
        A4_R4_G4_B4,
        R5_G5_B5_A1,
        A1_R5_G5_B5,
        R5_G6_B5,
        R4_G4_B4_A4_BLOCK,
        R5_G5_B5_A1_BLOCK,
        R5_G6_B5_BLOCK,
        RGB_PVRTCI_4BPP,
        RGBA_PVRTCI_4BPP,
        RGB_PVRTCI_2BPP,
        RGBA_PVRTCI_2BPP,
        RGBA_PVRTCII_4BPP,
        RGBA_PVRTCII_2BPP,
        RGB_ATC,
        RGB_A_EXPLICIT_ATC,
        RGB_A_INTERPOLATED_ATC,
        RGB_DXT1,
        RGBA_DXT1,
        RGBA_DXT3,
        RGBA_DXT5,
        RGB_ETC1,
        RGB_ETC1_ADD_A8,
        RGBA_PVRTCI_4BPP_ADD_A8,
        B8_G8_R8_A8_ADD_A8,
        R8_G8_B8_A8_ADD_A8,
        RGBA_DXT5_BLOCK_MORTON,
        A8_R8_G8_B8,
        A8_R8_G8_B8_PADDING,
        RGB_ETC1_ADD_A_PALETTE,
        RGBA_DXT5_BIGENDIAN_PADDING,
        RGBA_DXT5_REFLECTEDMORTON,
    }
}