using System.Collections.Generic;

namespace PopStudio.Settings
{
    public class ImageLabelSetting
    {
        public Dictionary<int, string> ImageIndexToNameMap { get; set; }
        public Dictionary<string, int> ImageNameToIndexMap { get; set; }

        public void Init()
        {
            ImageIndexToNameMap ??= new Dictionary<int, string>();
            ImageNameToIndexMap ??= new Dictionary<string, int>();
        }

        public object GetStringFromIndex(int i)
        {
            lock (ImageIndexToNameMap)
            {
                if (ImageIndexToNameMap?.TryGetValue(i, out string s) is true)
                {
                    return s;
                }
                return i;
            }
        }

        public int GetIndexFromString(object o)
        {
            if (o is int i)
            {
                return i;
            }
            if (o is string str)
            {
                lock (ImageNameToIndexMap)
                {
                    if (ImageNameToIndexMap?.TryGetValue(str, out int v) is true)
                    {
                        return v;
                    }
                    if (int.TryParse(str, out int r))
                    {
                        return r;
                    }
                }
            }
            return -1;
        }
    }
}
