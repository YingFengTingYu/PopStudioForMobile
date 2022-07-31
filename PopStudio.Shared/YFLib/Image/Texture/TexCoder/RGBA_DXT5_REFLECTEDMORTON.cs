namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class RGBA_DXT5_REFLECTEDMORTON : ICoder
    {
        public bool CheckWidth(int width) => width >= 4 && (width & (width - 1)) == 0;

        public bool CheckHeight(int height) => height >= 4 && (height & (height - 1)) == 0;

        public bool CheckWidthHeight(int width, int height) => true;

        public int GetSize(int width, int height) => width * height;

        public int GetCheck(int width) => width;

        public void Decode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                byte* texPtr = tempPtr;
                int width = tex.Width;
                int height = tex.Height;
                YFColor* color_buffer = stackalloc YFColor[16];
                int* order = stackalloc int[16] { 0, 2, 8, 10, 1, 3, 9, 11, 4, 6, 12, 14, 5, 7, 13, 15 };
                int index = 0;
                for (int y = 0; y < height; y += 4)
                {
                    for (int x = 0; x < width; x += 4)
                    {
                        CompressedCoder.S3TC.DecodeBlock_RGBA_DXT5(texPtr, color_buffer);
                        texPtr += 16;
                        for (int i = 0; i < 16; i++)
                        {
                            dataPtr[FromZOrder(index + order[i], width, height)] = color_buffer[i];
                        }
                        index += 16;
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
                YFColor* color_buffer = stackalloc YFColor[16];
                int* order = stackalloc int[16] { 0, 2, 8, 10, 1, 3, 9, 11, 4, 6, 12, 14, 5, 7, 13, 15 };
                int index = 0;
                for (int y = 0; y < height; y += 4)
                {
                    for (int x = 0; x < width; x += 4)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            color_buffer[i] = dataPtr[FromZOrder(index + order[i], width, height)];
                        }
                        CompressedCoder.S3TC.EncodeBlock_RGBA_DXT5(texPtr, color_buffer);
                        texPtr += 16;
                        index += 16;
                    }
                }
            }
        }

        static int FromZOrder(int z, int width, int height)
        {
            int minB = width;
            if (height < width) minB = height;
            int AddTimes = z / (minB * minB);
            int ReadZ = z & (minB * minB - 1);
            int x = 0, y = 0;
            for (int j = 0; j < 16; j++)
            {
                x |= ((ReadZ >> (j * 2 + 1)) & 1) << j;
                y |= ((ReadZ >> (j * 2 + 0)) & 1) << j;
            }
            if (width > height)
            {
                x += AddTimes * minB;
            }
            else if (width < height)
            {
                y += AddTimes * minB;
            }
            return y * width + x;
        }
    }
}
