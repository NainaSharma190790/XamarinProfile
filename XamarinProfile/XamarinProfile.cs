using System;

using Xamarin.Forms;

namespace XamarinProfile
{
	public class App : Application
		{
		public static int ScreenHeight;
		public static int ScreenWidth;


		public App ()
		{
			Database.CreateTables<CountryPicker>();
			MainPage = new UserRegistrationView();
		}

		private static XamarinProfileDataBase _database = new XamarinProfileDataBase();

		public static XamarinProfileDataBase Database { get { return _database; } }



		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

