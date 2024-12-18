using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;

namespace NokoWebApiSdk.Exceptions;

public static class ImageExtensions
{
	#region Public Methods

	public static byte[] ToArray(this SixLabors.ImageSharp.Image image)
	{
		var imageEncoder = new PngEncoder();
		return image.ToArray(imageEncoder);
	}

	public static byte[] ToArray(this SixLabors.ImageSharp.Image image, IImageEncoder fmt)
	{
		using var ms = new MemoryStream();
		image.Save(ms, fmt);
		return ms.ToArray();
	}
	
	public static SixLabors.ImageSharp.Image ToImage(this byte[] buff)
	{
		using var ms = new MemoryStream(buff);
		return SixLabors.ImageSharp.Image.Load(ms);
	}

	// [SupportedOSPlatform("windows")]
	// public static System.Drawing.Image ToNetImage(this byte[] buff)
	// {
	// 	using var ms = new MemoryStream(buff);
	// 	return System.Drawing.Image.FromStream(ms);
	// }

	#endregion Public Methods
}
