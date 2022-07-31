namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class A8_R8_G8_B8 : ICoder
    {
        public bool CheckWidth(int width) => true;

        public bool CheckHeight(int height) => true;

        public bool CheckWidthHeight(int width, int height) => true;

        public int GetSize(int width, int height) => width * height * 4;

        public int GetCheck(int width) => width << 2;

        public void Decode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                byte* texPtr = tempPtr;
                int S = tex.Width * tex.Height;
                for (int i = 0; i < S; i++)
                {
                    dataPtr->Alpha = *texPtr++;
                    dataPtr->Red = *texPtr++;
                    dataPtr->Green = *texPtr++;
                    dataPtr->Blue = *texPtr++;
                    dataPtr++;
                }
            }
        }

        public void Encode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                byte* texPtr = tempPtr;
                int S = tex.Width * tex.Height;
                for (int i = 0; i < S; i++)
                {
                    *texPtr++ = dataPtr->Alpha;
                    *texPtr++ = dataPtr->Red;
                    *texPtr++ = dataPtr->Green;
                    *texPtr++ = dataPtr->Blue;
                    dataPtr++;
                }
            }
        }
    }
}
