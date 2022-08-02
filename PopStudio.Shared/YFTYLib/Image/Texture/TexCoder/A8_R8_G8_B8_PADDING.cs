namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class A8_R8_G8_B8_PADDING : ICoder
    {
        public bool CheckWidth(int width) => true;

        public bool CheckHeight(int height) => true;

        public bool CheckWidthHeight(int width, int height) => true;

        public int GetSize(int width, int height) => ((width + 63) / 64 * 64) * height * 4;

        public int GetCheck(int width) => ((width + 63) / 64 * 64) << 2;

        public void Decode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                byte* texPtr = tempPtr;
                int width = tex.Width;
                int padding = (((width + 63) / 64 * 64) - width) * 4;
                int height = tex.Height;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        dataPtr->Alpha = *texPtr++;
                        dataPtr->Red = *texPtr++;
                        dataPtr->Green = *texPtr++;
                        dataPtr->Blue = *texPtr++;
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
                int padding = (((width + 63) / 64 * 64) - width) * 4;
                int height = tex.Height;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        *texPtr++ = dataPtr->Alpha;
                        *texPtr++ = dataPtr->Red;
                        *texPtr++ = dataPtr->Green;
                        *texPtr++ = dataPtr->Blue;
                        dataPtr++;
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
