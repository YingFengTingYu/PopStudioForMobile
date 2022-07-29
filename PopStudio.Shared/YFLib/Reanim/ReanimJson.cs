using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.Reanim
{
    internal static class ReanimJson
    {
        public static void Encode(Reanim reanim, YFFile outFile)
        {
            using (Stream stream = outFile.CreateAsStream())
            {
                JsonSerializer.Serialize(
                    stream,
                    reanim,
                    typeof(Reanim),
                    new ReanimJsonContext(new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        WriteIndented = true,
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                    })
                    );
            }
        }

        public static Reanim Decode(YFFile inFile)
        {
            using (Stream stream = inFile.OpenAsStream())
            {
                return JsonSerializer.Deserialize(
                    stream,
                    typeof(Reanim),
                    new ReanimJsonContext(new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true,
                    })
                    ) as Reanim;
            }
        }
    }
}