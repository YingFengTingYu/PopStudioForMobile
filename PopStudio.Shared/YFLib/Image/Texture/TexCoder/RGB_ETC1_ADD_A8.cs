namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class RGB_ETC1_ADD_A8 : ICoder
    {
        public bool CheckWidth(int width) => width > 0;

        public bool CheckHeight(int height) => height > 0;

        public bool CheckWidthHeight(int width, int height) => true;

        public int GetSize(int width, int height) => (((width + 3) / 4 * 4) * ((height + 3) / 4 * 4) / 2) + width * height;

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
                int S = width * height;
                for (int i = 0; i < S; i++)
                {
                    dataPtr++->Alpha = *texPtr++;
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
                int S = width * height;
                for (int i = 0; i < S; i++)
                {
                    *texPtr++ = dataPtr++->Alpha;
                }
            }
        }
    }
}
