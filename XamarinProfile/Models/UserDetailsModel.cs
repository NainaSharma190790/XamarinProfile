using System;

namespace XamarinProfile
{
	public class UserDetailsModel: BaseModel
	{
		#region Full Property

		private string _userProfileImage = string.Empty;

		public virtual string UserProfileImage
		{
			get { return _userProfileImage; }
			set { _userProfileImage = value; OnPropertyChanged("UserProfileImage"); }
		}

		private int _userID;
		public virtual int UserID
		{
			get { return _userID; }
			set { _userID = value; OnPropertyChanged("UserID"); }
		}

		private string _userFName;
		public string UserFName
		{
			get { return _userFName; }
			set { _userFName = value; OnPropertyChanged("UserFName"); }
		}

		private string _userLName;
		public string UserLName
		{
			get { return _userLName; }
			set { _userLName = value; OnPropertyChanged("UserLName"); }
		}


		private string _userLocation = string.Empty;
		public string UserLocation
		{
			get { return _userLocation; }
			set { _userLocation = value; OnPropertyChanged("UserLocation"); }
		}

		private bool _iOSExpert;
		public bool iOSExpert
		{
			get { return _iOSExpert; }
			set { _iOSExpert = value; OnPropertyChanged("iOSExpert"); }
		}

		private bool _androidExpert;
		public bool AndroidExpert
		{
			get { return _androidExpert; }
			set { _androidExpert = value; OnPropertyChanged("AndroidExpert"); }
		}

		private bool _testCloudExpert;
		public bool TestCloudExpert
		{
			get { return _testCloudExpert; }
			set { _testCloudExpert = value; OnPropertyChanged("TestCloudExpert"); }
		}

		private bool _insightsExpert;
		public bool InsightsExpert
		{
			get { return _insightsExpert; }
			set { _insightsExpert = value; OnPropertyChanged("InsightsExpert"); }
		}

		private bool _certifiedExpert;
		public bool CertifiedExpert
		{
			get { return _certifiedExpert; }
			set { _certifiedExpert = value; OnPropertyChanged("CertifiedExpert"); }
		}
		private bool _formsExpert;
		public bool FormsExpert
		{
			get { return _formsExpert; }
			set { _formsExpert = value; OnPropertyChanged("FormsExpert"); }
		}
		#endregion
	}
}
