using System;
using System.IO;
using SkiaSharp;

namespace Avalonia.Skia.Helpers
{
    /// <summary>
    /// Helps with saving images to stream/file.
    /// </summary>
    public static class ImageSavingHelper
    {
        /// <summary>
        /// Save Skia image to a file.
        /// </summary>
        /// <param name="image">Image to save</param>
        /// <param name="fileName">Target file.</param>
        /// <param name="quality">
        /// The optional quality for PNG compression. 
        /// The quality value is interpreted from 0 - 100. If quality is null 
        /// the encoder applies the default quality value.
        /// </param>
        public static void SaveImage(SKImage image, string fileName, int? quality = null)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));

            using (var stream = File.Create(fileName))
            {
                SaveImage(image, stream, quality);
            }
        }

        /// <summary>
        /// Save Skia bitmap to a file.
        /// </summary>
        /// <param name="bitmap">Bitmap to save</param>
        /// <param name="fileName">Target file.</param>
        /// <param name="quality">
        /// The optional quality for PNG compression. 
        /// The quality value is interpreted from 0 - 100. If quality is null 
        /// the encoder applies the default quality value.
        /// </param>
        public static void SaveImage(SKBitmap bitmap, string fileName, int? quality = null)
        {
            if (bitmap == null)
                throw new ArgumentNullException(nameof(bitmap));
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            using (var stream = File.Create(fileName))
            {
                SaveImage(bitmap, stream, quality);
            }
        }

        /// <summary>
        /// Save Skia bitmap to a stream.
        /// </summary>
        /// <param name="bitmap">Bitmap to save</param>
        /// <param name="stream">The output stream to save the image.</param>
        /// <param name="quality">
        /// The optional quality for PNG compression. 
        /// The quality value is interpreted from 0 - 100. If quality is null 
        /// the encoder applies the default quality value.
        /// </param>
        public static void SaveImage(SKBitmap bitmap, Stream stream, int? quality = null)
        {
            if (bitmap == null)
                throw new ArgumentNullException(nameof(bitmap));
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            using (var data = bitmap.Encode(SKEncodedImageFormat.Png, quality ?? 100))
            {
                data.SaveTo(stream);
            }
        }

        /// <summary>
        /// Save Skia image to a stream.
        /// </summary>
        /// <param name="image">Image to save</param>
        /// <param name="stream">The output stream to save the image.</param>
        /// <param name="quality">
        /// The optional quality for PNG compression. 
        /// The quality value is interpreted from 0 - 100. If quality is null 
        /// the encoder applies the default quality value.
        /// </param>
        public static void SaveImage(SKImage image, Stream stream, int? quality = null)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            using (var data = image.Encode(SKEncodedImageFormat.Png, quality ?? 100))
            {
                data.SaveTo(stream);
            }
        }

        // This method is here mostly for debugging purposes
        internal static void SavePicture(SKPicture picture, float scale, string path)
        {
            var snapshotSize = new SKSizeI((int)Math.Ceiling(picture.CullRect.Width * scale),
                (int)Math.Ceiling(picture.CullRect.Height * scale));
            using var snap =
                SKImage.FromPicture(picture, snapshotSize, SKMatrix.CreateScale(scale, scale));
            SaveImage(snap, path);
        }
    }
}
