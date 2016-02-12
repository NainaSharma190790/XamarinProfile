using System;

using Xamarin.Forms;
using Colors = XamarinProfile.Helpers.Color;

namespace XamarinProfile
{
	public class ForgotPasswordView : ContentPage
	{
		#region All Fields
		public StackLayout stack_MainLayout;
		public Label lbl_Forgot;
		public Button btn_Submit;
		public EditText txt_Email;
		public int Width = App.ScreenWidth;
		public int Height = App.ScreenHeight;

		#endregion
		public ForgotPasswordView ()
		{
			lbl_Forgot = new Label
			{
				HorizontalOptions = LayoutOptions.Center,
				Text="AppLogo.png",
				FontSize=20,
				TextColor=Colors.DarkGray.ToFormsColor()
			};

			txt_Email = new EditText
			{
				WidthRequest = Width,
				HorizontalOptions = LayoutOptions.Center,
				Keyboard=Keyboard.Email,
				Placeholder="Email",
				TextColor=Colors.DarkGray.ToFormsColor(),
				BackgroundColor=Color.Transparent,
				HeightRequest=Width/10
			};
			//txt_Email.SetBinding(Entry.TextProperty, "UserRegInfo.name");

			btn_Submit= new Button
			{
				WidthRequest = Width/2,
				HorizontalOptions = LayoutOptions.Center,
				HeightRequest=Width/8,			
				Text="Submit",	
				FontSize=17,
				TextColor=Color.White,
				BackgroundColor=Colors.DarkGray.ToFormsColor(),
			};
			btn_Submit.Clicked+= (sender, e) => 
			{
				Navigation.PushModalAsync(new UserRegistrationView());

			};


			stack_MainLayout = new StackLayout 
			{ 
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions=LayoutOptions.Fill,
				BackgroundColor=Color.White,
				WidthRequest=Width,
				Padding=new Thickness(Width/10,Width/10,Width/10,Width/10),
				Spacing=Width/10,
				HeightRequest=Height,
				Children = 
				{
					lbl_Forgot,txt_Email,btn_Submit
				}
				};

			this.Content = stack_MainLayout;
		}


	}
}


