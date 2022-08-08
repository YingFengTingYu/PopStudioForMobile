namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class R5_G6_B5_BIGENDIAN_PADDING : ICoder
    {
        public bool CheckWidth(int width) => true;

        public bool CheckHeight(int height) => true;

        public bool CheckWidthHeight(int width, int height) => true;

        public int GetSize(int width, int height) => ((width + 127) / 128 * 128) * height * 2;

        public int GetCheck(int width) => ((width + 127) / 128 * 128) << 1;

        public void Decode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                byte* texPtr = tempPtr;
                int width = tex.Width;
                int padding = (((width + 127) / 128 * 128) - width) * 2;
                int height = tex.Height;
                ushort buffer;
                int buffer2;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        buffer = (ushort)(*texPtr++ << 8);
                        buffer |= *texPtr++;
                        buffer2 = buffer >> 11;
                        dataPtr->Red = (byte)((buffer2 << 3) | (buffer2 >> 2));
                        buffer2 = (buffer >> 5) & 0x3F;
                        dataPtr->Green = (byte)((buffer2 << 2) | (buffer2 >> 4));
                        buffer2 = buffer & 0x1F;
                        dataPtr->Blue = (byte)((buffer2 << 3) | (buffer2 >> 2));
                        dataPtr->Alpha = 0xFF;
                        dataPtr++;
                    }
                    texPtr += padding;
                }
            }
        }

        public void Encode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                byte* texPtr = tempPtr;
                int width = tex.Width;
                int padding = (((width + 127) / 128 * 128) - width) * 2;
                int height = tex.Height;
                int buffer;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        buffer = dataPtr->Red >> 3 << 11;
                        buffer |= dataPtr->Green >> 2 << 5;
                        buffer |= dataPtr->Blue >> 3;
                        dataPtr++;
                        *texPtr++ = (byte)(buffer >> 8);
                        *texPtr++ = (byte)buffer;
                    }
                    for (int i = 0; i < padding; i++)
                    {
                        *texPtr++ = 0;
                    }
                }
            }
        }
    }
}
