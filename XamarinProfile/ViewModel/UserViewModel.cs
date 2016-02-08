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
using Android.Content.Res;
using Newtonsoft.Json;

namespace XamarinProfile
{
	public class UserViewModel:BaseViewModel
	{

		public UserViewModel()
		{
			Setup();
			UserRegInfo = new UserRegistrationRequest(); //{ first_name = "Mohd", last_name = "Riyaz", email = "test@test.com", mobileno = "0987654321", password = "test123#", city = "", address = "test road, test" };
			GetCountriesCommand.Execute(null);
			//CountryCategories = new ObservableCollection<Country>();


		}

//		private ObservableCollection<Country> _countryCategories;
//
//		public ObservableCollection<Country> CountryCategories
//		{
//			get { return _countryCategories; }
//			set { _countryCategories = value; OnPropertyChanged(); }
//		}	
		private UserRegistrationRequest _userRegInfo;

		public UserRegistrationRequest UserRegInfo
		{
			get { return _userRegInfo; }
			set
			{
				_userRegInfo = value;
				OnPropertyChanged();
			}
		}

		private string _expertValue;

		public string ExpertValue
		{
			get { return _expertValue; }
			set { _expertValue = value; OnPropertyChanged(); }
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

		public ICommand GetCountriesCommand
		{
			get
			{
				return new Command (async () =>
					{
						
						await ServiceHandler.PostGetData<CountriesResponse<CountryPicker>, string> (ServiceHandler.Constant.Countries, HttpMethod.Get, null).ContinueWith (t => 
							{
								if (!t.IsFaulted && t.Result != null)
								{
									PickerItems = t.Result.PayloadData.Countries.ToList();

								}
							});
					});
			}
		}


		public ICommand RegisterUser
		{
			get
			{
				return new Command (async (args) => 
					{	
						UserRegInfo.experts=ExpertValue;
						UserRegInfo.location=SelectedIndex.ToString();
						#region Service to register a user
						await ServiceHandler.PostGetData<UserRegistrationResponse, UserRegistrationRequest>(ServiceHandler.Constant.RegisterUser, HttpMethod.Post, UserRegInfo).ContinueWith(t =>
							{
								try
							{
								if (!t.IsFaulted && t.Result != null)
								{
									if (t.Result.StatusCode == "1") 
									{
										Console.WriteLine ("Success");
										Navigation.PushModalAsync(new UserListView());
									} 
									else
									{
										Console.WriteLine ("Error");
									}
								}
							} 
							catch (Exception ex)
							{

							}
						});
						#endregion	
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
		private ImageSource _imageSource = "iOS.png";


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
			}
			catch (System.Exception ex)
			{
				Status = ex.Message;
			}
		}

		#endregion
	}
}