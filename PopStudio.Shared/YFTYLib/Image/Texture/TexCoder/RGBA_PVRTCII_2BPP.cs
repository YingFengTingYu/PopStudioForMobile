namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class RGBA_PVRTCII_2BPP : ICoder
    {
        public bool CheckWidth(int width) => width > 0;

        public bool CheckHeight(int height) => height > 0;

        public bool CheckWidthHeight(int width, int height) => true;

        public int GetSize(int width, int height) => ((width + 3) / 4 * 4) * ((height + 3) / 4 * 4) / 4;

        public int GetCheck(int width) => (width + 3) >> 2;

        public void Decode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                CompressedCoder.PVRTC.DecodeTexture_RGBA_PVRTCII_2BPP(
                    tempPtr,
                    dataPtr,
                    (uint)tex.Width,
                    (uint)tex.Height
                    );
            }
        }

        public void Encode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                CompressedCoder.PVRTC.EncodeTexture_RGBA_PVRTCII_2BPP(
                    tempPtr,
                    dataPtr,
                    (uint)tex.Width,
                    (uint)tex.Height
                    );
            }
        }
    }
}
