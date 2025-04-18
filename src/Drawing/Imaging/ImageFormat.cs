namespace Belin.Core.Drawing.Imaging;

using System.Text.Json.Serialization;

/// <summary>
/// Defines the format of an image.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ImageFormat {

	/// <summary>
	/// The image format is AV1 Image File Format (AVIF).
	/// </summary>
	Avif,

	/// <summary>
	/// The image format is Graphics Interchange Format (GIF).
	/// </summary>
	Gif,

	/// <summary>
	/// The image format is Joint Photographic Experts Group (JPEG).
	/// </summary>
	Jpeg,

	/// <summary>
	/// The image format is Portable Network Graphics (PNG).
	/// </summary>
	Png,

	/// <summary>
	/// The image format is WebP.
	/// </summary>
	WebP
}

/// <summary>
/// Provides extension methods for image formats.
/// </summary>
public static class ImageFormatExtensions {

	/// <summary>
	/// Gets the media type corresponding to the specified image format.
	/// </summary>
	/// <param name="imageFormat">The image format.</param>
	/// <returns>The media type corresponding to the specified image format.</returns>
	public static string GetMediaType(this ImageFormat imageFormat) => $"image/{imageFormat.ToString().ToLowerInvariant()}";
}
