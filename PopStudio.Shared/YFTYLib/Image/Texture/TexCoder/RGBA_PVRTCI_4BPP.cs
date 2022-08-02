namespace PopStudio.Image.Texture.TexCoder
{
    public unsafe class RGBA_PVRTCI_4BPP : ICoder
    {
        public bool CheckWidth(int width) => width >= 8 && (width & (width - 1)) == 0;

        public bool CheckHeight(int height) => height >= 8 && (height & (height - 1)) == 0;

        public bool CheckWidthHeight(int width, int height) => true;

        public int GetSize(int width, int height) => width * height / 2;

        public int GetCheck(int width) => width >> 1;

        public void Decode(YFTexture2D tex, YFColor* dataPtr)
        {
            fixed (byte* tempPtr = tex.TexData)
            {
                CompressedCoder.PVRTC.DecodeTexture_RGBA_PVRTCI_4BPP(
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
                CompressedCoder.PVRTC.EncodeTexture_RGBA_PVRTCI_4BPP(
                    tempPtr,
                    dataPtr,
                    (uint)tex.Width,
                    (uint)tex.Height
                    );
            }
        }
    }
}
