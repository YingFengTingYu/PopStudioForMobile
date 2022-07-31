using PopStudio.Image.Texture;
using PopStudio.Plugin;
using System;
using System.IO;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.Image.FileFormat
{
    public static class PtxRsb
    {
        public static void Encode(YFFile inFile, YFFile outFile, int format, Endian endian, Func<int, Endian, TextureFormat> func)
        {
            YFTexture2D texture2d;
            using (Stream inStream = inFile.OpenAsStream())
            {
                using (YFBitmap bitmap = YFBitmap.Create(inStream))
                {
                    texture2d = Coder.Encode(bitmap, func(format, endian));
                }
            }
            using (BinaryStream bs = outFile.CreateAsBinaryStream())
            {
                bs.Endian = endian;
                PtxHead head = new PtxHead();
                head.width = texture2d.Width;
                head.height = texture2d.Height;
                head.check = texture2d.Check;
                head.format = format;
                head.alphaSize = texture2d.GetInfo<int>("AlphaSize");
                head.alphaFormat = texture2d.GetInfo<int>("AlphaFormat");
                head.noinfo = false;
                head.Write(bs);
                bs.Write(texture2d.TexData, 0, texture2d.TexData.Length);
            }
        }

        public static void Decode(YFFile inFile, YFFile outFile, Func<int, Endian, TextureFormat> func)
        {
            YFTexture2D texture2d;
            using (BinaryStream bs = inFile.OpenAsBinaryStream())
            {
                PtxHead head = new PtxHead();
                head.Read(bs);
                if (head.noinfo)
                {
                    throw new DataMismatchException();
                }
                int size = (int)(bs.Length - bs.Position);
                texture2d = new YFTexture2D();
                texture2d.Width = head.width;
                texture2d.Height = head.height;
                texture2d.Check = head.check;
                texture2d.TexFormat = func(head.format, bs.Endian);
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
            public const int magic = 1886681137;
            public const int magic_noinfo = 1886681136;
            public const int version = 1;

            public int width;
            public int height;
            public int check;
            public int format;
            public int alphaSize;
            public int alphaFormat;
            public bool noinfo;

            public PtxHead Read(BinaryStream bs)
            {
                int thismagic = bs.ReadInt32();
                if (thismagic == 829977712)
                {
                    bs.Endian = bs.Endian == Endian.Small ? Endian.Big : Endian.Small;
                }
                else if (thismagic == magic_noinfo)
                {
                    noinfo = true;
                }
                else if (thismagic == 813200496)
                {
                    noinfo = true;
                    bs.Endian = bs.Endian == Endian.Small ? Endian.Big : Endian.Small;
                }
                else if (thismagic != magic)
                {
                    throw new DataMismatchException();
                }
                bs.IdInt32(version);
                width = bs.ReadInt32();
                height = bs.ReadInt32();
                check = bs.ReadInt32();
                format = bs.ReadInt32();
                alphaSize = bs.ReadInt32();
                alphaFormat = bs.ReadInt32();
                return this;
            }

            public void Write(BinaryStream bs)
            {
                if (noinfo)
                {
                    bs.WriteInt32(magic_noinfo);
                    bs.WriteInt32(version);
                    bs.WriteInt32(0);
                    bs.WriteInt32(0);
                    bs.WriteInt32(0);
                    bs.WriteInt32(0);
                    bs.WriteInt32(0);
                    bs.WriteInt32(0);
                }
                else
                {
                    bs.WriteInt32(magic);
                    bs.WriteInt32(version);
                    bs.WriteInt32(width);
                    bs.WriteInt32(height);
                    bs.WriteInt32(check);
                    bs.WriteInt32(format);
                    bs.WriteInt32(alphaSize);
                    bs.WriteInt32(alphaFormat);
                }
            }
        }
    }
}
