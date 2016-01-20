using System;
using Xamarin.Forms;
using XamarinProfile;
using XamarinProfile.iOS;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]

namespace XamarinProfile.iOS
{
	public class CustomPickerRenderer:PickerRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
			{
				Control.BorderStyle = UITextBorderStyle.None;
				Control.Layer.CornerRadius = 0f;
			}
		}
	}
}

