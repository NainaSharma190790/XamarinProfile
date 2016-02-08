using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using XamarinProfile;
using XamarinProfile.Droid;
using Colors = XamarinProfile.Helpers.Color;
using Android.Views;

[assembly: ExportRenderer(typeof(EditText), typeof(EditTextRenderer))]
namespace XamarinProfile.Droid
{
	public class EditTextRenderer: EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
			{
				Control.SetBackgroundResource(Resource.Drawable.EditText);
				//Control.TextAlignment = Android.Views.TextAlignment.Center;
				Control.Gravity = GravityFlags.Center;
				//Control.HintTextColors = Color.Gray.ToAndroid ();

			}
		}
	}
}