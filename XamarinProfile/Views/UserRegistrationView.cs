using System;
using Colors = XamarinProfile.Helpers.Color;
using Xamarin.Forms;
using Android.Graphics;

namespace XamarinProfile
{
	public class UserRegistrationView : ContentPage
	{
		#region All Fields
		public StackLayout stack_pop,stack_popup,mainLayout,stack_TopView ,stack_MiddleView,stack_CoverPage,stack_FirstExpert,stack_SecondExpert;
		public RelativeLayout rltv_main;
		public ScrollView scroll_Main;
		public CustomPicker picker_Country;
		public Image image_bg,img_iOS,img_Android,img_Forms,img_Testcloud,img_Insights,img_Certified;
		public CircleImage img_User;
		public Label lbl_Expert;
		public Image btn_camera, btn_gallery;
		public Button btn_Submit;
		public EditText txt_Name,txt_Email,txt_Password;
		public int Width = App.ScreenWidth;
		public int Height = App.ScreenHeight;
		public int iClicks = 0;
		public string ExpertsValue="";
		#endregion

		public UserViewModel ViewModel { get { return BindingContext as UserViewModel; } }


		public UserRegistrationView ()
		{
//			if (ViewModel == null)
//				ViewModel = new UserViewModel();
//			BindingContext = ViewModel;

			BindingContext = new UserViewModel(this.Navigation);

			stack_CoverPage = new StackLayout
			{
				WidthRequest=Width,
				HeightRequest = Width/4,
				//BackgroundColor=Color.Red,
				BackgroundColor=Colors.DarkGray.ToFormsColor(),
			
			};
			img_User = new CircleImage
			{
				WidthRequest = Width/4,
				HeightRequest = Width/4,

				HorizontalOptions = LayoutOptions.Center,
				//BackgroundColor=Color.Yellow,
				TranslationY=-((Width/4)/2+10),
				Aspect=Aspect.Fill,
				Source="CircleImage.png",
			};
			img_User.SetBinding(CircleImage.SourceProperty, "ImageSource", BindingMode.Default);

		
			ViewModel.ProfilePicture = img_User;
			btn_camera = new Image
			{
				Source="camera.png",
				WidthRequest=Width/8,
				HeightRequest=Height/8,
				HorizontalOptions=LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center,
				BackgroundColor=Xamarin.Forms.Color.Red,

			};

			var Cameratap = new TapGestureRecognizer(OnCameraTapped);
			btn_camera.IsEnabled = true;
			btn_camera.GestureRecognizers.Clear();
			btn_camera.GestureRecognizers.Add(Cameratap);

			btn_gallery = new Image
			{
				Source="gallery.png",
				WidthRequest=Width/8,
				HeightRequest=Height/8,
				HorizontalOptions=LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center,
				BackgroundColor=Xamarin.Forms.Color.Green,

			};
			var Gallerytap = new TapGestureRecognizer(OnGalleryTapped);
			btn_gallery.IsEnabled = true;
			btn_gallery.GestureRecognizers.Clear();
			btn_gallery.GestureRecognizers.Add(Gallerytap);

			stack_pop = new StackLayout
			{
				HeightRequest = Width / 2,
				WidthRequest = Width / 2,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Xamarin.Forms.Color.White,
				TranslationY=Width / 2,
				Opacity = 1,
				Children = 
				{
					new StackLayout
					{
						VerticalOptions = LayoutOptions.Center,
						HorizontalOptions = LayoutOptions.Center,
						Orientation = StackOrientation.Horizontal,
						TranslationY=Width/6,
						Children=
						{
							btn_camera, btn_gallery
						}
						
					}
				}
			};
			stack_popup = new StackLayout
			{ 
				WidthRequest = Width,
				HeightRequest = Height,
				BackgroundColor=Xamarin.Forms.Color.Transparent,
				Children=
				{
					stack_pop
				}
			};
			image_bg = new Image
			{ 
				WidthRequest = Width,
				HeightRequest = Height,
				HorizontalOptions=LayoutOptions.FillAndExpand,
				VerticalOptions=LayoutOptions.FillAndExpand,
				Source="bg.png",
				IsVisible=false

			};
			var PopUpGestureRecognizer = new  TapGestureRecognizer 
			{
				//ViewModel.SelectPictureCommand.Execute(null);
				Command = new Command (() => 
					{
						//ViewModel.SelectPictureCommand.Execute(null);
						var eAndN = new Tuple<Easing, string>[]
						{
							new Tuple<Easing, string> (Easing.Linear, "Linear") 
						};
						double w = Width;
						double h = Height;
						var newPos = new Rectangle(0, h, w, h);
						var eAndName = eAndN[iClicks];
						var easing = eAndName.Item1;
						stack_popup.LayoutTo(newPos, 80, easing);
						iClicks %= eAndN.Length;
						image_bg.IsVisible=false;
							

					}),
				NumberOfTapsRequired = 1
			};
			stack_popup.GestureRecognizers.Add(PopUpGestureRecognizer);


			var ProfilePictureGestureRecognizer = new  TapGestureRecognizer 
			{
				//ViewModel.SelectPictureCommand.Execute(null);
				Command = new Command (() => 
					{
						var eAndN = new Tuple<Easing, string>[]
						{
							new Tuple<Easing, string> (Easing.Linear, "Linear") 
						};
						double w = Width;
						double h = Height;
						var newPos = new Rectangle(0, 0, w, h);
						var eAndName = eAndN[iClicks];
						var easing = eAndName.Item1;
						stack_popup.LayoutTo(newPos, 80, easing);
						iClicks %= eAndN.Length;
						image_bg.IsVisible=true;

				}),
				NumberOfTapsRequired = 1
			};
			img_User.GestureRecognizers.Add(ProfilePictureGestureRecognizer);
			txt_Name = new EditText
			{
				WidthRequest = Width,
				HorizontalOptions = LayoutOptions.Center,
				Placeholder="FullName",
				TextColor=Colors.DarkGray.ToFormsColor(),
				BackgroundColor=Xamarin.Forms.Color.Transparent,
				HeightRequest=Width/10,
			};
			txt_Name.SetBinding(Entry.TextProperty, "UserRegInfo.name");

			txt_Email = new EditText
			{
				WidthRequest = Width,
				HorizontalOptions = LayoutOptions.Center,
				Keyboard=Keyboard.Email,
				Placeholder="Email",
				TextColor=Colors.DarkGray.ToFormsColor(),
				BackgroundColor=Xamarin.Forms.Color.Transparent,
				HeightRequest=Width/10
					
			};
			txt_Email.SetBinding(Entry.TextProperty, "UserRegInfo.email");

			txt_Password = new EditText
			{
				WidthRequest = Width,
				HorizontalOptions = LayoutOptions.Center,
				IsPassword=true,
				Placeholder="Password",
				TextColor=Colors.DarkGray.ToFormsColor(),
				BackgroundColor=Xamarin.Forms.Color.Transparent,
				HeightRequest=Width/10

			};
			txt_Password.SetBinding(Entry.TextProperty, "UserRegInfo.password");

			picker_Country = new CustomPicker
			{
				WidthRequest = Width,
				HorizontalOptions = LayoutOptions.Center,
				Title="Location",
				HeightRequest=Width/10,
				BackgroundColor=Xamarin.Forms.Color.Transparent,

			};
				picker_Country.SelectedIndexChanged+=((sender, e) => 
				{
					ViewModel.SelectedIndex=picker_Country.SelectedIndex;
					
				});


			btn_Submit= new Button
			{
				WidthRequest = Width/2,
				HorizontalOptions = LayoutOptions.Center,
				HeightRequest=Width/8,			
				Text="Submit",	
				FontSize=17,
				TextColor=Xamarin.Forms.Color.White,
				BackgroundColor=Colors.DarkGray.ToFormsColor(),

			};
			btn_Submit.Clicked+= (sender, e) => 
			{
				btn_Submit.CommandParameter = 1;
				btn_Submit.Command= ViewModel.RegisterUser;
				Navigation.PushModalAsync(new UserListView());
				
			};
			stack_TopView = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Fill,
				HeightRequest=Width/3,
				Padding= new Thickness(0,0,0,Height/45),
				//BackgroundColor=Color.Blue,
				Children=
				{
					stack_CoverPage,img_User
				}
			};
			
