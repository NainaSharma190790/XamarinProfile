using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Colors = XamarinProfile.Helpers.Color;
namespace XamarinProfile
{
	public class UserListView : ContentPage
	{
		class Person
		{
			public Person(string name, string location, string profilePicture)
			{
				this.Name = name;
				this.Location = location;
				this.ProfilePicture = profilePicture;
			}

			public string Name { private set; get; }

			public string Location { private set; get; }

			public string ProfilePicture { private set; get; }
		};
		public int Height = App.ScreenHeight;
		public int Width = App.ScreenWidth;
		public Image img_iOS, img_Android, img_Forms,img_Testcloud,img_Insights,img_Certified;

		public UserListView()
		{
			Label header = new Label
			{
				Text = "ListView",
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				HorizontalOptions = LayoutOptions.Center
			};
			img_Testcloud = new Image
			{
				Source = "TestCloud.png",
				HeightRequest = Height/15,
				WidthRequest = Height/15,
			BackgroundColor= Colors.Green.ToFormsColor()
			};
			img_Insights = new Image
			{
				Source = "Insight.png",
				HeightRequest = Height/15,
				WidthRequest = Height/15,
			BackgroundColor= Colors.Green.ToFormsColor()
			};
			img_Certified = new Image
			{
				Source = "Certified.png",
				HeightRequest = Height/15,
				WidthRequest = Height/15,
			BackgroundColor= Colors.Green.ToFormsColor()
			};
			img_iOS = new Image
			{
				Source = "iOS.png",
				HeightRequest = Height/15,
				WidthRequest = Height/15,
			BackgroundColor= Colors.Green.ToFormsColor()
			};
			img_Android = new Image
			{
				Source = "Android.png",
				HeightRequest = Height/15,
				WidthRequest = Height/15,
			BackgroundColor= Colors.Green.ToFormsColor()
			};
			img_Forms = new Image
			{
				Source = "Forms.png",
				HeightRequest = Height/15,
				WidthRequest = Height/15,
			BackgroundColor= Colors.Green.ToFormsColor()
			};

			StackLayout FirstExpertLayout = new StackLayout
			{
				Orientation= StackOrientation.Horizontal,
				Spacing=Height/40,
				HorizontalOptions=LayoutOptions.End,
				VerticalOptions=LayoutOptions.Start,
				//TranslationY=Height/40,
				Children=
				{
					img_iOS,img_Android,img_Forms
				}
			};
			StackLayout SecondExpertLayout = new StackLayout
			{
				Orientation= StackOrientation.Horizontal,
				HorizontalOptions=LayoutOptions.End,
				VerticalOptions=LayoutOptions.Start,
				Spacing=Height/40,

				//TranslationY=Height/40,
				Children=
				{
					img_Testcloud,img_Insights,img_Certified
				}
				};
			StackLayout ExpertLayout = new StackLayout
			{
				Orientation= StackOrientation.Vertical,
				HorizontalOptions=LayoutOptions.EndAndExpand,
				Padding=new Thickness(0,0,Height/20,0),
				VerticalOptions=LayoutOptions.Center,
				Spacing=Height/40,
				Children=
				{
					FirstExpertLayout,
					SecondExpertLayout		
				}
				};

			// Define some data.
			List<Person> people = new List<Person>
			{
				new Person("Rob","USA","img1.png"),
				new Person("Nav", "USA","img2.png"),
				// ...etc.,...
				new Person("Ruby", "India", "img3.png"),
				new Person("James", "India", "img4.png"),
				new Person("Rob","USA","img5.png"),
				new Person("Rob","USA","img1.png"),
				new Person("Nav", "USA","img2.png"),
				// ...etc.,...
				new Person("Ruby", "India", "img3.png"),
				new Person("James", "India", "img4.png"),
				new Person("Rob","USA","img5.png"),
				new Person("Rob","USA","img1.png"),
				new Person("Nav", "USA","img2.png"),
				// ...etc.,...
				new Person("Ruby", "India", "img3.png"),
				new Person("James", "India", "img4.png"),
				new Person("Rob","USA","img5.png")
			};

			// Create the ListView.
			CustomListView listView = new CustomListView
			{
				// Source of data items.
				ItemsSource = people,
				RowHeight=Height/7,
				VerticalOptions=LayoutOptions.FillAndExpand,
				BackgroundColor=Colors.DarkGray.ToFormsColor(),
				SeparatorColor=Colors.Green.ToFormsColor(),
				SeparatorVisibility =SeparatorVisibility.Default,
				// Define template for displaying each item.
				// (Argument of DataTemplate constructor is called for 
				//      each item; it must return a Cell derivative.)
				ItemTemplate = new DataTemplate(() =>
					{
						// Create views with bindings for displaying each property.
						Label nameLabel = new Label();
						nameLabel.SetBinding(Label.TextProperty, "Name");
						nameLabel.TextColor=Colors.DarkGray.ToFormsColor();

						Label locationLabel = new Label();
						locationLabel.SetBinding(Label.TextProperty,"Location");
						locationLabel.TextColor=Colors.DarkGray.ToFormsColor();


						CircleImage profileImage = new CircleImage();
						//profileImage.HeightRequest=(Height/5)-10;
						profileImage.Aspect=Aspect.AspectFill;
						profileImage.SetBinding(Image.SourceProperty, "ProfilePicture");


						// Return an assembled ViewCell.
						return new ViewCell
						{
							View = new StackLayout
							{
								Padding = new Thickness(3,3,0,3),
								BackgroundColor=Color.White,
								Orientation = StackOrientation.Horizontal,
								Children = 
								{
									profileImage,
									new StackLayout
									{
										VerticalOptions = LayoutOptions.Center,
										Spacing = 0,
										Children = 
										{
											nameLabel,
											locationLabel
										}
										},
									ExpertLayout
								}
								}
						};
					})
			};

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(3, 3, 3, 3);
			this.BackgroundColor = Colors.DarkGray.ToFormsColor();
			// Build the page.
			this.Content = new StackLayout
			{
				BackgroundColor=Colors.DarkGray.ToFormsColor(),
				Children = 
				{
					//header,
					listView
				}
				};
		}
	}
}