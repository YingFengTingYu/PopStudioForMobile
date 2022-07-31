﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Buffers;
using System.IO;

namespace PopStudio.Image
{
    /// <summary>
    /// It is slower than GDIBitmap
    /// </summary>
    public class ImageBitmap : YFBitmap
    {
        public override int Width => m_width;
        public override int Height => m_height;

        readonly Image<Bgra32> m_image;
        readonly MemoryHandle m_handle;
        readonly int m_width;
        readonly int m_height;
        readonly bool nullhandle = false;

        public ImageBitmap()
        {
            nullhandle = true;
        }

        public ImageBitmap(int width, int height)
        {
            Configuration customConfig = Configuration.Default.Clone();
            customConfig.PreferContiguousImageBuffers = true;
            m_image = new Image<Bgra32>(customConfig, width, height);
            if (!m_image.DangerousTryGetSinglePixelMemory(out Memory<Bgra32> memory))
            {
                throw new Exception("This can only happen with multi-GB images or when PreferContiguousImageBuffers is not set to true.");
            }
            m_width = m_image.Width;
            m_height = m_image.Height;
            m_handle = memory.Pin();
        }

        public unsafe ImageBitmap(Stream stream)
        {
            Configuration customConfig = Configuration.Default.Clone();
            customConfig.PreferContiguousImageBuffers = true;
            m_image = SixLabors.ImageSharp.Image.Load<Bgra32>(customConfig, stream);
            if (!m_image.DangerousTryGetSinglePixelMemory(out Memory<Bgra32> memory))
            {
                throw new Exception("This can only happen with multi-GB images or when PreferContiguousImageBuffers is not set to true.");
            }
            m_width = m_image.Width;
            m_height = m_image.Height;
            m_handle = memory.Pin();
        }

        protected override YFBitmap InternalCreate(int width, int height) => new ImageBitmap(width, height);
        protected override YFBitmap InternalCreate(Stream stream) => new ImageBitmap(stream);
        /// <summary>
        /// BB GG RR AA
        /// </summary>
        /// <returns></returns>
        public override unsafe nint Pixels => new IntPtr(m_handle.Pointer);

        public override unsafe void Save(Stream stream)
        {
            m_image.SaveAsPng(stream, new PngEncoder
            {
                FilterMethod = PngFilterMethod.None,
                CompressionLevel = PngCompressionLevel.Level1
            });
        }

        public override void Dispose()
        {
            if (!nullhandle) m_handle.Dispose();
            m_image?.Dispose();
        }
    }
}