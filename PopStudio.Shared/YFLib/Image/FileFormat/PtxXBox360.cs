using PopStudio.Image.Texture;
using PopStudio.Plugin;
using System.IO;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.Image.FileFormat
{
    public static class PtxXBox360
    {
        public static void Encode(YFFile inFile, YFFile outFile)
        {
            YFTexture2D texture2d;
            using (Stream inStream = inFile.OpenAsStream())
            {
                using (YFBitmap bitmap = YFBitmap.Create(inStream))
                {
                    texture2d = Coder.Encode(bitmap, TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING);
                }
            }
            using (BinaryStream bs = outFile.CreateAsBinaryStream())
            {
                bs.Endian = Endian.Big;
                PtxHead head = new PtxHead();
                head.width = texture2d.Width;
                head.height = texture2d.Height;
                head.blockSize = ((texture2d.Width + 127) / 128 * 128) << 2;
                bs.Write(texture2d.TexData, 0, texture2d.TexData.Length);
                head.Write(bs);
            }
        }

        public static void Decode(YFFile inFile, YFFile outFile)
        {
            YFTexture2D texture2d;
            using (BinaryStream bs = inFile.OpenAsBinaryStream())
            {
                bs.Endian = Endian.Big;
                if (bs.Length < 16)
                {
                    throw new DataMismatchException();
                }
                bs.Position = bs.Length - 16;
                PtxHead head = new PtxHead();
                head.Read(bs);
                bs.Position = 0;
                int size = (int)(bs.Length - 16);
                texture2d = new YFTexture2D();
                texture2d.Width = head.width;
                texture2d.Height = head.height;
                texture2d.TexFormat = TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING;
                texture2d.TexData = new byte[size];
                bs.Read(texture2d.TexData, 0, size);
            }
            using (YFBitmap bitmap = Coder.Decode(texture2d))
            {
                using (Stream outStream = outFile.CreateAsStream())
                {
                    bitmap.Save(outStream);
                }
            }
        }

        public struct PtxHead
        {
            public static readonly int magic = 1409294362;

            public int width;
            public int height;
            public int blockSize;

            public PtxHead Read(BinaryStream bs)
            {
                width = bs.ReadInt32();
                height = bs.ReadInt32();
                blockSize = bs.ReadInt32();
                bs.IdInt32(magic);
                return this;
            }

            public void Write(BinaryStream bs)
            {
                bs.WriteInt32(width);
                bs.WriteInt32(height);
                bs.WriteInt32(blockSize);
                bs.WriteInt32(magic);
            }
        }
    }
}
