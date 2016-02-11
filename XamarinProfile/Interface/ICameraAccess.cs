using System;
using XLabs.Platform.Services.Media;

namespace XamarinProfile
{
	public interface ICameraAccess
	{
		void GetImageAsync(Action<MediaFile> imageData, bool fromCamera = true);
	}
}

