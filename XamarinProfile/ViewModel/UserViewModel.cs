using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using XLabs.Platform.Services.Media;
using XLabs.Ioc;
using System.Windows.Input;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;

namespace XamarinProfile
{
	public class UserViewModel:BaseViewModel
	{
		private INavigation _navigation; // HERE

		public UserViewModel(INavigation navigation) 
		{
			_navigation = navigation;
			Setup();
			UserRegInfo = new UserRegistrationPayload(); //{ first_name = "Mohd", last_name = "Riyaz", email = "test@test.com", mobileno = "0987654321", password = "test123#", city = "", address = "test road, test" };
			_userDetails =  new ObservableCollection<UserRegistrationRequest>();
			UserListInfo = new UserRegistrationRequest();
			GetCountriesCommand.Execute(null);
			//GetAllUsers.Execute(null);
		}


		public string imageString;
		private UserRegistrationPayload _userRegInfo;

		public UserRegistrationPayload UserRegInfo
		{
			get { return _userRegInfo; }
			set
			{
				_userRegInfo = value;
				OnPropertyChanged();
			}
		}
		private UserRegistrationRequest _userListInfo;

		public UserRegistrationRequest UserListInfo
		{
			get { return _userListInfo; }
			set
			{
				_userListInfo = value;
				OnPropertyChanged();
			}
		}

		private ImageSource _imageTest;
		public ImageSource ImageTest
		{
			get
			{
				return _imageTest;
			}
			set
			{
				_imageTest = value;
				OnPropertyChanged("ImageTest");
			}
		}
		private string _expertValue;

		public string ExpertValue
		{
			get { return _expertValue; }
			set { _expertValue = value; OnPropertyChanged(); }
		}

		private Image _profilePicture;

		public Image ProfilePicture
		{
			get { return _profilePicture; }
			set { _profilePicture = value; OnPropertyChanged(); }
		}

		private IList<CountryPicker> _pickerItems;
		public IList<CountryPicker> PickerItems
		{
			get { return _pickerItems; }
			set { _pickerItems = value; OnPropertyChanged(); }
		}


		private CountryPicker _selectedPickerItem;
		public CountryPicker SelectedPickerItem
		{
			get { return _selectedPickerItem; }
			set { _selectedPickerItem = value; OnPropertyChanged(); }
		}

		private int _selectedIndex;
		public int SelectedIndex
		{
			get { return _selectedIndex; }
			set
			{
				_selectedIndex = value; OnPropertyChanged();

				if (SelectedIndex > 0) // Using this condition we are making sure that we selecting valid Values!
				{
					SelectedPickerItem = PickerItems[SelectedIndex];
				}
			}
		}
		public CheckLogin CheckLogin
		{
			get { return  new CheckLogin { UserName = this.Username, Password = this.Password }; }
		}

		private string _userName="naina.sharma@netsmartz.net";

		public string Username
		{
			get { return _userName; }
			set { _userName = value; OnPropertyChanged("Username"); }
		}

		private string _password="123";

		public string Password
		{
			get { return _password; }
			set { _password = value; OnPropertyChanged("Password"); }
		}
		private bool isLoading;
		public bool IsLoading
		{
			get { return isLoading; }
			set
			{
				isLoading = value;
				OnPropertyChanged();
			}
		}


		private ObservableCollection<UserRegistrationRequest> _userDetails;

		public ObservableCollection<UserRegistrationRequest> UserDetails
		{
			get { return _userDetails; }
			set
			{
				_userDetails = value;
				OnPropertyChanged("UserDetails");
			}
		}
		public ICommand GetCountriesCommand
		{
			get
			{
				return new Command (async () =>
					{
						IsLoading=true;
						await ServiceHandler.PostGetData<CountriesResponse<CountryPicker>, string> (ServiceHandler.Constant.Countries, HttpMethod.Get, null).ContinueWith (t => 
							{
								if (!t.IsFaulted && t.Result != null)
								{
									PickerItems = t.Result.PayloadData.Countries.ToList();
									IsLoading=false;
								}
							});
					});
			}
		}


		public ICommand GetAllUsers {
			get {

				return new Command (async (args) => {
					IsLoading = true;
					int count;
					await ServiceHandler.PostGetData<UserRegistration<UserRegistrationRequest>, string> (ServiceHandler.Constant.AllUser, HttpMethod.Get, null).ContinueWith (t =>
						{
							try
							{
							if (!t.IsFaulted && t.Result != null)
							{
								UserDetails.Clear();
								var items = t.Result.PayloadData.GetUser;
								foreach (var item in items)
								{
									item.image="http://research.demo.netsmartz.us/webservices/images/"+item.image;
									UserListInfo.experts=item.experts;
									count=Convert.ToInt32(item.location);
									item.location=PickerItems[count].name;
									UserDetails.Add(item);
								}
								IsLoading = false;
							}
							}
							catch(Exception ex)
							{
								
							}

						});
				});
			}
		}

		public ICommand RegisterUser 
		{
			get 
			{
				return new Command (async (args) => {	
					UserRegInfo.experts = ExpertValue;
					UserRegInfo.location = SelectedIndex.ToString ();
					UserRegInfo.image = imageString;
					#region Service to register a user
					IsLoading = true;

						await ServiceHandler.PostGetData<UserRegistrationResponse, UserRegistrationPayload> (ServiceHandler.Constant.RegisterUser, HttpMethod.Post, UserRegInfo).ContinueWith (t =>
							{								
							if (!t.IsFaulted && t.Result != null)
							{
								if (t.Result.StatusCode == "1") {
									IsLoading = false;
									Console.WriteLine ("Success");
									_navigation.PushModalAsync (new UserListView ());
						
								} else {
									Console.WriteLine ("Error");
								}
							}
						
						
						},
							TaskScheduler.FromCurrentSynchronizationContext ());
					
					#endregion	
				});

			}
		}


