using System;
using PopStudio.Settings;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio
{
    public static class YFAPI
    {
        public static void DecodeImage(YFFile inFile, YFFile outFile, int format)
        {
            switch (format)
            {
                case 0: Image.FileFormat.PtxRsb.Decode(inFile, outFile, GlobalSetting.Singleton.PtxRsbFormat.GetTextureFormat); break;
                case 1: Image.FileFormat.TexTV.Decode(inFile, outFile, GlobalSetting.Singleton.TexTVFormat.GetTextureFormat); break;
                case 2: Image.FileFormat.Cdat.Decode(inFile, outFile, GlobalSetting.Singleton.CdatFormat.Cipher); break;
                case 3: Image.FileFormat.TexIOS.Decode(inFile, outFile, GlobalSetting.Singleton.TexIOSFormat.GetTextureFormat); break;
                case 4: Image.FileFormat.Txz.Decode(inFile, outFile, GlobalSetting.Singleton.TxzFormat.GetTextureFormat); break;
                case 5: Image.FileFormat.PtxPS3.Decode(inFile, outFile); break;
                case 6: Image.FileFormat.PtxXBox360.Decode(inFile, outFile); break;
                case 7: Image.FileFormat.PtxPSV.Decode(inFile, outFile); break;
                default: throw new NotImplementedException();
            }
        }

        public static void EncodeImage(YFFile inFile, YFFile outFile, int format, int internalFormat, Plugin.Endian endian)
        {
            switch (format)
            {
                case 0: Image.FileFormat.PtxRsb.Encode(inFile, outFile, internalFormat, endian, GlobalSetting.Singleton.PtxRsbFormat.GetTextureFormat); break;
                case 1: Image.FileFormat.TexTV.Encode(inFile, outFile, internalFormat, GlobalSetting.Singleton.TexTVFormat.UseZlib, GlobalSetting.Singleton.TexTVFormat.GetTextureFormat); break;
                case 2: Image.FileFormat.Cdat.Encode(inFile, outFile, GlobalSetting.Singleton.CdatFormat.Cipher); break;
                case 3: Image.FileFormat.TexIOS.Encode(inFile, outFile, internalFormat, GlobalSetting.Singleton.TexIOSFormat.GetTextureFormat); break;
                case 4: Image.FileFormat.Txz.Encode(inFile, outFile, internalFormat, GlobalSetting.Singleton.TxzFormat.GetTextureFormat); break;
                case 5: Image.FileFormat.PtxPS3.Encode(inFile, outFile); break;
                case 6: Image.FileFormat.PtxXBox360.Encode(inFile, outFile); break;
                case 7: Image.FileFormat.PtxPSV.Encode(inFile, outFile); break;
                default: throw new NotImplementedException();
            }
        }

        public static void TranscodeReanim(YFFile inFile, YFFile outFile, int inFormat, int outFormat)
        {
            Reanim.Reanim trail = inFormat switch
            {
                0 => Reanim.PC.Decode(inFile),
                1 => Reanim.TV.Decode(inFile),
                2 => Reanim.Phone32.Decode(inFile, GlobalSetting.Singleton.ImageLabelConvertor.GetStringFromIndex),
                3 => Reanim.Phone64.Decode(inFile, GlobalSetting.Singleton.ImageLabelConvertor.GetStringFromIndex),
                4 => Reanim.GameConsole.Decode(inFile),
                5 => Reanim.WP.Decode(inFile),
                6 => Reanim.ReanimJson.Decode(inFile),
                7 => Reanim.RawXml.Decode(inFile),
                _ => throw new NotImplementedException()
            };
            switch (outFormat)
            {
                case 0: Reanim.PC.Encode(trail, outFile); break;
                case 1: Reanim.TV.Encode(trail, outFile); break;
                case 2: Reanim.Phone32.Encode(trail, outFile, GlobalSetting.Singleton.ImageLabelConvertor.GetIndexFromString); break;
                case 3: Reanim.Phone64.Encode(trail, outFile, GlobalSetting.Singleton.ImageLabelConvertor.GetIndexFromString); break;
                case 4: Reanim.GameConsole.Encode(trail, outFile); break;
                case 5: Reanim.WP.Encode(trail, outFile); break;
                case 6: Reanim.ReanimJson.Encode(trail, outFile); break;
                case 7: Reanim.RawXml.Encode(trail, outFile); break;
                default: throw new NotImplementedException();
            }
        }

        public static void TranscodeParticle(YFFile inFile, YFFile outFile, int inFormat, int outFormat)
        {
            Particle.Particle particle = inFormat switch
            {
                0 => Particle.PC.Decode(inFile),
                1 => Particle.TV.Decode(inFile),
                2 => Particle.Phone32.Decode(inFile, GlobalSetting.Singleton.ImageLabelConvertor.GetStringFromIndex),
                3 => Particle.Phone64.Decode(inFile, GlobalSetting.Singleton.ImageLabelConvertor.GetStringFromIndex),
                4 => Particle.GameConsole.Decode(inFile),
                5 => Particle.WP.Decode(inFile),
                6 => Particle.ParticleJson.Decode(inFile),
                7 => Particle.RawXml.Decode(inFile),
                _ => throw new NotImplementedException()
            };
            switch (outFormat)
            {
                case 0: Particle.PC.Encode(particle, outFile); break;
                case 1: Particle.TV.Encode(particle, outFile); break;
                case 2: Particle.Phone32.Encode(particle, outFile, GlobalSetting.Singleton.ImageLabelConvertor.GetIndexFromString); break;
                case 3: Particle.Phone64.Encode(particle, outFile, GlobalSetting.Singleton.ImageLabelConvertor.GetIndexFromString); break;
                case 4: Particle.GameConsole.Encode(particle, outFile); break;
                case 5: Particle.WP.Encode(particle, outFile); break;
                case 6: Particle.ParticleJson.Encode(particle, outFile); break;
                case 7: Particle.RawXml.Encode(particle, outFile); break;
                default: throw new NotImplementedException();
            }
        }

        public static void TranscodeTrail(YFFile inFile, YFFile outFile, int inFormat, int outFormat)
        {
            Trail.Trail trail = inFormat switch
            {
                0 => Trail.PC.Decode(inFile),
                1 => Trail.TV.Decode(inFile),
                2 => Trail.Phone32.Decode(inFile, GlobalSetting.Singleton.ImageLabelConvertor.GetStringFromIndex),
                3 => Trail.Phone64.Decode(inFile, GlobalSetting.Singleton.ImageLabelConvertor.GetStringFromIndex),
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
                case 2: Trail.Phone32.Encode(trail, outFile, GlobalSetting.Singleton.ImageLabelConvertor.GetIndexFromString); break;
                case 3: Trail.Phone64.Encode(trail, outFile, GlobalSetting.Singleton.ImageLabelConvertor.GetIndexFromString); break;
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

        public static void DecodeRton(YFFile inFile, YFFile outFile, int format)
        {
            switch (format)
            {
                case 0: Rton.Rton.Decode(inFile, outFile); break;
                case 1: Rton.Rton.DecodeAndDecrypt(inFile, outFile, GlobalSetting.Singleton.RtonCipher.Cipher); break;
                default: throw new NotImplementedException();
            }
        }

        public static void EncodeRton(YFFile inFile, YFFile outFile, int format)
        {
            switch (format)
            {
                case 0: Rton.Rton.Encode(inFile, outFile); break;
                case 1: Rton.Rton.EncodeAndEncrypt(inFile, outFile, GlobalSetting.Singleton.RtonCipher.Cipher); break;
                default: throw new NotImplementedException();
            }
        }
    }
}
