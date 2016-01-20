using System;
using Xamarin.Forms;
using XamarinProfile;
using XamarinProfile.iOS;
using Xamarin.Forms.Platform.iOS;
using UIKit;

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
				Control.BorderStyle = UITextBorderStyle.None;
				Control.Layer.CornerRadius = 0f;
			}
		}
	}
}

