using System;
using Xamarin.Forms;
using XamarinProfile;
using XamarinProfile.iOS;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Colors = XamarinProfile.Helpers.Color;

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
				Control.BorderStyle = UITextBorderStyle.RoundedRect;
				Control.Layer.CornerRadius = 2f;
				Control.Layer.BorderColor = UIColor.FromRGBA(44,62,80,255).CGColor;
				Control.Layer.BorderWidth = 2f;
				Control.TextAlignment = UITextAlignment.Center;
			}
		}
	}
}

