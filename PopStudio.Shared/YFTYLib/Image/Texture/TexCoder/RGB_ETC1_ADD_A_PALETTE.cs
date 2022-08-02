namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class RGB_ETC1_ADD_A_PALETTE : ICoder
    {
        public bool CheckWidth(int width) => width > 0;

        public bool CheckHeight(int height) => height > 0;

        public bool CheckWidthHeight(int width, int height) => true;

        public int GetSize(int width, int height) => (((width + 3) / 4 * 4) * ((height + 3) / 4 * 4) / 2) + 1 + 16 + ((width * height + 1) / 2);

        public int GetCheck(int width) => width << 2;

        public void Decode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                byte* texPtr = tempPtr;
                int width = tex.Width;
                int height = tex.Height;
                YFColor* color_buffer = stackalloc YFColor[16];
                for (int y = 0; y < height; y += 4)
                {
                    for (int x = 0; x < width; x += 4)
                    {
                        CompressedCoder.ETC.DecodeBlock_RGB_ETC1(texPtr, color_buffer);
                        texPtr += 8;
                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                if ((y + i) < height && (x + j) < width)
                                {
                                    dataPtr[(y + i) * width + x + j] = color_buffer[(i << 2) | j];
                                }
                            }
                        }
                    }
                }
                // Read the table
                int num = *texPtr++;
                byte* palette = stackalloc byte[num == 0 ? 2 : num];
                int bitDepth;
                if (num == 0)
                {
                    palette[0] = 0x0;
                    palette[1] = 0xFF;
                    bitDepth = 1;
                }
                else
                {
                    for (int i = 0; i < num; i++)
                    {
                        palette[i] = (byte)((*texPtr << 4) | *texPtr);
                        texPtr++;
                    }
                    int tableSize_POT = 2;
                    for (bitDepth = 1; num > tableSize_POT; bitDepth++)
                    {
                        tableSize_POT *= 2;
                    }
                }
                int S = width * height;
                int BitPosition = 0;
                byte buffer = 0;
                int ReadOneBit()
                {
                    if (BitPosition == 0)
                    {
                        buffer = *texPtr++;
                    }
                    BitPosition = (BitPosition + 7) & 7;
                    return (buffer >> BitPosition) & 0b1;
                }
                int ReadBits(int bits)
                {
                    int ans = 0;
                    for (int i = bits - 1; i >= 0; i--)
                    {
                        ans |= ReadOneBit() << i;
                    }
                    return ans;
                }
                for (int i = 0; i < S; i++)
                {
                    dataPtr++->Alpha = palette[ReadBits(bitDepth)];
                }
            }
        }

        public void Encode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                byte* texPtr = tempPtr;
                int width = tex.Width;
                int height = tex.Height;
                YFColor* color_buffer = stackalloc YFColor[16];
                for (int y = 0; y < height; y += 4)
                {
                    for (int x = 0; x < width; x += 4)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                color_buffer[(i << 2) | j] =
                                    ((y + i) < height && (x + j) < width)
                                    ? dataPtr[(y + i) * width + x + j]
                                    : YFColor.Black;
                            }
                        }
                        CompressedCoder.ETC.EncodeBlock_RGB_ETC1(texPtr, color_buffer);
                        texPtr += 8;
                    }
                }
                *texPtr++ = 16;
                for (byte i = 0; i < 16; i++)
                {
                    *texPtr++ = i;
                }
                int S = width * height;
                int HalfS = S / 2;
                for (int i = 0; i < HalfS; i++)
                {
                    *texPtr = (byte)(dataPtr++->Alpha & 0xF0);
                    *texPtr |= (byte)(dataPtr++->Alpha >> 4);
                    texPtr++;
                }
                if ((S & 1) == 1)
                {
                    *texPtr++ = (byte)(dataPtr++->Alpha & 0xF0);
                }
                tex.SetInfo<int>("AlphaSize", 1 + 16 + (width * height + 1) / 2);
                tex.SetInfo<int>("AlphaFormat", 100);
            }
        }
    }
}
