using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using XamarinProfile.Droid;
using XamarinProfile;
using Android.Views;


[assembly: ExportRenderer(typeof(ButtonTextAlignment), typeof(ButtonTextAlignmentRenderer))]

namespace XamarinProfile.Droid
{
	public class ButtonTextAlignmentRenderer:ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			
			base.OnElementChanged(e);
			if (Control != null)
			{
				Control.Gravity = GravityFlags.Center;
				Control.SetPadding (0, 15, 0, 0);
				Control.SetBackgroundResource(Resource.Drawable.EditText);

			}
		}
	}
}


