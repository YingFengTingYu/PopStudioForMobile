using System.Text.Json;
using System.Text.Json.Serialization;

namespace PopStudio.Trail
{
    internal class Trail
    {
        [JsonPropertyName("MaxPoints")]
        public int? MaxPoints { get; set; }

        [JsonPropertyName("MinPointDistance")]
        public float? MinPointDistance { get; set; }

        [JsonIgnore]
        public int TrailFlags { get; set; }

        [JsonPropertyName("Loops")]
        public bool Loops
        {
            get => (TrailFlags & 0b1) != 0;
            set
            {
                if (value)
                {
                    TrailFlags |= 0b1;
                }
                else
                {
                    TrailFlags &= ~0b1;
                }
            }
        }

        [JsonPropertyName("Image")]
        public object ImageForJson
        {
            get => Image;
            set
            {
                if (value is JsonElement j)
                {
                    if (j.ValueKind == JsonValueKind.Number)
                    {
                        Image = j.GetInt32();
                    }
                    else
                    {
                        Image = j.GetString();
                    }
                }
                else
                {
                    Image = value;
                }
            }
        }

        [JsonIgnore]
        public object Image { get; set; }

        [JsonPropertyName("ImageResource")]
        public string ImageResource { get; set; }

        [JsonPropertyName("WidthOverLength")]
        public TrailTrackNode[] WidthOverLengthForJson
        {
            get => Check(WidthOverLength);
            set => WidthOverLength = value;
        }

        [JsonPropertyName("WidthOverTime")]
        public TrailTrackNode[] WidthOverTimeForJson
        {
            get => Check(WidthOverTime);
            set => WidthOverTime = value;
        }

        [JsonPropertyName("AlphaOverLength")]
        public TrailTrackNode[] AlphaOverLengthForJson
        {
            get => Check(AlphaOverLength);
            set => AlphaOverLength = value;
        }

        [JsonPropertyName("AlphaOverTime")]
        public TrailTrackNode[] AlphaOverTimeForJson
        {
            get => Check(AlphaOverTime);
            set => AlphaOverTime = value;
        }

        [JsonPropertyName("TrailDuration")]
        public TrailTrackNode[] TrailDurationForJson
        {
            get => Check(TrailDuration);
            set => TrailDuration = value;
        }

        [JsonIgnore]
        public TrailTrackNode[] WidthOverLength { get; set; }

        [JsonIgnore]
        public TrailTrackNode[] WidthOverTime { get; set; }

        [JsonIgnore]
        public TrailTrackNode[] AlphaOverLength { get; set; }

        [JsonIgnore]
        public TrailTrackNode[] AlphaOverTime { get; set; }

        [JsonIgnore]
        public TrailTrackNode[] TrailDuration { get; set; }

        private TrailTrackNode[] Check(TrailTrackNode[] v)
            => (v is null || v.Length <= 0) ? null : v;
    }
}
