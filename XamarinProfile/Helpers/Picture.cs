using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Foundation;

namespace XamarinProfile
{
	public class Picture: IPicture
	{
		public async void SavePictureToDisk (ImageSource imgSrc, int Id)
		{
			var renderer = new StreamImagesourceHandler ();
			var photo = await renderer.LoadImageAsync (imgSrc);
			var documentsDirectory = Environment.GetFolderPath
				(Environment.SpecialFolder.Personal);
			string jpgFilename = System.IO.Path.Combine (
				documentsDirectory, Id.ToString () + ".jpg");
			NSData imgData = photo.AsJPEG ();
			NSError err = null;
			if (imgData.Save (jpgFilename, false, out err)) {
				Console.WriteLine ("saved as " + jpgFilename);
			} else {
				Console.WriteLine ("NOT saved as " + jpgFilename +
					" because" + err.LocalizedDescription);
			}

		}


		public string GetPictureFromDisk (int Id)
		{
			var documentsDirectory = Environment.GetFolderPath
				(Environment.SpecialFolder.Personal);
			string jpgFilename = System.IO.Path.Combine (documentsDirectory, Id.ToString () + ".jpg");
			return jpgFilename;
		}
	}
}
