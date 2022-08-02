namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class R8_G8_B8_A8_ADD_A8 : ICoder
    {
        public bool CheckWidth(int width) => true;

        public bool CheckHeight(int height) => true;

        public bool CheckWidthHeight(int width, int height) => true;

        public int GetSize(int width, int height) => (width * height * 4) + width * height;

        public int GetCheck(int width) => width << 3;

        public void Decode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                YFColor* dataPtrBack = dataPtr;
                byte* texPtr = tempPtr;
                int S = tex.Width * tex.Height;
                for (int i = 0; i < S; i++)
                {
                    dataPtr->Red = *texPtr++;
                    dataPtr->Green = *texPtr++;
                    dataPtr->Blue = *texPtr++;
                    dataPtr->Alpha = *texPtr++;
                    dataPtr++;
                }
                for (int i = 0; i < S; i++)
                {
                    dataPtrBack++->Alpha = *texPtr++;
                }
            }
        }

        public void Encode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                YFColor* dataPtrBack = dataPtr;
                byte* texPtr = tempPtr;
                int S = tex.Width * tex.Height;
                for (int i = 0; i < S; i++)
                {
                    *texPtr++ = dataPtr->Red;
                    *texPtr++ = dataPtr->Green;
                    *texPtr++ = dataPtr->Blue;
                    *texPtr++ = dataPtr->Alpha;
                    dataPtr++;
                }
                for (int i = 0; i < S; i++)
                {
                    *texPtr++ = dataPtrBack++->Alpha;
                }
            }
        }
    }
}
