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

namespace XamarinProfile
{
	public class CameraViewModel:BaseViewModel
	{

		public CameraViewModel()
		{
			Setup();
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