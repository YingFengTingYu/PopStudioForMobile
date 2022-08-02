namespace PopStudio.Image.Texture
{
    public unsafe interface ICoder
    {
        public bool CheckWidth(int width); // => width;

        public bool CheckHeight(int height); // => height;

        public bool CheckWidthHeight(int width, int height);

        public int GetSize(int width, int height);

        public int GetCheck(int width);

        public void Decode(YFTexture2D tex, YFColor* dataPtr);

        public void Encode(YFTexture2D tex, YFColor* dataPtr);
    }
}
