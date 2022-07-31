namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class RGBA_DXT5_BIGENDIAN_PADDING : ICoder
    {
        public bool CheckWidth(int width) => width > 0;

        public bool CheckHeight(int height) => height > 0;

        public bool CheckWidthHeight(int width, int height) => true;

        public int GetSize(int width, int height) => ((width + 127) / 128 * 128) * ((height + 7) / 4 * 4);

        public int GetCheck(int width) => width;

        public void Decode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                byte* texPtr = tempPtr;
                int width = tex.Width;
                int height = tex.Height;
                int delta = (((width + 127) / 128 * 128) - ((width + 3) / 4 * 4)) * 4;
                YFColor* color_buffer = stackalloc YFColor[16];
                for (int y = 0; y < height; y += 4)
                {
                    for (int x = 0; x < width; x += 4)
                    {
                        CompressedCoder.S3TC.DecodeBlock_RGBA_DXT5_BigEndian(texPtr, color_buffer);
                        texPtr += 16;
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
                    texPtr += delta;
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
                int fwidth = (width + 127) / 128 * 128;
                int delta = (fwidth - ((width + 3) / 4 * 4)) * 4;
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
                                    : YFColor.Empty;
                            }
                        }
                        CompressedCoder.S3TC.EncodeBlock_RGBA_DXT5_BigEndian(texPtr, color_buffer);
                        texPtr += 16;
                    }
                    for (int i = 0; i < delta; i++)
                    {
                        *texPtr++ = 0xCD;
                    }
                }
                int blockSize = fwidth * 4;
                for (int i = 0; i < blockSize; i++)
                {
                    *texPtr++ = 0xCD;
                }
            }
        }
    }
}