			lbl_Expert = new Label
			{
				Text="Experts",
				FontSize=18,
				TextColor=Colors.DarkGray.ToFormsColor(),
				FontAttributes=FontAttributes.Bold
			};

			stack_MiddleView = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions=LayoutOptions.StartAndExpand,
				Padding= new Thickness(Width/8,Height/40,Width/8,0),
				//BackgroundColor=Color.Pink,
				Spacing=Height/45,
				Children=
				{
					txt_Name,txt_Email,txt_Password,picker_Country,lbl_Expert


				}
			};
			
			 img_iOS = new Image
			{
				WidthRequest=Width/7,
				HeightRequest=Width/7,
				HorizontalOptions=LayoutOptions.Start,
				Source="iOS.png",
				BackgroundColor=Colors.DarkGray.ToFormsColor(),
				IsEnabled=false
			};
			var iOStap = new TapGestureRecognizer(OniOSTapped);
			iOStap.NumberOfTapsRequired = 1;
			img_iOS.IsEnabled = true;
			img_iOS.GestureRecognizers.Clear();
			img_iOS.GestureRecognizers.Add(iOStap);

			img_Certified = new Image
			{
				WidthRequest=Width/7,
				HeightRequest=Width/7,
				HorizontalOptions=LayoutOptions.Center,
				Source="Certified.png",
				BackgroundColor=Colors.DarkGray.ToFormsColor(),
				IsEnabled=false
			};

