using System.Text.Json.Serialization;

namespace PopStudio.Reanim
{
    internal class ReanimTrack
    {
        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("transforms")]
        public ReanimTransform[] transforms { get; set; }
    }
}
