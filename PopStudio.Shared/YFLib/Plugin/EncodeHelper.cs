using PopStudio.PlatformAPI;
using System.Text;

namespace PopStudio.Plugin
{
    internal static class EncodeHelper
    {
        public static Encoding ANSI => YFString.GetString("Language_Type") switch
        {
            "简体中文" => Gb2312,
            "English" => Latin1,
            _ => Latin1,
        };

        static readonly Encoding Latin1;
        static readonly Encoding Gb2312;

        static EncodeHelper()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Gb2312 = Encoding.GetEncoding("GB2312");
            Latin1 = Encoding.Latin1;
        }
    }
}
