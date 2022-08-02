using System.Runtime.CompilerServices;

namespace PopStudio.Image.Texture.CompressedCoder
{
    /// <summary>
    /// 找了半天没找到格式说明。。。
    /// 我只能猜测他的格式了。。。
    /// 解码器编码器都是我自己写的
    /// 如有错误，还请指正（我感觉是有的，但是我没找到）
    /// </summary>
    internal static unsafe class ATC
	{
        public static void DecodeBlock_RGBA_ATC_Explicit(byte* texPtr, YFColor* color)
        {
            ATCDecoder.DecodeColorWord(texPtr + 8, color);
            ATCDecoder.DecodeExplicitAlphaWord(texPtr, color);
        }

        public static void DecodeBlock_RGBA_ATC_Interpolated(byte* texPtr, YFColor* color)
        {
            ATCDecoder.DecodeColorWord(texPtr + 8, color);
            ATCDecoder.DecodeInterpolatedAlphaWord(texPtr, color);
        }

        public static void DecodeBlock_RGB_ATC(byte* texPtr, YFColor* color)
        {
            ATCDecoder.DecodeColorWord(texPtr, color);
        }

        public static void EncodeBlock_RGBA_ATC_Explicit(byte* texPtr, YFColor* color)
        {
            ATCEncoder.EncodeBlock_RGBA_ATC_Explicit_Block(texPtr, color);
        }

        public static void EncodeBlock_RGBA_ATC_Interpolated(byte* texPtr, YFColor* color)
        {
            ATCEncoder.EncodeBlock_RGBA_ATC_Interpolated_Block(texPtr, color);
        }

        public static void EncodeBlock_RGB_ATC(byte* texPtr, YFColor* color)
        {
            ATCEncoder.EncodeBlock_RGB_ATC_Block(texPtr, color);
        }

        private static class ATCEncoder
        {
            public static void EncodeBlock_RGBA_ATC_Explicit_Block(byte* texPtr, YFColor* color)
            {
                ulong rgbword = Internal_Encode_Explicit_Alpha_Block(color);
                *texPtr++ = (byte)rgbword;
                *texPtr++ = (byte)(rgbword >> 8);
                *texPtr++ = (byte)(rgbword >> 16);
                *texPtr++ = (byte)(rgbword >> 24);
                *texPtr++ = (byte)(rgbword >> 32);
                *texPtr++ = (byte)(rgbword >> 40);
                *texPtr++ = (byte)(rgbword >> 48);
                *texPtr++ = (byte)(rgbword >> 56);
                rgbword = Internal_Encode_RGB_ATITC_Block(color);
                *texPtr++ = (byte)rgbword;
                *texPtr++ = (byte)(rgbword >> 8);
                *texPtr++ = (byte)(rgbword >> 16);
                *texPtr++ = (byte)(rgbword >> 24);
                *texPtr++ = (byte)(rgbword >> 32);
                *texPtr++ = (byte)(rgbword >> 40);
                *texPtr++ = (byte)(rgbword >> 48);
                *texPtr++ = (byte)(rgbword >> 56);
            }

            public static void EncodeBlock_RGBA_ATC_Interpolated_Block(byte* texPtr, YFColor* color)
            {
                ulong rgbword = Internal_Encode_Interpolated_Alpha_Block(color);
                *texPtr++ = (byte)rgbword;
                *texPtr++ = (byte)(rgbword >> 8);
                *texPtr++ = (byte)(rgbword >> 16);
                *texPtr++ = (byte)(rgbword >> 24);
                *texPtr++ = (byte)(rgbword >> 32);
                *texPtr++ = (byte)(rgbword >> 40);
                *texPtr++ = (byte)(rgbword >> 48);
                *texPtr++ = (byte)(rgbword >> 56);
                rgbword = Internal_Encode_RGB_ATITC_Block(color);
                *texPtr++ = (byte)rgbword;
                *texPtr++ = (byte)(rgbword >> 8);
                *texPtr++ = (byte)(rgbword >> 16);
                *texPtr++ = (byte)(rgbword >> 24);
                *texPtr++ = (byte)(rgbword >> 32);
                *texPtr++ = (byte)(rgbword >> 40);
                *texPtr++ = (byte)(rgbword >> 48);
                *texPtr++ = (byte)(rgbword >> 56);
            }

