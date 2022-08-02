using System.Text.Json;
using System.Text.Json.Serialization;

namespace PopStudio.Reanim
{
    internal class ReanimTransform
    {
        [JsonPropertyName("x")]
        public float? x { get; set; }

        [JsonPropertyName("y")]
        public float? y { get; set; }

        [JsonPropertyName("kx")]
        public float? kx { get; set; }

        [JsonPropertyName("ky")]
        public float? ky { get; set; }

        [JsonPropertyName("sx")]
        public float? sx { get; set; }

        [JsonPropertyName("sy")]
        public float? sy { get; set; }

        [JsonPropertyName("f")]
        public float? f { get; set; }

        [JsonPropertyName("a")]
        public float? a { get; set; }

        [JsonPropertyName("i")]
        public object ImageForJson
        {
            get => i;
            set
            {
                if (value is JsonElement j)
                {
                    if (j.ValueKind == JsonValueKind.Number)
                    {
                        i = j.GetInt32();
                    }
                    else
                    {
                        i = j.GetString();
                    }
                }
                else
                {
                    i = value;
                }
            }
        }

        [JsonIgnore]
        public object i { get; set; }

        [JsonPropertyName("resource")]
        public string iPath { get; set; } //for Android TV version

        [JsonPropertyName("i2")]
        public string i2 { get; set; } //for Android TV version

        [JsonPropertyName("resource2")]
        public string i2Path { get; set; } //for Android TV version

        [JsonPropertyName("font")]
        public string font { get; set; }

        [JsonPropertyName("text")]
        public string text { get; set; }
    }
}
