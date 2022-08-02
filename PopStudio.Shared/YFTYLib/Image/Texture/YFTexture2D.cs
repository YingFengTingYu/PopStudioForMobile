using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PopStudio.Image.Texture
{
    public unsafe class YFTexture2D
    {
        public int Width = 0;
        public int Height = 0;
        public int Check = 0;
        public TextureFormat TexFormat = TextureFormat.NONE;
        public byte[] TexData;
        private Dictionary<string, object> info;

        public T GetInfo<T>(string key) =>
            (InternalGetInfo(key) is T ans) ? ans : default;

        private object InternalGetInfo(string key) =>
            (info is not null && info.TryGetValue(key, out object ans)) ? ans : null;

        public void SetInfo<T>(string key, T value) => InternalSetInfo(key, value);

        private void InternalSetInfo(string key, object value)
        {
            info ??= new Dictionary<string, object>();
            if (!info.TryAdd(key, value))
            {
                info[key] = value;
            }
        }
    }
}
