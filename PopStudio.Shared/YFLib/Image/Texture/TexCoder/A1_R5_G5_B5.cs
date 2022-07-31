namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class A1_R5_G5_B5 : ICoder
    {
        public bool CheckWidth(int width) => true;

        public bool CheckHeight(int height) => true;

        public bool CheckWidthHeight(int width, int height) => true;

        public int GetSize(int width, int height) => width * height * 2;

        public int GetCheck(int width) => width << 1;

        public void Decode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                byte* texPtr = tempPtr;
                int S = tex.Width * tex.Height;
                ushort buffer;
                int buffer2;
                for (int i = 0; i < S; i++)
                {
                    buffer = *texPtr++;
                    buffer |= (ushort)(*texPtr++ << 8);
                    dataPtr->Alpha = (byte)-(buffer >> 15);
                    buffer2 = buffer >> 10;
                    dataPtr->Red = (byte)((buffer2 << 3) | (buffer2 >> 2));
                    buffer2 = (buffer >> 5) & 0x1F;
                    dataPtr->Green = (byte)((buffer2 << 3) | (buffer2 >> 2));
                    buffer2 = buffer & 0x1F;
                    dataPtr->Blue = (byte)((buffer2 << 3) | (buffer2 >> 2));
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
                int buffer;
                for (int i = 0; i < S; i++)
                {
                    buffer = dataPtr->Alpha >> 7 << 15;
                    buffer |= dataPtr->Red >> 3 << 10;
                    buffer |= dataPtr->Green >> 3 << 5;
                    buffer |= dataPtr->Blue >> 3;
                    dataPtr++;
                    *texPtr++ = (byte)buffer;
                    *texPtr++ = (byte)(buffer >> 8);
                }
            }
        }
    }
}
