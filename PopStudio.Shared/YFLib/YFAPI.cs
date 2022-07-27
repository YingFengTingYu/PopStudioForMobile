using PopStudio.Rton;
using System;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio
{
    public static class YFAPI
    {
        public static void DecodeRton(YFFile inFile, YFFile outFile, int format, RtonSetting setting)
        {
            switch (format)
            {
                case 0: // Simple Rton
                    Rton.Rton.Decode(inFile, outFile);
                    break;
                case 1: // Encrypted Rton
                    Rton.Rton.DecodeAndDecrypt(inFile, outFile, setting.Cipher);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public static void EncodeRton(YFFile inFile, YFFile outFile, int format, RtonSetting setting)
        {
            switch (format)
            {
                case 0: // Simple Rton
                    Rton.Rton.Encode(inFile, outFile);
                    break;
                case 1: // Encrypted Rton
                    Rton.Rton.EncodeAndEncrypt(inFile, outFile, setting.Cipher);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
