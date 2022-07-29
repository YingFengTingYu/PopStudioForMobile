using System.Text.Json.Serialization;

namespace PopStudio.Reanim
{
    internal class Reanim
    {
        [JsonPropertyName("doScale")]
        public sbyte? doScale { get; set; } //for wp

        [JsonPropertyName("fps")]
        public float fps { get; set; }

        [JsonPropertyName("tracks")]
        public ReanimTrack[] tracks { get; set; }
    }
}
