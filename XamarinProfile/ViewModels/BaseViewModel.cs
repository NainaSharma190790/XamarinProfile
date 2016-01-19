using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace XamarinProfile
{
	public class BaseViewModel: INotifyPropertyChanged
	{

		public bool IsNetworkConnected
		{
			get
			{
				DependencyService.Get<INetworkConnection>().CheckNetworkConnection();
				//TODO: Have to remove 
				return DependencyService.Get<INetworkConnection>().IsConnected; 
			}
		}


		public BaseViewModel()
		{
		}

		private string title = string.Empty;
		public const string TitlePropertyName = "Title";
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value, TitlePropertyName); }
		}

		private string subTitle = string.Empty;
		public const string SubtitlePropertyName = "Subtitle";
		public string Subtitle
		{
			get { return subTitle; }
			set { SetProperty(ref subTitle, value, SubtitlePropertyName); }
		}

		private string icon = null;
		public const string IconPropertyName = "Icon";
		public string Icon
		{
			get { return icon; }
			set { SetProperty(ref icon, value, IconPropertyName); }
		}

		private bool isBusy;
		public const string IsBusyPropertyName = "IsBusy";
		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value, IsBusyPropertyName); }
		}

		private bool canLoadMore = true;
		public const string CanLoadMorePropertyName = "CanLoadMore";
		public bool CanLoadMore
		{
			get { return canLoadMore; }
			set { SetProperty(ref canLoadMore, value, CanLoadMorePropertyName); }
		}

		protected void SetProperty<T>(
			ref T backingStore, T value,
			string propertyName,
			Action onChanged = null,
			Action<T> onChanging = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return;

			if (onChanging != null)
				onChanging(value);

			backingStore = value;

			if (onChanged != null)
				onChanged();

			OnPropertyChanged(propertyName);
		}

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		public void OnPropertyChanged(string propertyName)
		{

			if (PropertyChanged == null)
				return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

		}

	}
}
