namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class RGBA_DXT5_BLOCK_MORTON : ICoder
    {
        public bool CheckWidth(int width) => width >= 4 && (width & (width < 32 ? (width - 1) : 31)) == 0;

        public bool CheckHeight(int height) => height >= 4 && (height & (height < 32 ? (height - 1) : 31)) == 0;

        public bool CheckWidthHeight(int width, int height) => (width < 32 && height < 32 && (width == height)) || (!(width < 32 || height < 32));

        public int GetSize(int width, int height) => ((width + 3) / 4 * 4) * ((height + 3) / 4 * 4);

        public int GetCheck(int width) => width;

        public void Decode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                byte* texPtr = tempPtr;
                int width = tex.Width;
                int height = tex.Height;
                int border = width < 32 ? width : 32;
                int times = border * border / 16;
                YFColor* color_buffer = stackalloc YFColor[16];
                for (int y = 0; y < height; y += 32)
                {
                    for (int x = 0; x < width; x += 32)
                    {
                        for (int tb = 0; tb < times; tb++)
                        {
                            CompressedCoder.S3TC.DecodeBlock_RGBA_DXT5(texPtr, color_buffer);
                            texPtr += 16;
                            int dx = 0, dy = 0;
                            FromZOrder(tb, &dx, &dy, border);
                            for (int i = 0; i < 4; i++)
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    if ((y + i + dy) < height && (x + j + dx) < width)
                                    {
                                        dataPtr[(y + i + dy) * width + x + j + dx] = color_buffer[(i << 2) | j];
                                    }
                                }
                            }
                        }
                    }
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
                int border = width < 32 ? width : 32;
                int times = border * border / 16;
                YFColor* color_buffer = stackalloc YFColor[16];
                for (int y = 0; y < height; y += 32)
                {
                    for (int x = 0; x < width; x += 32)
                    {
                        for (int tb = 0; tb < times; tb++)
                        {
                            int dx = 0, dy = 0;
                            FromZOrder(tb, &dx, &dy, border);
                            for (int i = 0; i < 4; i++)
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    color_buffer[(i << 2) | j] =
                                        ((y + i + dy) < height && (x + j + dx) < width)
                                        ? dataPtr[(y + i + dy) * width + x + j + dx]
                                        : YFColor.Empty;
                                }
                            }
                            CompressedCoder.S3TC.EncodeBlock_RGBA_DXT5(texPtr, color_buffer);
                            texPtr += 16;
                        }
                    }
                }
            }
        }

        static void FromZOrder(int z, int* x, int* y, int border)
        {
            *x = *y = 0;
            for (int j = 0; j < 16; j++)
            {
                *x |= ((z >> (j * 2 + 0)) & 1) << j;
                *y |= ((z >> (j * 2 + 1)) & 1) << j;
            }
            *x *= 4;
            *y *= 4;
        }
    }
}
