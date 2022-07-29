using System;
using PopStudio.Settings;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio
{
    public static class YFAPI
    {
        public static void TranscodeReanim(YFFile inFile, YFFile outFile, int inFormat, int outFormat, ImageLabelSetting setting)
        {
            Reanim.Reanim trail = inFormat switch
            {
                0 => Reanim.PC.Decode(inFile),
                1 => Reanim.TV.Decode(inFile),
                2 => Reanim.Phone32.Decode(inFile, setting.GetStringFromIndex),
                3 => Reanim.Phone64.Decode(inFile, setting.GetStringFromIndex),
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
                case 2: Reanim.Phone32.Encode(trail, outFile, setting.GetIndexFromString); break;
                case 3: Reanim.Phone64.Encode(trail, outFile, setting.GetIndexFromString); break;
                case 4: Reanim.GameConsole.Encode(trail, outFile); break;
                case 5: Reanim.WP.Encode(trail, outFile); break;
                case 6: Reanim.ReanimJson.Encode(trail, outFile); break;
                case 7: Reanim.RawXml.Encode(trail, outFile); break;
                default: throw new NotImplementedException();
            }
        }

        public static void TranscodeParticle(YFFile inFile, YFFile outFile, int inFormat, int outFormat, ImageLabelSetting setting)
        {
            Particle.Particle particle = inFormat switch
            {
                0 => Particle.PC.Decode(inFile),
                1 => Particle.TV.Decode(inFile),
                2 => Particle.Phone32.Decode(inFile, setting.GetStringFromIndex),
                3 => Particle.Phone64.Decode(inFile, setting.GetStringFromIndex),
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
                case 2: Particle.Phone32.Encode(particle, outFile, setting.GetIndexFromString); break;
                case 3: Particle.Phone64.Encode(particle, outFile, setting.GetIndexFromString); break;
                case 4: Particle.GameConsole.Encode(particle, outFile); break;
                case 5: Particle.WP.Encode(particle, outFile); break;
                case 6: Particle.ParticleJson.Encode(particle, outFile); break;
                case 7: Particle.RawXml.Encode(particle, outFile); break;
                default: throw new NotImplementedException();
            }
        }

        public static void TranscodeTrail(YFFile inFile, YFFile outFile, int inFormat, int outFormat, ImageLabelSetting setting)
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

        public static void DecodeRton(YFFile inFile, YFFile outFile, int format, RtonSetting setting)
        {
            switch (format)
            {
                case 0: Rton.Rton.Decode(inFile, outFile); break;
                case 1: Rton.Rton.DecodeAndDecrypt(inFile, outFile, setting.Cipher); break;
                default: throw new NotImplementedException();
            }
        }

        public static void EncodeRton(YFFile inFile, YFFile outFile, int format, RtonSetting setting)
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