			var Certifiedtap = new TapGestureRecognizer(OnCertifiedTapped);
			img_Certified.IsEnabled = true;
			img_Certified.GestureRecognizers.Clear();
			img_Certified.GestureRecognizers.Add(Certifiedtap);

			img_Android = new Image
			{
				WidthRequest=Width/7,
				HeightRequest=Width/7,
				HorizontalOptions=LayoutOptions.End,
				Source="Android.png",
				BackgroundColor=Colors.DarkGray.ToFormsColor(),
				IsEnabled=false
			};

			var Androidtap = new TapGestureRecognizer(OnAndroidTapped);
			Androidtap.NumberOfTapsRequired = 1;
			img_Android.IsEnabled = true;
			img_Android.GestureRecognizers.Clear();
			img_Android.GestureRecognizers.Add(Androidtap);

			img_Forms = new Image
			{
				WidthRequest=Width/7,
				HeightRequest=Width/7,
				HorizontalOptions=LayoutOptions.Start,
				Source="Forms.png",
				BackgroundColor=Colors.DarkGray.ToFormsColor(),
				IsEnabled=false
			};

			var Formstap = new TapGestureRecognizer(OnFormsTapped);
			img_Forms.IsEnabled = true;
			img_Forms.GestureRecognizers.Clear();
			img_Forms.GestureRecognizers.Add(Formstap);


			img_Insights = new Image
			{
				WidthRequest=Width/7,
				HeightRequest=Width/7,
				HorizontalOptions=LayoutOptions.Center,
				Source="Insight.png",
				BackgroundColor=Colors.DarkGray.ToFormsColor(),
				IsEnabled=false
			};

			var Insightstap = new TapGestureRecognizer(OnInsightTapped);
			img_Insights.IsEnabled = true;
			img_Insights.GestureRecognizers.Clear();
			img_Insights.GestureRecognizers.Add(Insightstap);


			img_Testcloud = new Image
			{
				WidthRequest=Width/7,
				HeightRequest=Width/7,
				HorizontalOptions=LayoutOptions.End,
				Source="TestCloud.png",
				BackgroundColor=Colors.DarkGray.ToFormsColor(),
				IsEnabled=false
			};
			var Testcloudtap = new TapGestureRecognizer(OnTestCloudTapped);
			img_Testcloud.IsEnabled = true;
			img_Testcloud.GestureRecognizers.Clear();
			img_Testcloud.GestureRecognizers.Add(Testcloudtap);


