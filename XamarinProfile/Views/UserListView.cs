using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Colors = XamarinProfile.Helpers.Color;
namespace XamarinProfile
{
	public class UserListView : ContentPage
	{
		
		public int Height = App.ScreenHeight;
		public int Width = App.ScreenWidth;
		public UserViewModel ViewModel { get { return BindingContext as UserViewModel; } }
		public ListView list = null;
		public static List<UserRegistrationRequest> serverList = new List<UserRegistrationRequest>();

		public UserListView ()
		{

			BindingContext = new UserViewModel(this.Navigation);
			ViewModel.GetAllUsers.Execute(null);

			list = new ListView()
			{ 
				RowHeight=Height/6,
				VerticalOptions=LayoutOptions.FillAndExpand,
				BackgroundColor=Colors.DarkGray.ToFormsColor(),
				SeparatorColor=Colors.Green.ToFormsColor(),
				SeparatorVisibility =SeparatorVisibility.Default,
			};
			list.ItemTemplate = new DataTemplate(typeof(UserCellView));
			list.ItemsSource = serverList;
			//list.ItemSelected += list_ItemSelected;
			list.SelectedItem = null;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			try
			{
				//list.ItemsSource = ViewModel.GetAllUsers;
				//ViewModel.PreviousPage = this;
			}
			catch (Exception ex)
			{

			}
		}
	}
}