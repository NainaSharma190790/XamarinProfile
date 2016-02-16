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
		public ListView list =  new ListView();

		public UserListView ()
		{

			BindingContext = new UserViewModel(this.Navigation);

			list = new ListView()
			{ 
				RowHeight=Height/6,
				VerticalOptions=LayoutOptions.FillAndExpand,
				BackgroundColor=Colors.DarkGray.ToFormsColor(),
				SeparatorColor=Colors.Green.ToFormsColor(),
				SeparatorVisibility =SeparatorVisibility.Default,
			};
			string my=ViewModel.UserListInfo.experts;
			UserCellView xy= new UserCellView ();
			list.ItemsSource = ViewModel.UserDetails;
			list.ItemTemplate = new DataTemplate(typeof(UserCellView));

			this.Content = list;

		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			list.ItemsSource = ViewModel.UserDetails;
		}

	}
}