using System.Runtime.InteropServices;

namespace PopStudio.Image
{
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public unsafe struct YFColor
    {
        public static readonly YFColor Empty = new YFColor((byte)0, (byte)0, (byte)0, (byte)0);

        public static readonly YFColor Black = new YFColor((byte)0, (byte)0, (byte)0, (byte)0xFF);

        [FieldOffset(0)]
        public byte Blue;

        [FieldOffset(1)]
        public byte Green;

        [FieldOffset(2)]
        public byte Red;

        [FieldOffset(3)]
        public byte Alpha;

        public YFColor(byte red, byte green, byte blue, byte alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        static byte ToByte(int v)
        {
            if (v >= 255) return 255;
            if (v <= 0) return 0;
            return (byte)v;
        }

        static byte ToByte(uint v)
        {
            if (v >= 255) return 255;
            return (byte)v;
        }

        public YFColor(int red, int green, int blue, int alpha)
        {
            Red = ToByte(red);
            Green = ToByte(green);
            Blue = ToByte(blue);
            Alpha = ToByte(alpha);
        }

        public YFColor(uint red, uint green, uint blue, uint alpha)
        {
            Red = ToByte(red);
            Green = ToByte(green);
            Blue = ToByte(blue);
            Alpha = ToByte(alpha);
        }

        public YFColor(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = 255;
        }

        public YFColor(int red, int green, int blue)
        {
            Red = ToByte(red);
            Green = ToByte(green);
            Blue = ToByte(blue);
            Alpha = 255;
        }

        public YFColor(uint red, uint green, uint blue)
        {
            Red = ToByte(red);
            Green = ToByte(green);
            Blue = ToByte(blue);
            Alpha = 255;
        }

        public byte GetLuminance()
        {
            return (byte)((77 * Red + 150 * Green + 28 * Blue) / 255);
        }

        public void SetLuminance(byte l)
        {
            Red = l;
            Green = l;
            Blue = l;
        }

        public byte GetChannel(int i)
        {
            return i switch
            {
                0 => Red,
                1 => Green,
                2 => Blue,
                3 => Alpha,
                _ => 0
            };
        }

        public void SetChannel(int i, byte value)
        {
            switch (i)
            {
                case 0:
                    Blue = value;
                    break;
                case 1:
                    Green = value;
                    break;
                case 2:
                    Red = value;
                    break;
                case 3:
                    Alpha = value;
                    break;
            }
        }

        public override string ToString()
        {
            return $"#{Alpha:x2}{Red:x2}{Green:x2}{Blue:x2}";
        }

        public static explicit operator YFColor(uint color)
        {
            return *(YFColor*)&color;
        }

        public static explicit operator uint(YFColor color)
        {
            return *(uint*)&color;
        }
    }
}
