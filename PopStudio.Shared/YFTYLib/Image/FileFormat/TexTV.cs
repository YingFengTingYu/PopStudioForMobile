using PopStudio.Image.Texture;
using PopStudio.Plugin;
using System;
using System.IO;
using System.IO.Compression;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.Image.FileFormat
{
    public static class TexTV
    {
        public static void Encode(YFFile inFile, YFFile outFile, int format, bool zlib, Func<int, TextureFormat> func)
        {
            YFTexture2D texture2d;
            using (Stream inStream = inFile.OpenAsStream())
            {
                using (YFBitmap bitmap = YFBitmap.Create(inStream))
                {
                    texture2d = Coder.Encode(bitmap, func(format));
                }
            }
            using (BinaryStream bs = outFile.CreateAsBinaryStream())
            {
                TexHead head = new TexHead();
                head.width = texture2d.Width;
                head.height = texture2d.Height;
                head.format = format;
                if (zlib)
                {
                    head.flags |= 1;
                    head.Write(bs);
                    using (ZLibStream zlibStream = new ZLibStream(bs, CompressionLevel.Optimal, true))
                    {
                        zlibStream.Write(texture2d.TexData, 0, texture2d.TexData.Length);
                    }
                    head.zsize = (int)(bs.Length - 48);
                    bs.Position = 0;
                    head.Write(bs);
                }
                else
                {
                    head.Write(bs);
                    bs.Write(texture2d.TexData, 0, texture2d.TexData.Length);
                }
            }
        }

        public static void Decode(YFFile inFile, YFFile outFile, Func<int, TextureFormat> func)
        {
            YFTexture2D texture2d;
            using (BinaryStream bs = inFile.OpenAsBinaryStream())
            {
                TexHead head = new TexHead();
                head.Read(bs);
                texture2d = new YFTexture2D();
                texture2d.Width = head.width;
                texture2d.Height = head.height;
                texture2d.TexFormat = func(head.format);
                if ((head.flags & 1) == 1)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (ZLibStream zLibStream = new ZLibStream(bs, CompressionMode.Decompress))
                        {
                            zLibStream.CopyTo(ms);
                        }
                        texture2d.TexData = ms.ToArray();
                    }
                }
                else
                {
                    int size = (int)(bs.Length - bs.Position);
                    texture2d.TexData = new byte[size];
                    bs.Read(texture2d.TexData, 0, size);
                }
            }
            using (YFBitmap bitmap = Coder.Decode(texture2d))
            {
                using (Stream outStream = outFile.CreateAsStream())
                {
                    bitmap.Save(outStream);
                }
            }
        }

        public struct TexHead
        {
            public const string magic = "SEXYTEX\0";
            public const int version = 0;

            public int width;
            public int height;
            public int format;
            public uint flags;
            public int zsize;

            public TexHead Read(BinaryStream bs)
            {
                bs.IdString(magic);
                bs.IdInt32(version);
                width = bs.ReadInt32();
                height = bs.ReadInt32();
                format = bs.ReadInt32();
                flags = bs.ReadUInt32();
                bs.Position += 4;
                zsize = bs.ReadInt32();
                bs.Position += 12;
                return this;
            }

            public void Write(BinaryStream bs)
            {
                bs.WriteString(magic);
                bs.WriteInt32(version);
                bs.WriteInt32(width);
                bs.WriteInt32(height);
                bs.WriteInt32(format);
                bs.WriteUInt32(flags);
                bs.WriteInt32(1);
                bs.WriteInt32(zsize);
                bs.WriteInt32(0);
                bs.WriteInt32(0);
                bs.WriteInt32(0);
            }
        }
    }
}
