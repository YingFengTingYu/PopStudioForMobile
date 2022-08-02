using System;

namespace PopStudio.PopAnim
{
    [Flags]
    internal enum FrameFlags : byte
    {
        Removes = 1,
        Adds = 2,
        Moves = 4,
        FrameName = 8,
        Stop = 16,
        Commands = 32
    }
}
