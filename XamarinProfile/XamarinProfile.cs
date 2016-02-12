using System;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace XamarinProfile
{
	public class App : Application
		{
		public static int ScreenHeight;
		public static int ScreenWidth;
		public NavigationPage AppNavigation { get; set; }


		public App ()
		{
			Database.CreateTables<CountryPicker>();

			MainPage = new UserLoginView();
		}

		private static XamarinProfileDataBase _database = new XamarinProfileDataBase();

		public static XamarinProfileDataBase Database { get { return _database; } }


		public static App Instance
		{
			get
			{
				return (App)Xamarin.Forms.Application.Current;
			}
		}
		public Task NavigateToAsync(Page page)
		{
			return AppNavigation.PushAsync(page);
		}
		public Task NavigateToPopAsync(Page page)
		{
			return AppNavigation.PopToRootAsync();
		}
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

