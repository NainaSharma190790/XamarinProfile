using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XLabs.Ioc;
using XLabs.Platform.Services.Media;

namespace XamarinProfile.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();
			if (!Resolver.IsSet)
			{
				this.SetIoc();
			}
			App.ScreenHeight = (int)UIScreen.MainScreen.Bounds.Height;
			App.ScreenWidth = (int)UIScreen.MainScreen.Bounds.Width;
			LoadApplication (new XamarinProfile.App ());
			return base.FinishedLaunching (app, options);
		}
		private void SetIoc()
		{
			var resolverContainer = new SimpleContainer();
			resolverContainer.Register<IMediaPicker>(new MediaPicker());
			Resolver.SetResolver(resolverContainer.GetResolver());
		}
	}
}

