using PopStudio.Image.Texture;
using PopStudio.Plugin;
using System.IO;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.Image.FileFormat
{
    public static class PtxPSV
    {
        public static void Encode(YFFile inFile, YFFile outFile)
        {
            YFTexture2D texture2d;
            using (Stream inStream = inFile.OpenAsStream())
            {
                using (YFBitmap bitmap = YFBitmap.Create(inStream))
                {
                    texture2d = Coder.Encode(bitmap, TextureFormat.RGBA_DXT5_REFLECTEDMORTON);
                }
            }
            using (BinaryStream bs = outFile.CreateAsBinaryStream())
            {
                PtxHead head = new PtxHead();
                head.width = (ushort)texture2d.Width;
                head.height = (ushort)texture2d.Height;
                head.size = texture2d.TexData.Length;
                head.Write(bs);
                bs.Write(texture2d.TexData, 0, texture2d.TexData.Length);
            }
        }

        public static void Decode(YFFile inFile, YFFile outFile)
        {
            YFTexture2D texture2d;
            using (BinaryStream bs = inFile.OpenAsBinaryStream())
            {
                PtxHead head = new PtxHead();
                head.Read(bs);
                int size = (int)(bs.Length - bs.Position);
                texture2d = new YFTexture2D();
                texture2d.Width = head.width;
                texture2d.Height = head.height;
                texture2d.TexFormat = TextureFormat.RGBA_DXT5_REFLECTEDMORTON;
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
            public static readonly string magic = "GXT\0";
            public static readonly int version = 0x10000003;

            public int size;
            public ushort width;
            public ushort height;

            public PtxHead Read(BinaryStream bs)
            {
                bs.IdString(magic);
                bs.IdInt32(version);
                bs.IdInt32(1);
                bs.IdInt32(0x40);
                size = bs.ReadInt32();
                bs.IdInt32(0);
                bs.IdInt32(0);
                bs.IdInt32(0);
                bs.IdInt32(0x40);
                bs.IdInt32(size);
                bs.IdInt32(-1);
                bs.IdInt32(0);
                bs.IdInt32(0);
                bs.IdInt32(-2030043136);
                width = bs.ReadUInt16();
                height = bs.ReadUInt16();
                bs.IdInt32(1);
                return this;
            }

            public void Write(BinaryStream bs)
            {
                bs.WriteString(magic);
                bs.WriteInt32(version);
                bs.WriteInt32(1);
                bs.WriteInt32(0x40);
                bs.WriteInt32(size);
                bs.WriteInt32(0);
                bs.WriteInt32(0);
                bs.WriteInt32(0);
                bs.WriteInt32(0x40);
                bs.WriteInt32(size);
                bs.WriteInt32(-1);
                bs.WriteInt32(0);
                bs.WriteInt32(0);
                bs.WriteInt32(-2030043136);
                bs.WriteUInt16(width);
                bs.WriteUInt16(height);
                bs.WriteInt32(1);
            }
        }
    }
}