			stack_FirstExpert = new StackLayout
			{
				HorizontalOptions=LayoutOptions.Center,
				VerticalOptions=LayoutOptions.StartAndExpand,
				Orientation=StackOrientation.Horizontal,
				//BackgroundColor=Color.Yellow,
				Spacing=Height/40,
				Children=
				{
					img_iOS,img_Certified,img_Android
				}
			};
			stack_SecondExpert = new StackLayout
			{
				HorizontalOptions=LayoutOptions.Center,
				VerticalOptions=LayoutOptions.StartAndExpand,
				Orientation=StackOrientation.Horizontal,
				//BackgroundColor=Color.Red,
				Spacing=Height/40,
				Children=
				{
					img_Forms,img_Insights,img_Testcloud
				}
			};
			StackLayout stack_MainLayout = new StackLayout
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions=LayoutOptions.Fill,
				//BackgroundColor=Color.Aqua,
				Spacing=Width/25,
				Children=
				{
					stack_MiddleView,stack_FirstExpert,stack_SecondExpert,btn_Submit
				}
				};
			
			scroll_Main = new ScrollView 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,	
				VerticalOptions=LayoutOptions.Start,
			};
			scroll_Main.Content=stack_MainLayout;

			 mainLayout = new StackLayout 
			{ 
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions=LayoutOptions.End,
				BackgroundColor=Xamarin.Forms.Color.White,
				WidthRequest=Width,
				HeightRequest=Height,
				Children = 
				{
					stack_TopView,scroll_Main
				}
			};
			
			rltv_main = new RelativeLayout
			{ 
				WidthRequest = Width,
				HeightRequest = Height
			};

			rltv_main.Children.Add(mainLayout, Constraint.Constant(0), Constraint.Constant(0),Constraint.Constant(Width),Constraint.Constant(Height));
			rltv_main.Children.Add(image_bg, Constraint.Constant(0), Constraint.Constant(0),Constraint.Constant(Width),Constraint.Constant(Height));
			rltv_main.Children.Add(stack_popup, Constraint.Constant(0), Constraint.Constant(Height),Constraint.Constant(Width),Constraint.Constant(Height));

			this.Content = rltv_main;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.PropertyChanged += ViewModel_PropertyChanged;
			ViewModel.GetCountriesCommand.Execute(null); //It should be below the above event
		}
		void SetExpert(string IdValue)
		{
				if(ExpertsValue.Contains(IdValue))
					{
						ExpertsValue = ExpertsValue.Replace (IdValue,"");
					}
					else
					{
						ExpertsValue = ExpertsValue + IdValue;
					}
			ViewModel.ExpertValue=ExpertsValue;

		}

		void OniOSTapped(View view, object sender)
		{
			if(img_iOS.IsEnabled==false)
			{
				img_iOS.IsEnabled=true;
				img_iOS.BackgroundColor=Colors.DarkGray.ToFormsColor();
			}
			else
			{
				img_iOS.IsEnabled=false;
				img_iOS.BackgroundColor=Colors.Blue.ToFormsColor();
			}
			SetExpert ("1");
		}
		void OnAndroidTapped(View view, object sender)
		{
			if(img_Android.IsEnabled==false)
			{
				img_Android.IsEnabled=true;
				img_Android.BackgroundColor=Colors.DarkGray.ToFormsColor();
			}
			else
			{
				img_Android.IsEnabled=false;
				img_Android.BackgroundColor=Colors.Blue.ToFormsColor();
			}
			SetExpert ("3");
		}
		void OnFormsTapped(View view, object sender)
		{
			if(img_Forms.IsEnabled==false)
			{
				img_Forms.IsEnabled=true;
				img_Forms.BackgroundColor=Colors.DarkGray.ToFormsColor();		
			}
			else
			{
				img_Forms.IsEnabled=false;
				img_Forms.BackgroundColor=Colors.Blue.ToFormsColor();
			}
			SetExpert ("4");
		}
		void OnInsightTapped(View view, object sender)
		{
			if(img_Insights.IsEnabled==false)
			{
				img_Insights.IsEnabled=true;
				img_Insights.BackgroundColor=Colors.DarkGray.ToFormsColor();	
			}
			else
			{
				img_Insights.IsEnabled=false;
				img_Insights.BackgroundColor=Colors.Blue.ToFormsColor();
			}
			SetExpert ("5");
		}
		void OnTestCloudTapped(View view, object sender)
		{
			if(img_Testcloud.IsEnabled==false)
			{
				img_Testcloud.IsEnabled=true;
				img_Testcloud.BackgroundColor=Colors.DarkGray.ToFormsColor();	
			}
			else
			{
				img_Testcloud.IsEnabled=false;
				img_Testcloud.BackgroundColor=Colors.Blue.ToFormsColor();	
			}
			SetExpert ("6");
		}
		void OnCertifiedTapped(View view, object sender)
		{
			if(img_Certified.IsEnabled==false)
			{
				img_Certified.IsEnabled=true;
				img_Certified.BackgroundColor=Colors.DarkGray.ToFormsColor();
			}
			else
			{
				img_Certified.IsEnabled=false;
				img_Certified.BackgroundColor=Colors.Blue.ToFormsColor();
			}
			SetExpert ("2");
		}
		void OnCameraTapped(View view, object sender)
		{
			ViewModel.TakePictureCommand.Execute(null);
			image_bg.IsVisible=false;

		}
		void OnGalleryTapped(View view, object sender)
		{
			ViewModel.SelectPictureCommand.Execute(null);
			image_bg.IsVisible=false;

		}
		private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			Device.BeginInvokeOnMainThread (() => {
				if (e.PropertyName == "PickerItems" && ViewModel.PickerItems != null && ViewModel.PickerItems.Count > 0) {
					for (int i = 0; i < ViewModel.PickerItems.Count; i++) 
					{
						picker_Country.Items.Add (ViewModel.PickerItems [i].name); // You can show anything like I'm showing Name: ViewModel.PickerItems[i].ID | ViewModel.PickerItems[i].Abbr
					}
					 //We are telling user that It's a Picker "Select Item"
					ViewModel.SelectedIndex = 0;


				}
			});
		}

	}
}


