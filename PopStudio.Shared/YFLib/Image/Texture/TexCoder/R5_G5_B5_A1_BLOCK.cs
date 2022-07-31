namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class R5_G5_B5_A1_BLOCK : ICoder
    {
        public bool CheckWidth(int width) => (width & 31) == 0;

        public bool CheckHeight(int height) => (height & 31) == 0;

        public bool CheckWidthHeight(int width, int height) => true;

        public int GetSize(int width, int height) => width * height * 2;

        public int GetCheck(int width) => width << 1;

        public void Decode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                byte* texPtr = tempPtr;
                int width = tex.Width;
                int height = tex.Height;
                ushort buffer;
                int buffer2;
                for (int y_line = 0; y_line < height; y_line += 32)
                {
                    for (int x_line = 0; x_line < width; x_line += 32)
                    {
                        for (int y = 0; y < 32; y++)
                        {
                            YFColor* t = dataPtr + (y_line + y) * width + x_line;
                            for (int x = 0; x < 32; x++)
                            {
                                buffer = *texPtr++;
                                buffer |= (ushort)(*texPtr++ << 8);
                                buffer2 = buffer >> 11;
                                t->Red = (byte)((buffer2 << 3) | (buffer2 >> 2));
                                buffer2 = (buffer >> 6) & 0x1F;
                                t->Green = (byte)((buffer2 << 3) | (buffer2 >> 2));
                                buffer2 = (buffer >> 1) & 0x1F;
                                t->Blue = (byte)((buffer2 << 3) | (buffer2 >> 2));
                                t->Alpha = (byte)-(buffer & 0x1);
                                t++;
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
                int buffer;
                for (int y_line = 0; y_line < height; y_line += 32)
                {
                    for (int x_line = 0; x_line < width; x_line += 32)
                    {
                        for (int y = 0; y < 32; y++)
                        {
                            YFColor* t = dataPtr + (y_line + y) * width + x_line;
                            for (int x = 0; x < 32; x++)
                            {
                                buffer = t->Red >> 3 << 11;
                                buffer |= t->Green >> 3 << 6;
                                buffer |= t->Blue >> 3 << 1;
                                buffer |= t->Alpha >> 7;
                                t++;
                                *texPtr++ = (byte)buffer;
                                *texPtr++ = (byte)(buffer >> 8);
                            }
                        }
                    }
                }
            }
        }
    }
}
