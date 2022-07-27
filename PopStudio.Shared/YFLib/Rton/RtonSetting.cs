using System;
using System.Collections.Generic;
using System.Text;

namespace PopStudio.Rton
{
    public class RtonSetting
    {
        public string Cipher { get; set; }

        public RtonSetting()
        {

        }

        public RtonSetting(string s)
        {
            Cipher = s;
        }
    }
}
