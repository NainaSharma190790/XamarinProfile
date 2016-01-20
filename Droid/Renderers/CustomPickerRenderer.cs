using System;
using Xamarin.Forms;
using XamarinProfile;
using XamarinProfile.Droid;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace XamarinProfile.Droid
{
	public class CustomPickerRenderer:PickerRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
			{
				Control.SetBackgroundResource(Resource.Drawable.EditText);
				Control.SetTextColor(Color.Black.ToAndroid());
			}
		}
	}
}

