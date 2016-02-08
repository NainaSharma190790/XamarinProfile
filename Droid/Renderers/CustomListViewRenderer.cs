using System;
using Xamarin.Forms;
using XamarinProfile;
using XamarinProfile.Droid;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(CustomListView), typeof(CustomListViewRenderer))]

namespace XamarinProfile.Droid
{

	public class CustomListViewRenderer:ListViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
		{
			base.OnElementChanged (e);
			if (Control != null)
			{
				Control.DividerHeight = 2;
				//Control.Divider = new ColorDrawable(Android.Graphics.Color.Blue);
				//Control.SetBackgroundResource(Resource.Drawable.Divider);

			}
		}
	}
}

