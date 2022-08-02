using System.Text.Json.Serialization;

namespace PopStudio.Trail
{
    internal class TrailTrackNode
    {
        [JsonPropertyName("Time")]
        public float Time { get; set; }

        [JsonPropertyName("LowValue")]
        public float? LowValue { get; set; }

        [JsonPropertyName("HighValue")]
        public float? HighValue { get; set; }

        [JsonPropertyName("CurveType")]
        public object CurveTypeString
        {
            get => AsObject(CurveType);
            set => CurveType = FromObject(value);
        }

        [JsonPropertyName("Distribution")]
        public object DistributionString
        {
            get => AsObject(Distribution);
            set => Distribution = FromObject(value);
        }

        [JsonIgnore]
        public int? CurveType { get; set; }

        [JsonIgnore]
        public int? Distribution { get; set; }

        private static int? FromObject(object o)
        {
            if (o is null)
            {
                return null;
            }
            if (o is string str)
            {
                int? ans = str switch
                {
                    "Constant" => 0,
                    "Linear" => 1,
                    "EaseIn" => 2,
                    "EaseOut" => 3,
                    "EaseInOut" => 4,
                    "EaseInOutWeak" => 5,
                    "FastInOut" => 6,
                    "FastInOutWeak" => 7,
                    "WeakFastInOut" => 8,
                    "Bounce" => 9,
                    "BounceFastMiddle" => 10,
                    "BounceSlowMiddle" => 11,
                    "SinWave" => 12,
                    "EaseSinWave" => 13,
                    _ => null
                };
                if (ans is null && int.TryParse(str, out int m))
                {
                    ans = m;
                }
                return ans;
            }
            if (o is int i)
            {
                return i;
            }
            return null;
        }

        private static object AsObject(int? v) => v switch
        {
            null => null,
            0 => "Constant",
            1 => "Linear",
            2 => "EaseIn",
            3 => "EaseOut",
            4 => "EaseInOut",
            5 => "EaseInOutWeak",
            6 => "FastInOut",
            7 => "FastInOutWeak",
            8 => "WeakFastInOut",
            9 => "Bounce",
            10 => "BounceFastMiddle",
            11 => "BounceSlowMiddle",
            12 => "SinWave",
            13 => "EaseSinWave",
            _ => v
        };
    }
}
