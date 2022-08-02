using System.Text.Json.Serialization;

namespace PopStudio.Particle
{
    internal class Particle
    {
        [JsonIgnore]
        public ParticleEmitter[] Emitters { get; set; }

        [JsonPropertyName("Emitters")]
        public ParticleEmitter[] EmittersForJson
        {
            get => (Emitters is null || Emitters.Length <= 0) ? null : Emitters;
            set => Emitters = value;
        }
    }
}