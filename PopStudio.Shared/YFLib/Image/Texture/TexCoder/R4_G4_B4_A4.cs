namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class R4_G4_B4_A4 : ICoder
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
                    buffer2 = buffer >> 12;
                    dataPtr->Red = (byte)((buffer2 << 4) | buffer2);
                    buffer2 = (buffer >> 8) & 0xF;
                    dataPtr->Green = (byte)((buffer2 << 4) | buffer2);
                    buffer2 = (buffer >> 4) & 0xF;
                    dataPtr->Blue = (byte)((buffer2 << 4) | buffer2);
                    buffer2 = buffer & 0xF;
                    dataPtr->Alpha = (byte)((buffer2 << 4) | buffer2);
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
                    buffer = dataPtr->Red >> 4 << 12;
                    buffer |= dataPtr->Green >> 4 << 8;
                    buffer |= dataPtr->Blue >> 4 << 4;
                    buffer |= dataPtr->Alpha >> 4;
                    dataPtr++;
                    *texPtr++ = (byte)buffer;
                    *texPtr++ = (byte)(buffer >> 8);
                }
            }
        }
    }
}