		public ICommand LoginUser {
			get {

				return new Command (async (args) => {
					IsLoading = true;
					try
					{
						await ServiceHandler.PostGetData<UserRegistrationResponse, CheckLogin> (ServiceHandler.Constant.Login, HttpMethod.Post, CheckLogin).ContinueWith (t =>
							{

								if (!t.IsFaulted && t.Result != null) {
									if (t.Result.StatusCode == "1") {
										IsLoading = false;
										Console.WriteLine ("Success");
										_navigation.PushAsync(new UserRegistrationView ());

									} else {
										Console.WriteLine ("Error");
									}
								}


							},
							TaskScheduler.FromCurrentSynchronizationContext ());
					}
					catch (Exception ex) 
					{
						Console.WriteLine (ex.Message);
					}
				});
			}
		}



		#region Functionality of profile picture

		/// <summary>
		/// The _scheduler.
		/// </summary>
		private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();

		/// <summary>
		/// The picture chooser.
		/// </summary>
		private IMediaPicker _mediaPicker;

		/// <summary>
		/// The image source.
		/// </summary>
		private ImageSource _imageSource="CircleImage.png";


		/// <summary>
		/// The take picture command.
		/// </summary>
		private Command _takePictureCommand;

		/// <summary>
		/// The select picture command.
		/// </summary>
		private Command _selectPictureCommand;


		private string _status;


		////private CancellationTokenSource cancelSource;

		/// <summary>
		/// Initializes a new instance of the <see cref="CameraViewModel" /> class.
		/// </summary>
		private void Setup()
		{
			if (_mediaPicker != null)
			{
				return;
			}

			// var device = Resolver.Resolve<IDevice>();

			////RM: hack for working on windows phone? 
			_mediaPicker = Resolver.Resolve<IMediaPicker>();// ?? device.MediaPicker;
		}

		/// <summary>
		/// Gets or sets the image source.
		/// </summary>
		/// <value>The image source.</value>
		public ImageSource ImageSource
		{
			get
			{
				return _imageSource;
			}
			set
			{
				_imageSource = value;
				OnPropertyChanged("ImageSource");
			}
		}
		// <summary>
		/// Gets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public string Status
		{
			get { return _status; }
			private set
			{
				_status = value;
				OnPropertyChanged("Status");
			}
		}

		/// <summary>
		/// Gets the take picture command.
		/// </summary>
		/// <value>The take picture command.</value>
		public Command TakePictureCommand
		{
			get
			{
				return _takePictureCommand ?? (_takePictureCommand = new Command(
					async () => await TakePicture(),
					() => true));
			}
		}

		/// <summary>
		/// Takes the picture.
		/// </summary>
		/// <returns>Take Picture Task.</returns>
		private async Task<MediaFile> TakePicture()
		{
			Setup();

			ImageSource = null;

			return await _mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions { DefaultCamera = CameraDevice.Front, MaxPixelDimension = 400 }).ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						Status = t.Exception.InnerException.ToString();
					}
					else if (t.IsCanceled)
					{
						Status = "Canceled";
					}
					else
					{
						var mediaFile = t.Result;

						ImageSource = ImageSource.FromStream(() => mediaFile.Source);
						FileStream fs = new FileStream(mediaFile.Path, FileMode.Open,FileAccess.Read);
						byte[] ImageData = new byte[fs.Length];
						fs.Read(ImageData,0,System.Convert.ToInt32(fs.Length));
						fs.Close();
						imageString = Convert.ToBase64String (ImageData);

						return mediaFile;
					}

					return null;
				}, _scheduler);
		}



		/// <summary>
		/// Gets the select picture command.
		/// </summary>
		/// <value>The select picture command.</value>
		public Command SelectPictureCommand
		{
			get
			{
				return _selectPictureCommand ?? (_selectPictureCommand = new Command(
					async () => await SelectPicture(),
					() => true));
			}
		}

		public MediaFile m_ediaFile { get; set; }

		/// <summary>
		/// Selects the picture.
		/// </summary>
		/// <returns>Select Picture Task.</returns>
		private async Task SelectPicture()
		{
			Setup();

			//ImageSource = null;
			try
			{
				var mediaFile = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
					{
						DefaultCamera = CameraDevice.Front,
						MaxPixelDimension = 400
					});
				ImageSource = ImageSource.FromStream(() => mediaFile.Source);

				//Convert image to string
				FileStream fs = new FileStream(mediaFile.Path, FileMode.Open,FileAccess.Read);
				byte[] ImageData = new byte[fs.Length];
				fs.Read(ImageData,0,System.Convert.ToInt32(fs.Length));
				fs.Close();
				imageString = Convert.ToBase64String (ImageData);

				//Convert string to image

				Byte[] ImageFotoBase64 = System.Convert.FromBase64String(imageString); 
				ImageTest = ImageSource.FromStream(() => new MemoryStream(ImageFotoBase64)); 

			}
			catch (System.Exception ex)
			{
				Status = ex.Message;
			}
		}


		#endregion
		//		public System.Drawing.Image Base64ToImage()   
		//		{  
		//			byte[] imageBytes = Convert.FromBase64String(imageString);  
		//			MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);  
		//			ms.Write(imageBytes, 0, imageBytes.Length);  
		//			System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);  
		//			return image;  
		//		} 
		//
		//		public Image test = (Image)Base64ToImage();
	}
}