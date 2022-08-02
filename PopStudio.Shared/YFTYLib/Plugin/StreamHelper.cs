using System.IO;

namespace PopStudio.Plugin
{
    public static class StreamHelper
    {
        public static void StaticCopyTo(this Stream src, Stream s, byte[] array)
        {
            int count;
            while ((count = src.Read(array, 0, array.Length)) != 0)
            {
                s.Write(array, 0, count);
            }
        }
    }
}
