using System.Text.Encodings.Web;
using System.Text.Json;
using System.IO;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.PopAnim
{
    internal class PamJson
    {
        public static PopAnimInfo Decode(YFFile inFile)
        {
            using (Stream stream = inFile.OpenAsStream())
            {
                return JsonSerializer.Deserialize(
                    stream,
                    typeof(PopAnimInfo),
                    new PopAnimJsonContext(new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true,
                    })
                    ) as PopAnimInfo;
            }
        }

        public static void Encode(PopAnimInfo pam, YFFile outFile)
        {
            using (Stream stream = outFile.CreateAsStream())
            {
                JsonSerializer.Serialize(
                    stream,
                    pam,
                    typeof(PopAnimInfo),
                    new PopAnimJsonContext(new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        WriteIndented = true,
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                    })
                    );
            }
        }
    }
}
