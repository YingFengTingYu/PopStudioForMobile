using PopStudio.Image.Texture;
using PopStudio.Plugin;
using System.IO;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.Image.FileFormat
{
    public static class PtxPS3
    {
        public static void Encode(YFFile inFile, YFFile outFile)
        {
            YFTexture2D texture2d;
            using (Stream inStream = inFile.OpenAsStream())
            {
                using (YFBitmap bitmap = YFBitmap.Create(inStream))
                {
                    texture2d = Coder.Encode(bitmap, TextureFormat.RGBA_DXT5);
                }
            }
            using (BinaryStream bs = outFile.CreateAsBinaryStream())
            {
                PtxHead head = new PtxHead();
                head.width = texture2d.Width;
                head.height = texture2d.Height;
                head.texturesize = texture2d.TexData.Length;
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
                texture2d.TexFormat = TextureFormat.RGBA_DXT5;
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
            public const string magic = "DDS ";
            public const string nvt = "NVTT";
            public const string tex = "DXT5";

            public int height;
            public int width;
            public int texturesize;

            public PtxHead Read(BinaryStream bs)
            {
                bs.IdString(magic);
                bs.IdInt32(0x7C);
                bs.IdInt32(528391);
                height = bs.ReadInt32();
                width = bs.ReadInt32();
                texturesize = bs.ReadInt32();
                for (int i = 0; i < 11; i++)
                {
                    bs.IdInt32(0);
                }
                bs.IdString(nvt);
                bs.IdInt32(131080);
                bs.IdInt32(32);
                bs.IdInt32(4);
                bs.IdString(tex);
                for (int i = 0; i < 5; i++)
                {
                    bs.IdInt32(0);
                }
                bs.IdInt32(4096);
                for (int i = 0; i < 4; i++)
                {
                    bs.IdInt32(0);
                }
                return this;
            }

            public void Write(BinaryStream bs)
            {
                bs.WriteString(magic);
                bs.WriteInt32(0x7C);
                bs.WriteInt32(528391);
                bs.WriteInt32(height);
                bs.WriteInt32(width);
                bs.WriteInt32(texturesize);
                for (int i = 0; i < 11; i++)
                {
                    bs.WriteInt32(0);
                }
                bs.WriteString(nvt);
                bs.WriteInt32(131080);
                bs.WriteInt32(32);
                bs.WriteInt32(4);
                bs.WriteString(tex);
                for (int i = 0; i < 5; i++)
                {
                    bs.WriteInt32(0);
                }
                bs.WriteInt32(4096);
                for (int i = 0; i < 4; i++)
                {
                    bs.WriteInt32(0);
                }
            }
        }
    }
}
