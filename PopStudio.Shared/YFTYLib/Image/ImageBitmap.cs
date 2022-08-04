#if SKIASHARP
using SkiaSharp;
#else
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Buffers;
#endif
using System.IO;

namespace PopStudio.Image
{
    /// <summary>
    /// It is slower than GDIBitmap
    /// </summary>
    public class ImageBitmap : YFBitmap
    {
#if SKIASHARP
        public override int Width => m_width;
        public override int Height => m_height;

        readonly SKBitmap m_bitmap;
        readonly int m_width;
        readonly int m_height;

        public ImageBitmap()
        {
        }

        public ImageBitmap(int width, int height)
        {
            m_bitmap = new SKBitmap(width, height, SKColorType.Bgra8888, SKAlphaType.Unpremul);
            m_width = m_bitmap.Width;
            m_height = m_bitmap.Height;
        }

        public ImageBitmap(Stream stream)
        {
            using (SKCodec sKCodec = SKCodec.Create(stream))
            {
                m_bitmap = SKBitmap.Decode(sKCodec, new SKImageInfo
                {
                    ColorType = SKColorType.Bgra8888,
                    AlphaType = SKAlphaType.Unpremul,
                    ColorSpace = null,
                    Width = sKCodec.Info.Width,
                    Height = sKCodec.Info.Height
                });
            }
            m_width = m_bitmap.Width;
            m_height = m_bitmap.Height;
        }

        public ImageBitmap(string filePath)
        {
            using (SKCodec sKCodec = SKCodec.Create(filePath))
            {
                m_bitmap = SKBitmap.Decode(sKCodec, new SKImageInfo
                {
                    ColorType = SKColorType.Bgra8888,
                    AlphaType = SKAlphaType.Unpremul,
                    ColorSpace = null,
                    Width = sKCodec.Info.Width,
                    Height = sKCodec.Info.Height
                });
            }
            m_width = m_bitmap.Width;
            m_height = m_bitmap.Height;
        }

        protected override YFBitmap InternalCreate(int width, int height) => new ImageBitmap(width, height);
        protected override YFBitmap InternalCreate(Stream stream) => new ImageBitmap(stream);

        /// <summary>
        /// BB GG RR AA
        /// </summary>
        /// <returns></returns>
        public override nint Pixels => m_bitmap.GetPixels();

        public override void Save(Stream stream)
        {
            using (SKPixmap sKPixmap = m_bitmap.PeekPixels())
            {
                using (SKData p = sKPixmap?.Encode(new SKPngEncoderOptions { ZLibLevel = 1 }))
                {
                    byte[] t = p.ToArray();
                    stream.Write(t, 0, t.Length);
                    t = null;
                }
            }
        }

        public override void Dispose() => m_bitmap?.Dispose();
#else
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
#endif
    }
}
