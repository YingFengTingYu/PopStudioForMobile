namespace PopStudio.Image.Texture.CompressedCoder
{
    /// <summary>
    /// DXT1-5/BC1-3解码编码器
    /// </summary>
    internal static unsafe class S3TC
    {
        /// <summary>
        /// 将16字节DXT颜色Word解码为RGBA_DXT5纹理4*4块
        /// </summary>
        /// <param name="texPtr">被解码的纹理数据，端序不匹配需要预先调整</param>
        /// <param name="color">解码出的颜色指针，要求传入有至少16个YFColor的内存空间</param>
        public static void EncodeBlock_RGBA_DXT5(byte* texPtr, YFColor* color)
        {
            DXTEncoder.EncodeColorWord(texPtr + 8, color, false, false);
            DXTEncoder.EncodeInterpolatedAlphaWord(texPtr, color, false);
        }

        public static void EncodeBlock_RGBA_DXT5_BigEndian(byte* texPtr, YFColor* color)
        {
            DXTEncoder.EncodeColorWord(texPtr + 8, color, false, true);
            DXTEncoder.EncodeInterpolatedAlphaWord(texPtr, color, true);
        }

        /// <summary>
        /// 将16字节DXT颜色Word解码为RGBA_DXT3纹理4*4块
        /// </summary>
        /// <param name="texPtr">被解码的纹理数据，端序不匹配需要预先调整</param>
        /// <param name="color">解码出的颜色指针，要求传入有至少16个YFColor的内存空间</param>
        public static void EncodeBlock_RGBA_DXT3(byte* texPtr, YFColor* color)
        {
            DXTEncoder.EncodeColorWord(texPtr + 8, color, false, false);
            DXTEncoder.EncodeExplicitAlphaWord(texPtr, color, false);
        }

        public static void EncodeBlock_RGBA_DXT3_BigEndian(byte* texPtr, YFColor* color)
        {
            DXTEncoder.EncodeColorWord(texPtr + 8, color, false, true);
            DXTEncoder.EncodeExplicitAlphaWord(texPtr, color, true);
        }

        /// <summary>
        /// 将8字节DXT颜色Word解码为RGBA_DXT1纹理4*4块
        /// </summary>
        /// <param name="texPtr">被解码的纹理数据，端序不匹配需要预先调整</param>
        /// <param name="color">解码出的颜色指针，要求传入有至少16个YFColor的内存空间</param>
        public static void EncodeBlock_RGBA_DXT1(byte* texPtr, YFColor* color)
        {
            DXTEncoder.EncodeColorWord(texPtr, color, true, false);
        }

        public static void EncodeBlock_RGBA_DXT1_BigEndian(byte* texPtr, YFColor* color)
        {
            DXTEncoder.EncodeColorWord(texPtr, color, true, true);
        }

        /// <summary>
        /// 将8字节DXT颜色Word解码为RGB_DXT1纹理4*4块
        /// </summary>
        /// <param name="texPtr">被解码的纹理数据，端序不匹配需要预先调整</param>
        /// <param name="color">解码出的颜色指针，要求传入有至少16个YFColor的内存空间</param>
        public static void EncodeBlock_RGB_DXT1(byte* texPtr, YFColor* color)
        {
            DXTEncoder.EncodeColorWord(texPtr, color, false, false);
        }

        public static void EncodeBlock_RGB_DXT1_BigEndian(byte* texPtr, YFColor* color)
        {
            DXTEncoder.EncodeColorWord(texPtr, color, false, true);
        }

        /// <summary>
        /// 将16字节DXT颜色Word解码为RGBA_DXT5纹理4*4块
        /// </summary>
        /// <param name="texPtr">被解码的纹理数据，端序不匹配需要预先调整</param>
        /// <param name="color">解码出的颜色指针，要求传入有至少16个YFColor的内存空间</param>
        public static void DecodeBlock_RGBA_DXT5(byte* texPtr, YFColor* color)
        {
            DXTDecoder.DecodeColorWord(texPtr + 8, color, false, false);
            DXTDecoder.DecodeInterpolatedAlphaWord(texPtr, color, false);
        }

        public static void DecodeBlock_RGBA_DXT5_BigEndian(byte* texPtr, YFColor* color)
        {
            DXTDecoder.DecodeColorWord(texPtr + 8, color, false, true);
            DXTDecoder.DecodeInterpolatedAlphaWord(texPtr, color, true);
        }

        /// <summary>
        /// 将16字节DXT颜色Word解码为RGBA_DXT3纹理4*4块
        /// </summary>
        /// <param name="texPtr">被解码的纹理数据，端序不匹配需要预先调整</param>
        /// <param name="color">解码出的颜色指针，要求传入有至少16个YFColor的内存空间</param>
        public static void DecodeBlock_RGBA_DXT3(byte* texPtr, YFColor* color)
        {
            DXTDecoder.DecodeColorWord(texPtr + 8, color, false, false);
            DXTDecoder.DecodeExplicitAlphaWord(texPtr, color, false);
        }

        public static void DecodeBlock_RGBA_DXT3_BigEndian(byte* texPtr, YFColor* color)
        {
            DXTDecoder.DecodeColorWord(texPtr + 8, color, false, true);
            DXTDecoder.DecodeExplicitAlphaWord(texPtr, color, true);
        }

        /// <summary>
        /// 将8字节DXT颜色Word解码为RGBA_DXT1纹理4*4块
        /// </summary>
        /// <param name="texPtr">被解码的纹理数据，端序不匹配需要预先调整</param>
        /// <param name="color">解码出的颜色指针，要求传入有至少16个YFColor的内存空间</param>
        public static void DecodeBlock_RGBA_DXT1(byte* texPtr, YFColor* color)
        {
            DXTDecoder.DecodeColorWord(texPtr, color, true, false);
        }

        public static void DecodeBlock_RGBA_DXT1_BigEndian(byte* texPtr, YFColor* color)
        {
            DXTDecoder.DecodeColorWord(texPtr, color, true, true);
        }

        /// <summary>
        /// 将8字节DXT颜色Word解码为RGB_DXT1纹理4*4块
        /// </summary>
        /// <param name="texPtr">被解码的纹理数据，端序不匹配需要预先调整</param>
        /// <param name="color">解码出的颜色指针，要求传入有至少16个YFColor的内存空间</param>
        public static void DecodeBlock_RGB_DXT1(byte* texPtr, YFColor* color)
        {
            DXTDecoder.DecodeColorWord(texPtr, color, false, false);
        }

        public static void DecodeBlock_RGB_DXT1_BigEndian(byte* texPtr, YFColor* color)
        {
            DXTDecoder.DecodeColorWord(texPtr, color, false, true);
        }

        /// <summary>
        /// 用于快速编码DXT的类
        /// </summary>
        private static class DXTEncoder
        {
            /// <summary>
            /// 编码DXT4/5中线性插值的Alpha通道
            /// </summary>
            /// <param name="texPtr"></param>
            /// <param name="color"></param>
            public static void EncodeInterpolatedAlphaWord(byte* texPtr, YFColor* color, bool bigendian)
            {
                int maxAlpha = 0x0;
                int minAlpha = 0xFF;
                for (int i = 0; i < 16; i++)
                {
                    if (color[i].Alpha < minAlpha) minAlpha = color[i].Alpha;
                    if (color[i].Alpha > maxAlpha) maxAlpha = color[i].Alpha;
                }
                int inset = (maxAlpha - minAlpha) >> 4;
                minAlpha += inset;
                maxAlpha -= inset;
                if (minAlpha == maxAlpha)
                {
                    if (bigendian)
                    {
                        *texPtr++ = (byte)minAlpha;
                        *texPtr++ = (byte)maxAlpha;
                    }
                    else
                    {
                        *texPtr++ = (byte)maxAlpha;
                        *texPtr++ = (byte)minAlpha;
                    }
                    *texPtr++ = 0;
                    *texPtr++ = 0;
                    *texPtr++ = 0;
                    *texPtr++ = 0;
                    *texPtr++ = 0;
                    *texPtr++ = 0;
                }
                else
                {
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
                        byte a = color[i].Alpha;
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
                    for (int i = 0; i < 4; i++)
                    {
                        if (bigendian)
                        {
                            *texPtr++ = (byte)(indices >> 8);
                            *texPtr++ = (byte)indices;
                        }
                        else
                        {
                            *texPtr++ = (byte)indices;
                            *texPtr++ = (byte)(indices >> 8);
                        }
                        indices >>= 16;
                    }
                }
            }

            /// <summary>
            /// 给DXT2/4用的预乘Alpha通道
            /// </summary>
            /// <param name="inColor"></param>
            public static void PremultiplyAlpha(YFColor* inColor, YFColor* outColor)
            {
                for (int i = 0; i < 16; i++)
                {
                    outColor->Red = (byte)(inColor->Red * inColor->Alpha / 0xFF);
                    outColor->Green = (byte)(inColor->Green * inColor->Alpha / 0xFF);
                    outColor->Blue = (byte)(inColor->Blue * inColor->Alpha / 0xFF);
                    outColor->Alpha = inColor->Alpha;
                    inColor++;
                    outColor++;
                }
            }

            /// <summary>
            /// 给DXT2/3用的编码A4
            /// </summary>
            /// <param name="texPtr"></param>
            /// <param name="color"></param>
            public static void EncodeExplicitAlphaWord(byte* texPtr, YFColor* color, bool bigendian)
            {
                for (int i = 0; i < 4; i++)
                {
                    byte buf1 = 0;
                    buf1 |= (byte)(color++->Alpha >> 4);
                    buf1 |= (byte)(color++->Alpha & 0xF0);
                    byte buf2 = 0;
                    buf2 |= (byte)(color++->Alpha >> 4);
                    buf2 |= (byte)(color++->Alpha & 0xF0);
                    if (bigendian)
                    {
                        *texPtr++ = buf2;
                        *texPtr++ = buf1;
                    }
                    else
                    {
                        *texPtr++ = buf1;
                        *texPtr++ = buf2;
                    }
                }
            }

            public static void EncodeColorWord(byte* texPtr, YFColor* color, bool alpha, bool bigendian)
            {
                // 判断要不要用alpha编码
                if (alpha)
                {
                    alpha = false;
                    for (int i = 0; i < 16; i++)
                    {
                        if ((color[i].Alpha & 0x80) == 0)
                        {
                            alpha = true;
                            break;
                        }
                    }
                }
                // 求出最大最小颜色
                YFColor minColor, maxColor;
                YFColor inset;
                minColor.Red = 255;
                minColor.Green = 255;
                minColor.Blue = 255;
                minColor.Alpha = 255;
                maxColor.Red = 0;
                maxColor.Green = 0;
                maxColor.Blue = 0;
                maxColor.Alpha = 255;
                for (int i = 0; i < 16; i++)
                {
                    if (color[i].Red < minColor.Red) minColor.Red = color[i].Red;
                    if (color[i].Green < minColor.Green) minColor.Green = color[i].Green;
                    if (color[i].Blue < minColor.Blue) minColor.Blue = color[i].Blue;
                    if (color[i].Red > maxColor.Red) maxColor.Red = color[i].Red;
                    if (color[i].Green > maxColor.Green) maxColor.Green = color[i].Green;
                    if (color[i].Blue > maxColor.Blue) maxColor.Blue = color[i].Blue;
                }
                inset.Red = (byte)((maxColor.Red - minColor.Red) >> 4);
                inset.Green = (byte)((maxColor.Green - minColor.Green) >> 4);
                inset.Blue = (byte)((maxColor.Blue - minColor.Blue) >> 4);
                minColor.Red += inset.Red;
                minColor.Green += inset.Green;
                minColor.Blue += inset.Blue;
                maxColor.Red -= inset.Red;
                maxColor.Green -= inset.Green;
                maxColor.Blue -= inset.Blue;
                // 降频到RGB565
                minColor.Red = (byte)((minColor.Red & 0xF8) | (minColor.Red >> 5));
                minColor.Green = (byte)((minColor.Green & 0xFC) | (minColor.Green >> 6));
                minColor.Blue = (byte)((minColor.Blue & 0xF8) | (minColor.Blue >> 5));
                maxColor.Red = (byte)((maxColor.Red & 0xF8) | (maxColor.Red >> 5));
                maxColor.Green = (byte)((maxColor.Green & 0xFC) | (maxColor.Green >> 6));
                maxColor.Blue = (byte)((maxColor.Blue & 0xF8) | (maxColor.Blue >> 5));
                // 计算颜色适合度
                uint color_flags = 0;
                ushort c0, c1;
                if (alpha)
                {
                    c0 = (ushort)((minColor.Red >> 3 << 11) | (minColor.Green >> 2 << 5) | (minColor.Blue >> 3));
                    c1 = (ushort)((maxColor.Red >> 3 << 11) | (maxColor.Green >> 2 << 5) | (maxColor.Blue >> 3));
                    YFColor* color_buffer = stackalloc YFColor[3];
                    color_buffer[0].Red = minColor.Red;
                    color_buffer[0].Green = minColor.Green;
                    color_buffer[0].Blue = minColor.Blue;
                    color_buffer[0].Alpha = 0xFF;
                    color_buffer[1].Red = maxColor.Red;
                    color_buffer[1].Green = maxColor.Green;
                    color_buffer[1].Blue = maxColor.Blue;
                    color_buffer[1].Alpha = 0xFF;
                    color_buffer[2].Red = (byte)((minColor.Red + maxColor.Red) >> 1);
                    color_buffer[2].Green = (byte)((minColor.Green + maxColor.Green) >> 1);
                    color_buffer[2].Blue = (byte)((minColor.Blue + maxColor.Blue) >> 1);
                    color_buffer[2].Alpha = 0xFF;
                    for (int i = 15; i >= 0; i--)
                    {
                        uint index = 0;
                        if ((color[i].Alpha & 0x80) == 0)
                        {
                            index = 3;
                        }
                        else
                        {
                            int minDiff = int.MaxValue;
                            for (uint j = 0; j < 3; j++)
                            {
                                int delta_red = color_buffer[j].Red - color[i].Red;
                                if (delta_red < 0)
                                {
                                    delta_red = -delta_red;
                                }
                                int delta_green = color_buffer[j].Green - color[i].Green;
                                if (delta_green < 0)
                                {
                                    delta_green = -delta_green;
                                }
                                int delta_blue = color_buffer[j].Blue - color[i].Blue;
                                if (delta_blue < 0)
                                {
                                    delta_blue = -delta_blue;
                                }
                                int diff = delta_red + delta_green + delta_blue;
                                if (diff < minDiff)
                                {
                                    minDiff = diff;
                                    index = j;
                                }
                            }
                        }
                        color_flags <<= 2;
                        color_flags |= index;
                    }
                }
                else
                {
                    c0 = (ushort)((maxColor.Red >> 3 << 11) | (maxColor.Green >> 2 << 5) | (maxColor.Blue >> 3));
                    c1 = (ushort)((minColor.Red >> 3 << 11) | (minColor.Green >> 2 << 5) | (minColor.Blue >> 3));
                    YFColor* color_buffer = stackalloc YFColor[4];
                    color_buffer[0].Red = maxColor.Red;
                    color_buffer[0].Green = maxColor.Green;
                    color_buffer[0].Blue = maxColor.Blue;
                    color_buffer[0].Alpha = 0xFF;
                    color_buffer[1].Red = minColor.Red;
                    color_buffer[1].Green = minColor.Green;
                    color_buffer[1].Blue = minColor.Blue;
                    color_buffer[1].Alpha = 0xFF;
                    color_buffer[2].Red = (byte)(((maxColor.Red << 1) + minColor.Red) / 3);
                    color_buffer[2].Green = (byte)(((maxColor.Green << 1) + minColor.Green) / 3);
                    color_buffer[2].Blue = (byte)(((maxColor.Blue << 1) + minColor.Blue) / 3);
                    color_buffer[2].Alpha = 0xFF;
                    color_buffer[3].Red = (byte)((maxColor.Red + (minColor.Red << 1)) / 3);
                    color_buffer[3].Green = (byte)((maxColor.Green + (minColor.Green << 1)) / 3);
                    color_buffer[3].Blue = (byte)((maxColor.Blue + (minColor.Blue << 1)) / 3);
                    color_buffer[3].Alpha = 0xFF;
                    for (int i = 15; i >= 0; i--)
                    {
                        uint index = 0;
                        int minDiff = int.MaxValue;
                        for (uint j = 0; j < 4; j++)
                        {
                            int delta_red = color_buffer[j].Red - color[i].Red;
                            if (delta_red < 0)
                            {
                                delta_red = -delta_red;
                            }
                            int delta_green = color_buffer[j].Green - color[i].Green;
                            if (delta_green < 0)
                            {
                                delta_green = -delta_green;
                            }
                            int delta_blue = color_buffer[j].Blue - color[i].Blue;
                            if (delta_blue < 0)
                            {
                                delta_blue = -delta_blue;
                            }
                            int diff = delta_red + delta_green + delta_blue;
                            if (diff < minDiff)
                            {
                                minDiff = diff;
                                index = j;
                            }
                        }
                        color_flags <<= 2;
                        color_flags |= index;
                    }
                }
                if (bigendian)
                {
                    *texPtr++ = (byte)(c0 >> 8);
                    *texPtr++ = (byte)c0;
                    *texPtr++ = (byte)(c1 >> 8);
                    *texPtr++ = (byte)c1;
                    *texPtr++ = (byte)(color_flags >> 8);
                    *texPtr++ = (byte)color_flags;
                    *texPtr++ = (byte)(color_flags >> 24);
                    *texPtr++ = (byte)(color_flags >> 16);
                }
                else
                {
                    *texPtr++ = (byte)c0;
                    *texPtr++ = (byte)(c0 >> 8);
                    *texPtr++ = (byte)c1;
                    *texPtr++ = (byte)(c1 >> 8);
                    *texPtr++ = (byte)color_flags;
                    *texPtr++ = (byte)(color_flags >> 8);
                    *texPtr++ = (byte)(color_flags >> 16);
                    *texPtr++ = (byte)(color_flags >> 24);
                }
            }
        }

        /// <summary>
        /// 用于解码DXT纹理的类
        /// </summary>
        private static class DXTDecoder
        {
            /// <summary>
            /// 将8字节Alpha8Word解码到4*4颜色块，不覆盖Red/Green/Blue，只覆盖Alpha
            /// </summary>
            /// <param name="texPtr">被解码的纹理数据，端序不匹配需要预先调整</param>
            /// <param name="color">解码出的颜色指针，要求传入有至少16个YFColor的内存空间</param>
            public static void DecodeInterpolatedAlphaWord(byte* texPtr, YFColor* color, bool bigendian)
            {
                byte* alpha_buffer = stackalloc byte[8];
                ulong alpha_flags = 0;
                if (bigendian)
                {
                    alpha_buffer[1] = *texPtr++;  // 转byte自动&0xFF
                    alpha_buffer[0] = *texPtr++;
                    alpha_flags |= (ulong)*texPtr++ << 8;
                    alpha_flags |= *texPtr++;
                    alpha_flags |= (ulong)*texPtr++ << 24;
                    alpha_flags |= (ulong)*texPtr++ << 16;
                    alpha_flags |= (ulong)*texPtr++ << 40;
                    alpha_flags |= (ulong)*texPtr++ << 32;
                }
                else
                {
                    alpha_buffer[0] = *texPtr++;  // 转byte自动&0xFF
                    alpha_buffer[1] = *texPtr++;
                    alpha_flags |= *texPtr++;
                    alpha_flags |= (ulong)*texPtr++ << 8;
                    alpha_flags |= (ulong)*texPtr++ << 16;
                    alpha_flags |= (ulong)*texPtr++ << 24;
                    alpha_flags |= (ulong)*texPtr++ << 32;
                    alpha_flags |= (ulong)*texPtr++ << 40;
                }
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
                // 根据索引赋值
                for (int i = 0; i < 16; i++)
                {
                    color++->Alpha = alpha_buffer[alpha_flags & 0x7];
                    alpha_flags >>= 3;
                }
            }

            /// <summary>
            /// 恢复DXT2/DXT4预乘的Alpha通道
            /// </summary>
            /// <param name="color">被恢复的颜色指针，要求传入有至少16个YFColor的内存空间</param>
            public static void RecoverPremultipliedAlpha(YFColor* color)
            {
                for (int i = 0; i < 16; i++)
                {
                    int alpha = color->Alpha;
                    if (alpha != 0)
                    {
                        int channel = color->Red * 0xFF / alpha;
                        if (channel > 255) channel = 255;
                        color->Red = (byte)channel;
                        channel = color->Green * 0xFF / alpha;
                        if (channel > 255) channel = 255;
                        color->Green = (byte)channel;
                        channel = color->Blue * 0xFF / alpha;
                        if (channel > 255) channel = 255;
                        color->Blue = (byte)channel;
                    }
                    color++;
                }
            }

            /// <summary>
            /// 将8字节Alpha4Word解码到4*4颜色块，不覆盖Red/Green/Blue，只覆盖Alpha
            /// </summary>
            /// <param name="texPtr">被解码的纹理数据，端序不匹配需要预先调整</param>
            /// <param name="color">解码出的颜色指针，要求传入有至少16个YFColor的内存空间</param>
            public static void DecodeExplicitAlphaWord(byte* texPtr, YFColor* color, bool bigendian)
            {
                for (int i = 0; i < 4; i++)
                {
                    ushort buf = 0;
                    if (bigendian)
                    {
                        buf |= (ushort)(*texPtr++ << 8);
                        buf |= *texPtr++;
                    }
                    else
                    {
                        buf |= *texPtr++;
                        buf |= (ushort)(*texPtr++ << 8);
                    }
                    int a = buf & 0xF;
                    color++->Alpha = (byte)((a << 4) | a);
                    a = (buf & 0xF0) >> 4;
                    color++->Alpha = (byte)((a << 4) | a);
                    a = (buf & 0xF00) >> 8;
                    color++->Alpha = (byte)((a << 4) | a);
                    a = (buf & 0xF000) >> 12;
                    color++->Alpha = (byte)((a << 4) | a);
                }
            }

            /// <summary>
            /// 将8字节DXT颜色Word解码为4*4颜色块，传入指针为ushort类型，是为了方便端序处理
            /// </summary>
            /// <param name="texPtr">被解码的纹理数据，端序不匹配需要预先调整</param>
            /// <param name="color">解码出的颜色指针，要求传入有至少16个YFColor的内存空间</param>
            /// <param name="alpha">是否解码Alpha通道，RGBA_DXT1选择true，其他格式选择false</param>
            public static void DecodeColorWord(byte* texPtr, YFColor* color, bool alpha, bool bigendian)
            {
                YFColor* color_buffer = stackalloc YFColor[4];
                ushort c0 = 0;
                ushort c1 = 0;
                // 读取颜色指针
                uint color_flags = 0;
                if (bigendian)
                {
                    c0 |= (ushort)(*texPtr++ << 8);
                    c0 |= *texPtr++;
                    c1 |= (ushort)(*texPtr++ << 8);
                    c1 |= *texPtr++;
                    color_flags |= (uint)*texPtr++ << 8;
                    color_flags |= *texPtr++;
                    color_flags |= (uint)*texPtr++ << 24;
                    color_flags |= (uint)*texPtr++ << 16;
                }
                else
                {
                    c0 |= *texPtr++;
                    c0 |= (ushort)(*texPtr++ << 8);
                    c1 |= *texPtr++;
                    c1 |= (ushort)(*texPtr++ << 8);
                    color_flags |= *texPtr++;
                    color_flags |= (uint)*texPtr++ << 8;
                    color_flags |= (uint)*texPtr++ << 16;
                    color_flags |= (uint)*texPtr++ << 24;
                }
                // 先取出颜色，把RGB565变成RGB888
                // 解码出来c0的rgb565
                int r = c0 >> 11;
                int g = (c0 >> 5) & 0x3F;
                int b = c0 & 0x1F;
                // color0 = c0
                color_buffer[0].Red = (byte)((r << 3) | (r >> 2));
                color_buffer[0].Green = (byte)((g << 2) | (g >> 4));
                color_buffer[0].Blue = (byte)((b << 3) | (b >> 2));
                color_buffer[0].Alpha = 0xFF;
                // 解码出来c1的rgb565
                r = c1 >> 11;
                g = (c1 >> 5) & 0x3F;
                b = c1 & 0x1F;
                // color1 = c1
                color_buffer[1].Red = (byte)((r << 3) | (r >> 2));
                color_buffer[1].Green = (byte)((g << 2) | (g >> 4));
                color_buffer[1].Blue = (byte)((b << 3) | (b >> 2));
                color_buffer[1].Alpha = 0xFF;
                if (c0 > c1)
                {
                    // color2 = 2 / 3 * c0 + 1 / 3 * c1
                    color_buffer[2].Red = (byte)(((color_buffer[0].Red << 1) + color_buffer[1].Red + 1) / 3);
                    color_buffer[2].Green = (byte)(((color_buffer[0].Green << 1) + color_buffer[1].Green + 1) / 3);
                    color_buffer[2].Blue = (byte)(((color_buffer[0].Blue << 1) + color_buffer[1].Blue + 1) / 3);
                    color_buffer[2].Alpha = 0xFF;
                    // color3 = 1 / 3 * c0 + 2 / 3 * c1
                    color_buffer[3].Red = (byte)((color_buffer[0].Red + (color_buffer[1].Red << 1) + 1) / 3);
                    color_buffer[3].Green = (byte)((color_buffer[0].Green + (color_buffer[1].Green << 1) + 1) / 3);
                    color_buffer[3].Blue = (byte)((color_buffer[0].Blue + (color_buffer[1].Blue << 1) + 1) / 3);
                    color_buffer[3].Alpha = 0xFF;
                }
                else
                {
                    // color2 = 1 / 2 * c0 + 1 / 2 * c1
                    color_buffer[2].Red = (byte)((color_buffer[0].Red + color_buffer[1].Red) >> 1);
                    color_buffer[2].Green = (byte)((color_buffer[0].Green + color_buffer[1].Green) >> 1);
                    color_buffer[2].Blue = (byte)((color_buffer[0].Blue + color_buffer[1].Blue) >> 1);
                    color_buffer[2].Alpha = 0xFF;
                    // color3 = 0
                    color_buffer[3].Red = 0x0;
                    color_buffer[3].Green = 0x0;
                    color_buffer[3].Blue = 0x0;
                    color_buffer[3].Alpha = alpha ? (byte)0x0 : (byte)0xFF;
                }
                // 根据索引赋值
                for (int i = 0; i < 16; i++)
                {
                    *color++ = color_buffer[color_flags & 0x3];
                    color_flags >>= 2;
                }
            }
        }
    }
}
