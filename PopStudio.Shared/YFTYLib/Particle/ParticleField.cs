using System.Text.Json.Serialization;

namespace PopStudio.Particle
{
    internal class ParticleField
    {
        [JsonPropertyName("FieldType")]
        public int? FieldType { get; set; }

        [JsonPropertyName("X")]
        public ParticleTrackNode[] XForJson { get => Check(X); set => X = value; }

        [JsonPropertyName("Y")]
        public ParticleTrackNode[] YForJson { get => Check(Y); set => Y = value; }

        [JsonIgnore]
        public ParticleTrackNode[] X { get; set; }

        [JsonIgnore]
        public ParticleTrackNode[] Y { get; set; }

        private ParticleTrackNode[] Check(ParticleTrackNode[] v)
            => (v is null || v.Length <= 0) ? null : v;
    }
}
