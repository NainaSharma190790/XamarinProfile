using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Colors = XamarinProfile.Helpers.Color;

namespace XamarinProfile
{
	public class UserCellView : ViewCell
	{
		public string source="";
		public Label expert;
		public UserCellView()
		{
			View = CreateCellView();//CreateCell(null);

		}
		public View CreateCellView()
		{

			var nameLabel = new Label();
			nameLabel.SetBinding(Label.TextProperty, "name");
			nameLabel.TextColor=Colors.DarkGray.ToFormsColor();

			var locationLabel = new Label();
			locationLabel.SetBinding(Label.TextProperty,"location");
			locationLabel.TextColor=Colors.DarkGray.ToFormsColor();

			expert = new Label();
			expert.SetBinding(Label.TextProperty, "expert");

			var profileImage = new CircleImage();
			profileImage.Aspect=Aspect.AspectFill;
			//profileImage.Source = "img1.png";
			profileImage.SetBinding(CircleImage.SourceProperty, "image", BindingMode.Default);


			var retView = new StackLayout 
			{
				Padding = new Thickness (3, 3, 0, 3),
				BackgroundColor = Color.White,
				Orientation = StackOrientation.Horizontal,
				Children = {
					profileImage,
					new StackLayout {
						VerticalOptions = LayoutOptions.Center,
						Spacing = 0,
						Children = {
							nameLabel,
							locationLabel
						}
					},
					GenEventTablelGrid ()

				}
			};
			return retView;

		}
		private Grid GenEventTablelGrid()
		{
			var grid = new Grid()
			{
				ColumnSpacing = 3,
				RowSpacing = 3,	
			};

			//var p =  new UserRegistrationRequest();
			var myString=expert.Text;
			string[] stringArray = myString.Split (",".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
			int i = 0;
			int j = 0;
			for (int x = 0; x < stringArray.Length; x++) 
			{
				int ID = Convert.ToInt32( stringArray [x]);
				if (i < 3) {
					grid.Children.Add (GetImage (ID), i, j);
					i++;
				} else {
					i = 0;
					j = 1;
					grid.Children.Add (GetImage (ID), i, j);
					i++;
				}
			}
			return grid;
		}

		private Image GetImage(int ExpertID)
		{
			switch (ExpertID) {
			case 1:
				source = "iOS.png";
				break;
			case 2:
				source = "Certified.png";
				break;
			case 3:
				source = "Android.png";
				break;
			case 4:
				source = "Forms.png";
				break;
			case 5:
				source = "Insight.png";
				break;
			case 6:
				source = "TestCloud.png";
				break;

			}
			var image = new Image () {
				Source = source,
				HeightRequest = App.ScreenWidth / 12,
				Aspect = Aspect.AspectFill,
				BackgroundColor = Colors.Green.ToFormsColor ()
			};

			return image;
		}
	}
}