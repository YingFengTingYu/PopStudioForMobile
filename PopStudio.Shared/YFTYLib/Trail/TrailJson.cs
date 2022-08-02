using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.Trail
{
    internal class TrailJson
    {
        public static Trail Decode(YFFile inFile)
        {
            using (Stream stream = inFile.OpenAsStream())
            {
                return JsonSerializer.Deserialize(
                    stream,
                    typeof(Trail),
                    new TrailJsonContext(new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true,
                    })
                    ) as Trail;
            }
        }

        public static void Encode(Trail trail, YFFile outFile)
        {
            using (Stream stream = outFile.CreateAsStream())
            {
                JsonSerializer.Serialize(
                    stream,
                    trail,
                    typeof(Trail),
                    new TrailJsonContext(new JsonSerializerOptions
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
