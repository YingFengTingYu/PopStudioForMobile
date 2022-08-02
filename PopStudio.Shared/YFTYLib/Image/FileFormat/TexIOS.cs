using PopStudio.Image.Texture;
using PopStudio.Plugin;
using System;
using System.IO;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.Image.FileFormat
{
    public static class TexIOS
    {
        public static void Encode(YFFile inFile, YFFile outFile, int format, Func<int, TextureFormat> func)
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
                head.width = (ushort)texture2d.Width;
                head.height = (ushort)texture2d.Height;
                head.format = (ushort)format;
                head.Write(bs);
                bs.Write(texture2d.TexData, 0, texture2d.TexData.Length);
            }
        }

        public static void Decode(YFFile inFile, YFFile outFile, Func<int, TextureFormat> func)
        {
            YFTexture2D texture2d;
            using (BinaryStream bs = inFile.OpenAsBinaryStream())
            {
                TexHead head = new TexHead();
                head.Read(bs);
                int size = (int)(bs.Length - bs.Position);
                texture2d = new YFTexture2D();
                texture2d.Width = head.width;
                texture2d.Height = head.height;
                texture2d.TexFormat = func(head.format);
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

        public struct TexHead
        {
            public const ushort magic = 2677;

            public ushort width;
            public ushort height;
            public ushort format;

            public TexHead Read(BinaryStream bs)
            {
                bs.IdUInt16(magic);
                width = bs.ReadUInt16();
                height = bs.ReadUInt16();
                format = bs.ReadUInt16();
                return this;
            }

            public void Write(BinaryStream bs)
            {
                bs.WriteUInt16(magic);
                bs.WriteUInt16(width);
                bs.WriteUInt16(height);
                bs.WriteUInt16(format);
            }
        }
    }
}
