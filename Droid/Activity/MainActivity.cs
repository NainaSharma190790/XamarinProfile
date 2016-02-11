using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XLabs.Ioc;
using XLabs.Platform.Services.Media;

namespace XamarinProfile.Droid
{
	[Activity(Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			int Width = Convert.ToInt32(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);
			float HeightF = (Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
			int Height = Convert.ToInt16(HeightF);
			App.ScreenHeight = Height;
			App.ScreenWidth = Width;

			if (!Resolver.IsSet)
			{
				this.SetIoc();
			}
			global::Xamarin.Forms.Forms.Init(this, bundle);
			LoadApplication(new XamarinProfile.App());
		}

		private void SetIoc()
		{
			var resolverContainer = new SimpleContainer();
			resolverContainer.Register<IMediaPicker>(new MediaPicker());
			Resolver.SetResolver(resolverContainer.GetResolver());
		}
	}
}