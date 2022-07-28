﻿using System;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio
{
    public static class YFAPI
    {
        public static void TranscodeTrail(YFFile inFile, YFFile outFile, int inFormat, int outFormat, Trail.TrailSetting setting)
        {
            Trail.Trail trail = inFormat switch
            {
                0 => Trail.PC.Decode(inFile),
                1 => Trail.TV.Decode(inFile),
                2 => Trail.Phone32.Decode(inFile, setting.GetStringFromIndex),
                3 => Trail.Phone64.Decode(inFile, setting.GetStringFromIndex),
                4 => Trail.GameConsole.Decode(inFile),
                5 => Trail.WP.Decode(inFile),
                6 => Trail.TrailJson.Decode(inFile),
                7 => Trail.RawXml.Decode(inFile),
                _ => throw new NotImplementedException()
            };
            switch (outFormat)
            {
                case 0: Trail.PC.Encode(trail, outFile); break;
                case 1: Trail.TV.Encode(trail, outFile); break;
                case 2: Trail.Phone32.Encode(trail, outFile, setting.GetIndexFromString); break;
                case 3: Trail.Phone64.Encode(trail, outFile, setting.GetIndexFromString); break;
                case 4: Trail.GameConsole.Encode(trail, outFile); break;
                case 5: Trail.WP.Encode(trail, outFile); break;
                case 6: Trail.TrailJson.Encode(trail, outFile); break;
                case 7: Trail.RawXml.Encode(trail, outFile); break;
                default: throw new NotImplementedException();
            }
        }

        public static void DecodePam(YFFile inFile, YFFile outFile)
        {
            PopAnim.Pam.Decode(inFile, outFile);
        }

        public static void EncodePam(YFFile inFile, YFFile outFile)
        {
            PopAnim.Pam.Encode(inFile, outFile);
        }

        public static void DecodeRton(YFFile inFile, YFFile outFile, int format, Rton.RtonSetting setting)
        {
            switch (format)
            {
                case 0: Rton.Rton.Decode(inFile, outFile); break;
                case 1: Rton.Rton.DecodeAndDecrypt(inFile, outFile, setting.Cipher); break;
                default: throw new NotImplementedException();
            }
        }

        public static void EncodeRton(YFFile inFile, YFFile outFile, int format, Rton.RtonSetting setting)
        {
            switch (format)
            {
                case 0: Rton.Rton.Encode(inFile, outFile); break;
                case 1: Rton.Rton.EncodeAndEncrypt(inFile, outFile, setting.Cipher); break;
                default: throw new NotImplementedException();
            }
        }
    }
}