            public static void EncodeBlock_RGB_ATC_Block(byte* texPtr, YFColor* color)
            {
                ulong rgbword = Internal_Encode_RGB_ATITC_Block(color);
                *texPtr++ = (byte)rgbword;
                *texPtr++ = (byte)(rgbword >> 8);
                *texPtr++ = (byte)(rgbword >> 16);
                *texPtr++ = (byte)(rgbword >> 24);
                *texPtr++ = (byte)(rgbword >> 32);
                *texPtr++ = (byte)(rgbword >> 40);
                *texPtr++ = (byte)(rgbword >> 48);
                *texPtr++ = (byte)(rgbword >> 56);
            }

            static ulong Internal_Encode_Explicit_Alpha_Block(YFColor* colorBlock)
            {
                ulong ulong_value = 0;
                for (int i = 15; i >= 0; i--)
                {
                    ulong_value <<= 4;
                    ulong_value |= (uint)(colorBlock[i].Alpha >> 4);
                }
                return ulong_value;
            }

            static ulong Internal_Encode_Interpolated_Alpha_Block(YFColor* colorBlock)
            {
                int maxAlpha = 0;
                int minAlpha = 255;
                for (int i = 0; i < 16; i++)
                {
                    if (colorBlock[i].Alpha < minAlpha) minAlpha = colorBlock[i].Alpha;
                    if (colorBlock[i].Alpha > maxAlpha) maxAlpha = colorBlock[i].Alpha;
                }
                int inset = (maxAlpha - minAlpha) >> 4;
                minAlpha = minAlpha + inset;
                maxAlpha = maxAlpha - inset;
                if (minAlpha == maxAlpha)
                {
                    return (uint)maxAlpha | ((ulong)minAlpha << 8);
                }
                byte* alphas = stackalloc byte[8];
                alphas[0] = (byte)maxAlpha;
                alphas[1] = (byte)minAlpha;
                alphas[2] = (byte)((6 * maxAlpha + minAlpha) / 7);
                alphas[3] = (byte)((5 * maxAlpha + (minAlpha << 1)) / 7);
                alphas[4] = (byte)(((maxAlpha << 2) + 3 * minAlpha) / 7);
                alphas[5] = (byte)((3 * maxAlpha + (minAlpha << 2)) / 7);
                alphas[6] = (byte)(((maxAlpha << 1) + 5 * minAlpha) / 7);
                alphas[7] = (byte)((maxAlpha + 6 * minAlpha) / 7);
                ulong indices = 0;
                byte buffer = 0;
                for (int i = 15; i >= 0; i--)
                {
                    int minDistance = int.MaxValue;
                    byte a = colorBlock[i].Alpha;
                    for (byte j = 0; j < 8; j++)
                    {
                        int dist = a - alphas[j];
                        if (dist < 0) dist = -dist;
                        if (dist < minDistance)
                        {
                            minDistance = dist;
                            buffer = j;
                        }
                    }
                    indices <<= 3;
                    indices |= buffer;
                }
                indices <<= 16;
                indices |= (uint)maxAlpha | ((uint)minAlpha << 8);
                return indices;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static ushort ColorTo565(YFColor color)
            {
                return (ushort)(((color.Red >> 3) << 11) | ((color.Green >> 2) << 5) | (color.Blue >> 3));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static ushort ColorTo555(YFColor color)
            {
                return (ushort)(((color.Red >> 3) << 10) | ((color.Green >> 3) << 5) | (color.Blue >> 3));
            }

            static ulong Internal_Encode_RGB_ATITC_Block(YFColor* color)
            {
                YFColor c1, c0;
                GetMinMaxColorsByBoundingBox(color, &c0, &c1);
                int* redecode_color = stackalloc int[16];
                int score1, score2, score3;
                uint res1, res2, res3;
                ReDecodeColor(c0, c1, false, redecode_color);
                res1 = EmitColorIndices(color, redecode_color, &score1);
                ReDecodeColor(c0, c1, true, redecode_color);
                res2 = EmitColorIndices(color, redecode_color, &score2);
                ReDecodeColor(c1, c0, true, redecode_color);
                res3 = EmitColorIndices(color, redecode_color, &score3);
                int minscore = score1, minindex = 0;
                if (score2 < minscore)
                {
                    minscore = score2;
                    minindex = 1;
                }
                if (score3 < minscore)
                {
                    minindex = 2;
                }
                if (minindex == 0)
                {
                    ulong ans = (ulong)res1 << 32;
                    ans |= (uint)ColorTo565(c1) << 16;
                    ans |= ColorTo555(c0);
                    return ans;
                }
                else if (minindex == 1)
                {
                    ulong ans = (ulong)res2 << 32;
                    ans |= (uint)ColorTo565(c1) << 16;
                    ans |= ColorTo555(c0) | 0x8000U;
                    return ans;
                }
                else
                {
                    ulong ans = (ulong)res3 << 32;
                    ans |= (uint)ColorTo565(c0) << 16;
                    ans |= ColorTo555(c1) | 0x8000U;
                    return ans;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static int abs(int v)
            {
                if (v < 0) return -v;
                return v;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static int Internal_ToFourByte(int a)
            {
                if (a <= 0) return 0;
                if (a >= 1020) return 1020;
                return a;
            }

            static void ReDecodeColor(YFColor c0, YFColor c1, bool Flag, int* colors)
            {
                if (Flag)
                {
                    colors[8] = (c0.Red & 0xF8) | (c0.Red >> 5);
                    colors[9] = (c0.Green & 0xF8) | (c0.Green >> 5);
                    colors[10] = (c0.Blue & 0xF8) | (c0.Blue >> 5);
                    colors[12] = (c1.Red & 0xF8) | (c1.Red >> 5);
                    colors[13] = (c1.Green & 0xFC) | (c1.Green >> 6);
                    colors[14] = (c1.Blue & 0xF8) | (c1.Blue >> 5);
                    colors[0] = 0;
                    colors[1] = 0;
                    colors[2] = 0;
                    colors[4] = (byte)(Internal_ToFourByte((colors[8] << 2) - colors[12]) >> 2);
                    colors[5] = (byte)(Internal_ToFourByte((colors[9] << 2) - colors[13]) >> 2);
                    colors[6] = (byte)(Internal_ToFourByte((colors[10] << 2) - colors[14]) >> 2);
                }
                else
                {
                    colors[0] = (c0.Red & 0xF8) | (c0.Red >> 5);
                    colors[1] = (c0.Green & 0xF8) | (c0.Green >> 5);
                    colors[2] = (c0.Blue & 0xF8) | (c0.Blue >> 5);
                    colors[12] = (c1.Red & 0xF8) | (c1.Red >> 5);
                    colors[13] = (c1.Green & 0xFC) | (c1.Green >> 6);
                    colors[14] = (c1.Blue & 0xF8) | (c1.Blue >> 5);
                    colors[4] = (colors[0] * 5 + colors[12] * 3) >> 3;
                    colors[5] = (colors[1] * 5 + colors[13] * 3) >> 3;
                    colors[6] = (colors[2] * 5 + colors[14] * 3) >> 3;
                    colors[8] = (colors[0] * 3 + colors[12] * 5) >> 3;
                    colors[9] = (colors[1] * 3 + colors[13] * 5) >> 3;
                    colors[10] = (colors[2] * 3 + colors[14] * 5) >> 3;
                }
            }

            static uint EmitColorIndices(YFColor* colorBlock, int* colors, int* score)
            {
                *score = 0;
                uint result = 0;
                for (int i = 0; i< 16; i++)
                {
                    int c0 = colorBlock[i].Red;
                    int c1 = colorBlock[i].Green;
                    int c2 = colorBlock[i].Blue;
                    int d0 = abs(colors[0] - c0) + abs(colors[1] - c1) + abs(colors[2] - c2);
                    int d1 = abs(colors[4] - c0) + abs(colors[5] - c1) + abs(colors[6] - c2);
                    int d2 = abs(colors[8] - c0) + abs(colors[9] - c1) + abs(colors[10] - c2);
                    int d3 = abs(colors[12] - c0) + abs(colors[13] - c1) + abs(colors[14] - c2);
                    uint b0 = d0 > d3 ? 1u : 0u;
                    uint b1 = d1 > d2 ? 1u : 0u;
                    uint b2 = d0 > d2 ? 1u : 0u;
                    uint b3 = d1 > d3 ? 1u : 0u;
                    uint b4 = d2 > d3 ? 1u : 0u;
                    uint x0 = b1 & b2;
                    uint x1 = b0 & b3;
                    uint x2 = b0 & b4;
                    uint index = x2 | ((x0 | x1) << 1);
                    *score += index switch
                    {
                        0 => d0,
                        1 => d1,
                        2 => d2,
                        _ => d3
                    };
                    result |= index << (i << 1);
                }
                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static byte ToByte(int v)
            {
                if (v >= 255) return 255;
                if (v <= 0) return 0;
                return (byte)v;
            }

            static void GetMinMaxColorsByBoundingBox(YFColor* colorBlock, YFColor* minColor, YFColor* maxColor)
            {
                YFColor inset;
                minColor->Red = 255;
                minColor->Green = 255;
                minColor->Blue = 255;
                maxColor->Red = 0;
                maxColor->Green = 0;
                maxColor->Blue = 0;
                for (int i = 0; i < 16; i++)
                {
                    if (colorBlock->Red < minColor->Red) minColor->Red = colorBlock->Red;
                    if (colorBlock->Green < minColor->Green) minColor->Green = colorBlock->Green;
                    if (colorBlock->Blue < minColor->Blue) minColor->Blue = colorBlock->Blue;
                    if (colorBlock->Red > maxColor->Red) maxColor->Red = colorBlock->Red;
                    if (colorBlock->Green > maxColor->Green) maxColor->Green = colorBlock->Green;
                    if (colorBlock->Blue > maxColor->Blue) maxColor->Blue = colorBlock->Blue;
                    colorBlock++;
                }
                inset.Red = (byte)((maxColor->Red - minColor->Red) >> 4);
                inset.Green = (byte)((maxColor->Green - minColor->Green) >> 4);
                inset.Blue = (byte)((maxColor->Blue - minColor->Blue) >> 4);
                minColor->Red = ToByte(minColor->Red + inset.Red);
                minColor->Green = ToByte(minColor->Green + inset.Green);
                minColor->Blue = ToByte(minColor->Blue + inset.Blue);
                maxColor->Red = ToByte(maxColor->Red - inset.Red);
                maxColor->Green = ToByte(maxColor->Green - inset.Green);
                maxColor->Blue = ToByte(maxColor->Blue - inset.Blue);
            }
        }

        private static class ATCDecoder
        {
            /// <summary>
            /// 将8字节Alpha4Word解码到4*4颜色块，不覆盖Red/Green/Blue，只覆盖Alpha
            /// </summary>
            /// <param name="texPtr">被解码的纹理数据，端序不匹配需要预先调整</param>
            /// <param name="color">解码出的颜色指针，要求传入有至少16个YFColor的内存空间</param>
            public static void DecodeExplicitAlphaWord(byte* texPtr, YFColor* color)
            {
                for (int i = 0; i < 8; i++)
                {
                    int a = *texPtr++ & 0xF;
                    color++->Alpha = (byte)((a << 4) | a);
                    a = *texPtr++ >> 4;
                    color++->Alpha = (byte)((a << 4) | a);
                }
            }

            /// <summary>
            /// 将8字节Alpha8Word解码到4*4颜色块，不覆盖Red/Green/Blue，只覆盖Alpha
            /// </summary>
            /// <param name="texPtr">被解码的纹理数据，端序不匹配需要预先调整</param>
            /// <param name="color">解码出的颜色指针，要求传入有至少16个YFColor的内存空间</param>
            public static void DecodeInterpolatedAlphaWord(byte* texPtr, YFColor* color)
            {
                byte* alpha_buffer = stackalloc byte[8];
                alpha_buffer[0] = *texPtr++;  // 转byte自动&0xFF
                alpha_buffer[1] = *texPtr++;
                texPtr++;
                // 判断颜色1是否大于颜色2
                if (alpha_buffer[0] > alpha_buffer[1])
                {
                    alpha_buffer[2] = (byte)((6 * alpha_buffer[0] + alpha_buffer[1]) / 7);
                    alpha_buffer[3] = (byte)((5 * alpha_buffer[0] + (alpha_buffer[1] << 1)) / 7);
                    alpha_buffer[4] = (byte)(((alpha_buffer[0] << 2) + 3 * alpha_buffer[1]) / 7);
                    alpha_buffer[5] = (byte)((3 * alpha_buffer[0] + (alpha_buffer[1] << 2)) / 7);
                    alpha_buffer[6] = (byte)(((alpha_buffer[0] << 1) + 5 * alpha_buffer[1]) / 7);
                    alpha_buffer[7] = (byte)((alpha_buffer[0] + 6 * alpha_buffer[1]) / 7);
                }
                else
                {
                    alpha_buffer[2] = (byte)(((alpha_buffer[0] << 2) + alpha_buffer[1]) / 5);
                    alpha_buffer[3] = (byte)((3 * alpha_buffer[0] + (alpha_buffer[1] << 1)) / 5);
                    alpha_buffer[4] = (byte)(((alpha_buffer[0] << 1) + 3 * alpha_buffer[1]) / 5);
                    alpha_buffer[5] = (byte)((alpha_buffer[0] + (alpha_buffer[1] << 2)) / 5);
                    alpha_buffer[6] = 0x0;
                    alpha_buffer[7] = 0xFF;
                }
                // 读取颜色指针
                ulong alpha_flags = 0;
                alpha_flags |= *texPtr++;
                alpha_flags |= (ulong)*texPtr++ << 8;
                alpha_flags |= (ulong)*texPtr++ << 16;
                alpha_flags |= (ulong)*texPtr++ << 24;
                alpha_flags |= (ulong)*texPtr++ << 32;
                alpha_flags |= (ulong)*texPtr++ << 40;
                // 根据索引赋值
                for (int i = 0; i < 16; i++)
                {
                    color++->Alpha = alpha_buffer[alpha_flags & 0x7];
                    alpha_flags >>= 3;
                }
            }

            private static int abs(int v)
            {
                return v < 0 ? -v : v;
            }

            public static void DecodeColorWord(byte* texPtr, YFColor* color)
            {
                YFColor* color_buffer = stackalloc YFColor[4];
                ushort color0 = 0;
                color0 |= *texPtr++;
                color0 |= (ushort)(*texPtr++ << 8);
                ushort color1 = 0;
                color1 |= *texPtr++;
                color1 |= (ushort)(*texPtr++ << 8);
                bool mode = (color0 & 0x8000) != 0;
                if (mode)
                {
                    int r = (color0 >> 10) & 0x1F;
                    int g = (color0 >> 5) & 0x1F;
                    int b = color0 & 0x1F;
                    color_buffer[2].Red = (byte)((r << 3) | (r >> 2));
                    color_buffer[2].Green = (byte)((g << 3) | (g >> 2));
                    color_buffer[2].Blue = (byte)((b << 3) | (b >> 2));
                    color_buffer[2].Alpha = 0xFF;

                    r = color1 >> 11;
                    g = (color1 >> 5) & 0x3F;
                    b = color1 & 0x1F;
                    color_buffer[3].Red = (byte)((r << 3) | (r >> 2));
                    color_buffer[3].Green = (byte)((g << 2) | (g >> 4));
                    color_buffer[3].Blue = (byte)((b << 3) | (b >> 2));
                    color_buffer[3].Alpha = 0xFF;

                    color_buffer[0].Red = 0x0;
                    color_buffer[0].Green = 0x0;
                    color_buffer[0].Blue = 0x0;
                    color_buffer[0].Alpha = 0xFF;

                    color_buffer[1].Red = (byte)(abs((color_buffer[2].Red << 2) - color_buffer[3].Red) >> 2);
                    color_buffer[1].Green = (byte)(abs((color_buffer[2].Green << 2) - color_buffer[3].Green) >> 2);
                    color_buffer[1].Blue = (byte)(abs((color_buffer[2].Blue << 2) - color_buffer[3].Blue) >> 2);
                    color_buffer[1].Alpha = 0xFF;
                }
                else
                {
                    int r = (color0 >> 10) & 0x1F;
                    int g = (color0 >> 5) & 0x1F;
                    int b = color0 & 0x1F;
                    color_buffer[0].Red = (byte)((r << 3) | (r >> 2));
                    color_buffer[0].Green = (byte)((g << 3) | (g >> 2));
                    color_buffer[0].Blue = (byte)((b << 3) | (b >> 2));
                    color_buffer[0].Alpha = 0xFF;

                    r = color1 >> 11;
                    g = (color1 >> 5) & 0x3F;
                    b = color1 & 0x1F;
                    color_buffer[3].Red = (byte)((r << 3) | (r >> 2));
                    color_buffer[3].Green = (byte)((g << 2) | (g >> 4));
                    color_buffer[3].Blue = (byte)((b << 3) | (b >> 2));
                    color_buffer[3].Alpha = 0xFF;

                    color_buffer[1].Red = (byte)((color_buffer[0].Red * 5 + color_buffer[3].Red * 3 + 4) >> 3);
                    color_buffer[1].Green = (byte)((color_buffer[0].Green * 5 + color_buffer[3].Green * 3 + 4) >> 3);
                    color_buffer[1].Blue = (byte)((color_buffer[0].Blue * 5 + color_buffer[3].Blue * 3 + 4) >> 3);
                    color_buffer[1].Alpha = 0xFF;

                    color_buffer[2].Red = (byte)((color_buffer[0].Red * 3 + color_buffer[3].Red * 5 + 4) >> 3);
                    color_buffer[2].Green = (byte)((color_buffer[0].Green * 3 + color_buffer[3].Green * 5 + 4) >> 3);
                    color_buffer[2].Blue = (byte)((color_buffer[0].Blue * 3 + color_buffer[3].Blue * 5 + 4) >> 3);
                    color_buffer[2].Alpha = 0xFF;
                }
                
                uint colorFlags = 0;
                colorFlags |= *texPtr++;
                colorFlags |= (uint)(*texPtr++ << 8);
                colorFlags |= (uint)(*texPtr++ << 16);
                colorFlags |= (uint)(*texPtr++ << 24);
                for (int i = 0; i < 16; i++)
                {
                    *color++ = color_buffer[colorFlags & 0b11];
                    colorFlags >>= 2;
                }
            }
		}
	}
}
