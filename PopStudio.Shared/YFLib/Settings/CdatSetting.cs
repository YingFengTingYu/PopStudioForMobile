using PopStudio.Plugin;
using System.Collections.Generic;

namespace PopStudio.Settings
{
    public class CdatSetting
    {
        public string Cipher { get; set; }

        public void Init()
        {
            Cipher ??= "AS23DSREPLKL335KO4439032N8345NF";
        }
    }
}
