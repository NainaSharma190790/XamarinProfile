using System;
using Xamarin.Forms;
using XamarinProfile;
using XamarinProfile.iOS;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Colors = XamarinProfile.Helpers.Color;

[assembly: ExportRenderer(typeof(EditText), typeof(EditTextRenderer))]

namespace XamarinProfile.iOS
{
	public class EditTextRenderer:EntryRenderer
	{

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
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

