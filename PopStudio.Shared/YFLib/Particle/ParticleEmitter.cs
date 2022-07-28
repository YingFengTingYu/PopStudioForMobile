using System.Text.Json.Serialization;
using System.Text.Json;

namespace PopStudio.Particle
{
    internal class ParticleEmitter
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; } //<Name>

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
        public object Image { get; set; } //<Image>

        [JsonPropertyName("ImageResource")]
        public string ImagePath { get; set; }

        [JsonPropertyName("ImageCol")]
        public int? ImageCol { get; set; }

        [JsonPropertyName("ImageRow")]
        public int? ImageRow { get; set; }

        [JsonPropertyName("ImageFrames")]
        public int? ImageFrames { get; set; } //1

        [JsonPropertyName("Animated")]
        public int? Animated { get; set; }

        [JsonIgnore]
        public int ParticleFlags { get; set; }

        [JsonPropertyName("RandomLaunchSpin")]
        public bool RandomLaunchSpin
        {
            get => (ParticleFlags & 0b1) != 0;
            set
            {
                if (value)
                {
                    ParticleFlags |= 0b1;
                }
                else
                {
                    ParticleFlags &= ~0b1;
                }
            }
        }

        [JsonPropertyName("AlignLaunchSpin")]
        public bool AlignLaunchSpin
        {
            get => (ParticleFlags & 0b10) != 0;
            set
            {
                if (value)
                {
                    ParticleFlags |= 0b10;
                }
                else
                {
                    ParticleFlags &= ~0b10;
                }
            }
        }

        [JsonPropertyName("AlignToPixel")]
        public bool AlignToPixel
        {
            get => (ParticleFlags & 0b100) != 0;
            set
            {
                if (value)
                {
                    ParticleFlags |= 0b100;
                }
                else
                {
                    ParticleFlags &= ~0b100;
                }
            }
        }

        [JsonPropertyName("SystemLoops")]
        public bool SystemLoops
        {
            get => (ParticleFlags & 0b1000) != 0;
            set
            {
                if (value)
                {
                    ParticleFlags |= 0b1000;
                }
                else
                {
                    ParticleFlags &= ~0b1000;
                }
            }
        }

        [JsonPropertyName("ParticleLoops")]
        public bool ParticleLoops
        {
            get => (ParticleFlags & 0b10000) != 0;
            set
            {
                if (value)
                {
                    ParticleFlags |= 0b10000;
                }
                else
                {
                    ParticleFlags &= ~0b10000;
                }
            }
        }

        [JsonPropertyName("ParticlesDontFollow")]
        public bool ParticlesDontFollow
        {
            get => (ParticleFlags & 0b100000) != 0;
            set
            {
                if (value)
                {
                    ParticleFlags |= 0b100000;
                }
                else
                {
                    ParticleFlags &= ~0b100000;
                }
            }
        }

        [JsonPropertyName("RandomStartTime")]
        public bool RandomStartTime
        {
            get => (ParticleFlags & 0b1000000) != 0;
            set
            {
                if (value)
                {
                    ParticleFlags |= 0b1000000;
                }
                else
                {
                    ParticleFlags &= ~0b1000000;
                }
            }
        }

        [JsonPropertyName("DieIfOverloaded")]
        public bool DieIfOverloaded
        {
            get => (ParticleFlags & 0b10000000) != 0;
            set
            {
                if (value)
                {
                    ParticleFlags |= 0b10000000;
                }
                else
                {
                    ParticleFlags &= ~0b10000000;
                }
            }
        }

        [JsonPropertyName("Additive")]
        public bool Additive
        {
            get => (ParticleFlags & 0b100000000) != 0;
            set
            {
                if (value)
                {
                    ParticleFlags |= 0b100000000;
                }
                else
                {
                    ParticleFlags &= ~0b100000000;
                }
            }
        }

        [JsonPropertyName("FullScreen")]
        public bool FullScreen
        {
            get => (ParticleFlags & 0b1000000000) != 0;
            set
            {
                if (value)
                {
                    ParticleFlags |= 0b1000000000;
                }
                else
                {
                    ParticleFlags &= ~0b1000000000;
                }
            }
        }

        [JsonPropertyName("SoftwareOnly")]
        public bool SoftwareOnly
        {
            get => (ParticleFlags & 0b10000000000) != 0;
            set
            {
                if (value)
                {
                    ParticleFlags |= 0b10000000000;
                }
                else
                {
                    ParticleFlags &= ~0b10000000000;
                }
            }
        }

        [JsonPropertyName("HardwareOnly")]
        public bool HardwareOnly
        {
            get => (ParticleFlags & 0b100000000000) != 0;
            set
            {
                if (value)
                {
                    ParticleFlags |= 0b100000000000;
                }
                else
                {
                    ParticleFlags &= ~0b100000000000;
                }
            }
        }

        [JsonPropertyName("EmitterType")]
        public object EmitterTypeString
        {
            get => AsObject(EmitterType);
            set => EmitterType = FromObject(value);
        }

        [JsonIgnore]
        public int? EmitterType { get; set; } //1

        [JsonPropertyName("OnDuration")]
        public string OnDuration { get; set; } //<OnDuration>

        [JsonPropertyName("SystemDuration")]
        public ParticleTrackNode[] SystemDurationForJson { get => Check(SystemDuration); set => SystemDuration = value; } //<SystemDuration>

        [JsonPropertyName("CrossFadeDuration")]
        public ParticleTrackNode[] CrossFadeDurationForJson { get => Check(CrossFadeDuration); set => CrossFadeDuration = value; } //<CrossFadeDuration>

        [JsonPropertyName("SpawnRate")]
        public ParticleTrackNode[] SpawnRateForJson { get => Check(SpawnRate); set => SpawnRate = value; }//<SpawnRate>

        [JsonPropertyName("SpawnMinActive")]
        public ParticleTrackNode[] SpawnMinActiveForJson { get => Check(SpawnMinActive); set => SpawnMinActive = value; } //<SpawnMinActive>

        [JsonPropertyName("SpawnMaxActive")]
        public ParticleTrackNode[] SpawnMaxActiveForJson { get => Check(SpawnMaxActive); set => SpawnMaxActive = value; } //<SpawnMaxActive>

        [JsonPropertyName("SpawnMaxLaunched")]
        public ParticleTrackNode[] SpawnMaxLaunchedForJson { get => Check(SpawnMaxLaunched); set => SpawnMaxLaunched = value; } //<SpawnMaxLaunched>

        [JsonPropertyName("EmitterRadius")]
        public ParticleTrackNode[] EmitterRadiusForJson { get => Check(EmitterRadius); set => EmitterRadius = value; } //<EmitterRadius>

        [JsonPropertyName("EmitterOffsetX")]
        public ParticleTrackNode[] EmitterOffsetXForJson { get => Check(EmitterOffsetX); set => EmitterOffsetX = value; } //<EmitterOffsetX>

        [JsonPropertyName("EmitterOffsetY")]
        public ParticleTrackNode[] EmitterOffsetYForJson { get => Check(EmitterOffsetY); set => EmitterOffsetY = value; } //<EmitterOffsetY>

        [JsonPropertyName("EmitterBoxX")]
        public ParticleTrackNode[] EmitterBoxXForJson { get => Check(EmitterBoxX); set => EmitterBoxX = value; } //<EmitterBoxX>

        [JsonPropertyName("EmitterBoxY")]
        public ParticleTrackNode[] EmitterBoxYForJson { get => Check(EmitterBoxY); set => EmitterBoxY = value; } //<EmitterBoxY>

        [JsonPropertyName("EmitterPath")]
        public ParticleTrackNode[] EmitterPathForJson { get => Check(EmitterPath); set => EmitterPath = value; } //<EmitterPath>

        [JsonPropertyName("EmitterSkewX")]
        public ParticleTrackNode[] EmitterSkewXForJson { get => Check(EmitterSkewX); set => EmitterSkewX = value; } //<EmitterSkewX>

        [JsonPropertyName("EmitterSkewY")]
        public ParticleTrackNode[] EmitterSkewYForJson { get => Check(EmitterSkewY); set => EmitterSkewY = value; }//<EmitterSkewY>

        [JsonPropertyName("ParticleDuration")]
        public ParticleTrackNode[] ParticleDurationForJson { get => Check(ParticleDuration); set => ParticleDuration = value; } //<ParticleDuration>

        [JsonPropertyName("SystemRed")]
        public ParticleTrackNode[] SystemRedForJson { get => Check(SystemRed); set => SystemRed = value; } //<SystemRed>

        [JsonPropertyName("SystemGreen")]
        public ParticleTrackNode[] SystemGreenForJson { get => Check(SystemGreen); set => SystemGreen = value; } //<SystemGreen>

        [JsonPropertyName("SystemBlue")]
        public ParticleTrackNode[] SystemBlueForJson { get => Check(SystemBlue); set => SystemBlue = value; } //<SystemBlue>

        [JsonPropertyName("SystemAlpha")]
        public ParticleTrackNode[] SystemAlphaForJson { get => Check(SystemAlpha); set => SystemAlpha = value; } //<SystemAlpha>

        [JsonPropertyName("SystemBrightness")]
        public ParticleTrackNode[] SystemBrightnessForJson { get => Check(SystemBrightness); set => SystemBrightness = value; } //<SystemBrightness>

        [JsonPropertyName("LaunchSpeed")]
        public ParticleTrackNode[] LaunchSpeedForJson { get => Check(LaunchSpeed); set => LaunchSpeed = value; } //<LaunchSpeed>

        [JsonPropertyName("LaunchAngle")]
        public ParticleTrackNode[] LaunchAngleForJson { get => Check(LaunchAngle); set => LaunchAngle = value; } //<LaunchAngle>

        [JsonPropertyName("Field")]
        public ParticleField[] FieldForJson { get => Check(Field); set => Field = value; } //<Field>

        [JsonPropertyName("SystemField")]
        public ParticleField[] SystemFieldForJson { get => Check(SystemField); set => SystemField = value; } //<SystemField>

        [JsonPropertyName("ParticleRed")]
        public ParticleTrackNode[] ParticleRedForJson { get => Check(ParticleRed); set => ParticleRed = value; } //<ParticleRed>

        [JsonPropertyName("ParticleGreen")]
        public ParticleTrackNode[] ParticleGreenForJson { get => Check(ParticleGreen); set => ParticleGreen = value; } //<ParticleGreen>

        [JsonPropertyName("ParticleBlue")]
        public ParticleTrackNode[] ParticleBlueForJson { get => Check(ParticleBlue); set => ParticleBlue = value; } //<ParticleBlue>

        [JsonPropertyName("ParticleAlpha")]
        public ParticleTrackNode[] ParticleAlphaForJson { get => Check(ParticleAlpha); set => ParticleAlpha = value; } //<ParticleAlpha>

        [JsonPropertyName("ParticleBrightness")]
        public ParticleTrackNode[] ParticleBrightnessForJson { get => Check(ParticleBrightness); set => ParticleBrightness = value; } //<ParticleBrightness>

        [JsonPropertyName("ParticleSpinAngle")]
        public ParticleTrackNode[] ParticleSpinAngleForJson { get => Check(ParticleSpinAngle); set => ParticleSpinAngle = value; } //<ParticleSpinAngle>

        [JsonPropertyName("ParticleSpinSpeed")]
        public ParticleTrackNode[] ParticleSpinSpeedForJson { get => Check(ParticleSpinSpeed); set => ParticleSpinSpeed = value; } //<ParticleSpinSpeed>

        [JsonPropertyName("ParticleScale")]
        public ParticleTrackNode[] ParticleScaleForJson { get => Check(ParticleScale); set => ParticleScale = value; } //<ParticleScale>

        [JsonPropertyName("ParticleStretch")]
        public ParticleTrackNode[] ParticleStretchForJson { get => Check(ParticleStretch); set => ParticleStretch = value; } //<ParticleStretch>

        [JsonPropertyName("CollisionReflect")]
        public ParticleTrackNode[] CollisionReflectForJson { get => Check(CollisionReflect); set => CollisionReflect = value; } //<CollisionReflect>

        [JsonPropertyName("CollisionSpin")]
        public ParticleTrackNode[] CollisionSpinForJson { get => Check(CollisionSpin); set => CollisionSpin = value; } //<CollisionSpin>

        [JsonPropertyName("ClipTop")]
        public ParticleTrackNode[] ClipTopForJson { get => Check(ClipTop); set => ClipTop = value; } //<ClipTop>

        [JsonPropertyName("ClipBottom")]
        public ParticleTrackNode[] ClipBottomForJson { get => Check(ClipBottom); set => ClipBottom = value; } //<ClipBottom>

        [JsonPropertyName("ClipLeft")]
        public ParticleTrackNode[] ClipLeftForJson { get => Check(ClipLeft); set => ClipLeft = value; } //<ClipLeft>

        [JsonPropertyName("ClipRight")]
        public ParticleTrackNode[] ClipRightForJson { get => Check(ClipRight); set => ClipRight = value; } //<ClipRight>

        [JsonPropertyName("AnimationRate")]
        public ParticleTrackNode[] AnimationRateForJson { get => Check(AnimationRate); set => AnimationRate = value; } //<AnimationRate>

        [JsonIgnore]
        public ParticleTrackNode[] SystemDuration { get; set; } //<SystemDuration>

        [JsonIgnore]
        public ParticleTrackNode[] CrossFadeDuration { get; set; } //<CrossFadeDuration>

        [JsonIgnore]
        public ParticleTrackNode[] SpawnRate { get; set; }//<SpawnRate>

        [JsonIgnore]
        public ParticleTrackNode[] SpawnMinActive { get; set; } //<SpawnMinActive>

        [JsonIgnore]
        public ParticleTrackNode[] SpawnMaxActive { get; set; } //<SpawnMaxActive>

        [JsonIgnore]
        public ParticleTrackNode[] SpawnMaxLaunched { get; set; } //<SpawnMaxLaunched>

        [JsonIgnore]
        public ParticleTrackNode[] EmitterRadius { get; set; } //<EmitterRadius>

        [JsonIgnore]
        public ParticleTrackNode[] EmitterOffsetX { get; set; } //<EmitterOffsetX>

        [JsonIgnore]
        public ParticleTrackNode[] EmitterOffsetY { get; set; } //<EmitterOffsetY>

        [JsonIgnore]
        public ParticleTrackNode[] EmitterBoxX { get; set; } //<EmitterBoxX>

        [JsonIgnore]
        public ParticleTrackNode[] EmitterBoxY { get; set; } //<EmitterBoxY>

        [JsonIgnore]
        public ParticleTrackNode[] EmitterPath { get; set; } //<EmitterPath>

        [JsonIgnore]
        public ParticleTrackNode[] EmitterSkewX { get; set; } //<EmitterSkewX>

        [JsonIgnore]
        public ParticleTrackNode[] EmitterSkewY { get; set; } //<EmitterSkewY>

        [JsonIgnore]
        public ParticleTrackNode[] ParticleDuration { get; set; } //<ParticleDuration>

        [JsonIgnore]
        public ParticleTrackNode[] SystemRed { get; set; } //<SystemRed>

        [JsonIgnore]
        public ParticleTrackNode[] SystemGreen { get; set; } //<SystemGreen>

        [JsonIgnore]
        public ParticleTrackNode[] SystemBlue { get; set; } //<SystemBlue>

        [JsonIgnore]
        public ParticleTrackNode[] SystemAlpha { get; set; } //<SystemAlpha>

        [JsonIgnore]
        public ParticleTrackNode[] SystemBrightness { get; set; } //<SystemBrightness>

        [JsonIgnore]
        public ParticleTrackNode[] LaunchSpeed { get; set; } //<LaunchSpeed>

        [JsonIgnore]
        public ParticleTrackNode[] LaunchAngle { get; set; } //<LaunchAngle>

        [JsonIgnore]
        public ParticleField[] Field { get; set; } //<Field>

        [JsonIgnore]
        public ParticleField[] SystemField { get; set; } //<SystemField>

        [JsonIgnore]
        public ParticleTrackNode[] ParticleRed { get; set; } //<ParticleRed>

        [JsonIgnore]
        public ParticleTrackNode[] ParticleGreen { get; set; } //<ParticleGreen>

        [JsonIgnore]
        public ParticleTrackNode[] ParticleBlue { get; set; } //<ParticleBlue>

        [JsonIgnore]
        public ParticleTrackNode[] ParticleAlpha { get; set; } //<ParticleAlpha>

        [JsonIgnore]
        public ParticleTrackNode[] ParticleBrightness { get; set; } //<ParticleBrightness>

        [JsonIgnore]
        public ParticleTrackNode[] ParticleSpinAngle { get; set; } //<ParticleSpinAngle>

        [JsonIgnore]
        public ParticleTrackNode[] ParticleSpinSpeed { get; set; } //<ParticleSpinSpeed>

        [JsonIgnore]
        public ParticleTrackNode[] ParticleScale { get; set; } //<ParticleScale>

        [JsonIgnore]
        public ParticleTrackNode[] ParticleStretch { get; set; } //<ParticleStretch>

        [JsonIgnore]
        public ParticleTrackNode[] CollisionReflect { get; set; } //<CollisionReflect>

        [JsonIgnore]
        public ParticleTrackNode[] CollisionSpin { get; set; } //<CollisionSpin>

        [JsonIgnore]
        public ParticleTrackNode[] ClipTop { get; set; } //<ClipTop>

        [JsonIgnore]
        public ParticleTrackNode[] ClipBottom { get; set; } //<ClipBottom>

        [JsonIgnore]
        public ParticleTrackNode[] ClipLeft { get; set; } //<ClipLeft>

        [JsonIgnore]
        public ParticleTrackNode[] ClipRight { get; set; } //<ClipRight>

        [JsonIgnore]
        public ParticleTrackNode[] AnimationRate { get; set; } //<AnimationRate>

        private ParticleField[] Check(ParticleField[] v)
            => (v is null || v.Length <= 0) ? null : v;

        private ParticleTrackNode[] Check(ParticleTrackNode[] v)
            => (v is null || v.Length <= 0) ? null : v;

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
                    "Circle" => 0,
                    "Box" => 1,
                    "BoxPath" => 2,
                    "CirclePath" => 3,
                    "CircleEvenSpacing" => 4,
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
            0 => "Circle",
            1 => "Box",
            2 => "BoxPath",
            3 => "CirclePath",
            4 => "CircleEvenSpacing",
            _ => v
        };
    }
}
