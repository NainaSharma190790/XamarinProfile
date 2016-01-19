using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace XamarinProfile.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();
			App.ScreenHeight = (int)UIScreen.MainScreen.Bounds.Height;
			App.ScreenWidth = (int)UIScreen.MainScreen.Bounds.Width;
			LoadApplication (new XamarinProfile.App ());
			return base.FinishedLaunching (app, options);
		}
	}
}

