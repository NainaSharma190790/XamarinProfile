using System;
using Xamarin.Forms;
using Colors = XamarinProfile.Helpers.Color;
using XLabs.Forms.Controls;

namespace XamarinProfile
{
	public class UserLoginView:ContentPage
	{
		#region All Fields
		public StackLayout stack_MainLayout,stack_BottomView;
		public Image img_Logo;
		public ExtendedLabel lbl_Registration,lbl_Forgot;
		public Button btn_Login;
		public EditText txt_Email,txt_Password;
		public int Width = App.ScreenWidth;
		public int Height = App.ScreenHeight;

		#endregion

		public UserViewModel ViewModel { get { return BindingContext as UserViewModel; } }
		public UserLoginView ()
		{
			
			BindingContext = new UserViewModel(this.Navigation);


			var indicator = new ActivityIndicator
			{
				Color = Colors.DarkGray.ToFormsColor(),

			};
			indicator.SetBinding (ActivityIndicator.IsRunningProperty, "IsLoading");
			indicator.SetBinding (ActivityIndicator.IsVisibleProperty, "IsLoading");

			img_Logo = new Image
			{
				HeightRequest = Width/4,
				WidthRequest = Width/4,
				HorizontalOptions = LayoutOptions.Center,
				Source="AppLogo.png",
			};

			txt_Email = new EditText
			{
				WidthRequest = Width,
				HorizontalOptions = LayoutOptions.Center,
				Keyboard=Keyboard.Email,
				Placeholder="Email",
				TextColor=Colors.DarkGray.ToFormsColor(),
				BackgroundColor=Color.Transparent,
				HeightRequest=Width/10,
				Text="naina.sharma@netsmartz.net"
			};
			txt_Email.SetBinding(Entry.TextProperty, "Username");

			txt_Password = new EditText
			{
				WidthRequest = Width,
				HorizontalOptions = LayoutOptions.Center,
				IsPassword=true,
				Placeholder="Password",
				TextColor=Colors.DarkGray.ToFormsColor(),
				BackgroundColor=Color.Transparent,
				HeightRequest=Width/10,
				Text="123"
					
			};
			txt_Password.SetBinding(Entry.TextProperty, "Password");

			btn_Login= new Button
			{
				WidthRequest = Width/2,
				HorizontalOptions = LayoutOptions.Center,
				HeightRequest=Width/8,			
				Text="Login",	
				FontSize=17,
				TextColor=Color.White,
				BackgroundColor=Colors.DarkGray.ToFormsColor(),
			};
			btn_Login.Clicked+= (sender, e) => 
			{

				btn_Login.CommandParameter = 1;
				btn_Login.Command= ViewModel.LoginUser;

			};

			lbl_Registration = new ExtendedLabel
			{
				WidthRequest = Width,
				Text="Sing Up",
				HorizontalOptions = LayoutOptions.Start,
				TextColor=Colors.DarkGray.ToFormsColor(),
				BackgroundColor=Color.Transparent,
				FontSize=18,
				IsUnderline=true

			};
			var Regtap = new TapGestureRecognizer(OnRegistrationTapped);
			Regtap.NumberOfTapsRequired = 1;
			lbl_Registration.IsEnabled = true;
			lbl_Registration.GestureRecognizers.Clear();
			lbl_Registration.GestureRecognizers.Add(Regtap);

			lbl_Forgot = new ExtendedLabel
			{
				WidthRequest = Width,
				Text="Forgot Password",
				HorizontalOptions = LayoutOptions.End,
				TextColor=Colors.DarkGray.ToFormsColor(),
				BackgroundColor=Color.Transparent,
				FontSize=18,
				IsUnderline=true

			};
			var Forgottap = new TapGestureRecognizer(OnForgotPasswordTapped);
			Forgottap.NumberOfTapsRequired = 1;
			lbl_Forgot.IsEnabled = true;
			lbl_Forgot.GestureRecognizers.Clear();
			lbl_Forgot.GestureRecognizers.Add(Forgottap);

			 stack_BottomView = new StackLayout
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions=LayoutOptions.Fill,
				//BackgroundColor=Color.Aqua,
				Orientation=StackOrientation.Horizontal,
				Children=
				{
					lbl_Registration,lbl_Forgot			
				}
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
					indicator,img_Logo,txt_Email,txt_Password,btn_Login,stack_BottomView
				}
				};
			this.Content = stack_MainLayout;
		}
		void OnRegistrationTapped(View view, object sender)
		{
			Navigation.PushModalAsync(new UserRegistrationView());

		}
		void OnForgotPasswordTapped(View view, object sender)
		{
			Navigation.PushModalAsync(new ForgotPasswordView());

		}


	}
}


