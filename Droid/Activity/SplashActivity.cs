
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace XamarinProfile.Droid
{
	[Activity(MainLauncher = true, NoHistory = true,ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashActivity : Activity
	{
		
			#region Private

			private static int SPLASH_TIME_OUT = 1000;

			#endregion

			protected override void OnCreate(Bundle bundle)
			{
				RequestWindowFeature(WindowFeatures.NoTitle);
				base.OnCreate(bundle);
				SetContentView(Resource.Layout.SplashLayout);
				Handler handler = new Handler();
				handler.PostDelayed(CallHomeActivity, SPLASH_TIME_OUT);
			}

			#region CALL HOME ACTIVITY
			public void CallHomeActivity()
			{
				StartActivity(typeof(MainActivity));
				Finish();
				// OverridePendingTransition(Resource.Animation.slide_in_left, Resource.Animation.slide_out_left);
			}

			#endregion
		}
}