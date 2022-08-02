using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.Particle
{
    internal class ParticleJson
    {
        public static Particle Decode(YFFile inFile)
        {
            using (Stream stream = inFile.OpenAsStream())
            {
                return JsonSerializer.Deserialize(
                    stream,
                    typeof(Particle),
                    new ParticleJsonContext(new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true,
                    })
                    ) as Particle;
            }
        }

        public static void Encode(Particle particles, YFFile outFile)
        {
            using (Stream stream = outFile.CreateAsStream())
            {
                JsonSerializer.Serialize(
                    stream,
                    particles,
                    typeof(Particle),
                    new ParticleJsonContext(new JsonSerializerOptions
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