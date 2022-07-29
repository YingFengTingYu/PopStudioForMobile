namespace PopStudio.YFLib.Image.Texture
{
    public unsafe interface ITextureCoder
    {
        public void Decode(byte* texData, int width, int height);
    }
}
