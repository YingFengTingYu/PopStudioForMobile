using PopStudio.Plugin;
using System.Text;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.Image.FileFormat
{
    public static class Cdat
    {
        public static void Encode(YFFile inFile, YFFile outFile, string cipher)
        {
            byte[] code = Encoding.UTF8.GetBytes(cipher);
            using (BinaryStream bs2 = outFile.CreateAsBinaryStream())
            {
                using (BinaryStream bs = inFile.OpenAsBinaryStream())
                {
                    CdatHead head = new CdatHead
                    {
                        size = bs.Length
                    };
                    head.Write(bs2);
                    if (head.size >= 0x100)
                    {
                        int index = 0;
                        int arysize = code.Length;
                        for (int i = 0; i < 0x100; i++)
                        {
                            bs2.WriteByte((byte)(bs.ReadByte() ^ code[index++]));
                            index %= arysize;
                        }
                    }
                    bs.CopyTo(bs2);
                }
            }
        }

        public static void Decode(YFFile inFile, YFFile outFile, string cipher)
        {
            byte[] code = Encoding.UTF8.GetBytes(cipher);
            using (BinaryStream bs = inFile.OpenAsBinaryStream())
            {
                CdatHead head = new CdatHead();
                head.Read(bs);
                using (BinaryStream bs2 = outFile.CreateAsBinaryStream())
                {
                    if (bs.Length >= 0x112)
                    {
                        int index = 0;
                        int arysize = code.Length;
                        for (int i = 0; i < 0x100; i++)
                        {
                            bs2.WriteByte((byte)(bs.ReadByte() ^ code[index++]));
                            index %= arysize;
                        }
                    }
                    bs.CopyTo(bs2);
                }
            }
        }

        public struct CdatHead
        {
            public const string magic = "CRYPT_RES\x0A\x00";

            public long size;

            public CdatHead Read(BinaryStream bs)
            {
                bs.IdString(magic);
                size = bs.ReadInt64();
                return this;
            }

            public void Write(BinaryStream bs)
            {
                bs.WriteString(magic);
                bs.WriteInt64(size);
            }
        }
    }
}
