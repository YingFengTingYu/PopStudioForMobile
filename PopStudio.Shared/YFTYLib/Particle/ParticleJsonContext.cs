using System.Text.Json.Serialization;

namespace PopStudio.Particle
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(Particle))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(int))]
    internal partial class ParticleJsonContext : JsonSerializerContext
    {
    }
}
