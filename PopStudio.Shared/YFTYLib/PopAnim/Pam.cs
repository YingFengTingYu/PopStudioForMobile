using PopStudio.Plugin;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.PopAnim
{
    /// <summary>
    /// It's all from Disassembling PVZ2 and Zuma's Revenge!
    /// </summary>
    internal class PamBinary
    {
        public static void Encode(PopAnimInfo pam, YFFile outFile)
        {
            using (BinaryStream bs = outFile.CreateAsBinaryStream())
            {
                pam.Write(bs);
            }
        }

        public static PopAnimInfo Decode(YFFile inFile)
        {
            using (BinaryStream bs = inFile.OpenAsBinaryStream())
            {
                return new PopAnimInfo().Read(bs);
            }
        }
    }
}
