using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Digests;
using System.Text;

namespace PopStudio.Plugin
{
    internal static class RijndaelHelper
    {
        public static byte[] GetKey(string str)
        {
            byte[] src = Encoding.UTF8.GetBytes(str);
            MD5Digest digest = new MD5Digest();
            digest.BlockUpdate(src, 0, src.Length);
            byte[] md5Bytes = new byte[digest.GetDigestSize()];
            digest.DoFinal(md5Bytes, 0);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in md5Bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public static byte[] Encrypt(byte[] plainTextBytes, byte[] keyBytes, byte[] ivStringBytes, IBlockCipherPadding padding)
        {
            var engine = new RijndaelEngine(ivStringBytes.Length << 3);
            var blockCipher = new CbcBlockCipher(engine);
            var cipher = new PaddedBufferedBlockCipher(blockCipher, padding);
            var keyParam = new KeyParameter(keyBytes);
            var keyParamWithIV = new ParametersWithIV(keyParam, ivStringBytes, 0, ivStringBytes.Length);
            cipher.Init(true, keyParamWithIV);
            var comparisonBytes = new byte[cipher.GetOutputSize(plainTextBytes.Length)];
            var length = cipher.ProcessBytes(plainTextBytes, comparisonBytes, 0);
            cipher.DoFinal(comparisonBytes, length);
            return comparisonBytes;
        }

        public static byte[] Decrypt(byte[] cipherTextBytes, byte[] keyBytes, byte[] ivStringBytes, IBlockCipherPadding padding)
        {
            var engine = new RijndaelEngine(ivStringBytes.Length << 3);
            var blockCipher = new CbcBlockCipher(engine);
            var cipher = new PaddedBufferedBlockCipher(blockCipher, padding);
            var keyParam = new KeyParameter(keyBytes);
            var keyParamWithIV = new ParametersWithIV(keyParam, ivStringBytes, 0, ivStringBytes.Length);
            cipher.Init(false, keyParamWithIV);
            var comparisonBytes = new byte[cipher.GetOutputSize(cipherTextBytes.Length)];
            var length = cipher.ProcessBytes(cipherTextBytes, comparisonBytes, 0);
            cipher.DoFinal(comparisonBytes, length);
            return comparisonBytes;
        }
    }
}