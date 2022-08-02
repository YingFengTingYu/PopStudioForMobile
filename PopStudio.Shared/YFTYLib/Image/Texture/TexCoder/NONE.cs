using System;

namespace PopStudio.Image.Texture.TexCoder
{
    public class NONE : ICoder
    {
        public bool CheckHeight(int height)
        {
            throw new NotImplementedException();
        }

        public bool CheckWidth(int width)
        {
            throw new NotImplementedException();
        }

        public bool CheckWidthHeight(int width, int height)
        {
            throw new NotImplementedException();
        }

        public int GetSize(int width, int height)
        {
            throw new NotImplementedException();
        }

        public int GetCheck(int height)
        {
            throw new NotImplementedException();
        }

        public unsafe void Decode(YFTexture2D tex, YFColor* dataPtr)
        {
            throw new NotImplementedException();
        }

        public unsafe void Encode(YFTexture2D tex, YFColor* dataPtr)
        {
            throw new NotImplementedException();
        }
    }
}
