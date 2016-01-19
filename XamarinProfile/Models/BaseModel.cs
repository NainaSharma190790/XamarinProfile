using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XamarinProfile
{
	public class BaseModel : IBusinessBase, INotifyPropertyChanged, INotifyPropertyChanging
	{
		#region Constructor
		public BaseModel()
		{

		}
		#endregion

		#region IBusinessBase implementation
		[PrimaryKey, AutoIncrement]
		public int ItemID
		{
			get;
			set;
		}
		#endregion

		#region Public Properties
		public int ID { get; set; }
		public string Title { get; set; }

		public DateTime CreatedOn { get; set; }

		public string CreatedByID { get; set; }


		public DateTime ModifiedOn { get{return System.DateTime.Now;}  }

		public string ModifiedByID { get { return "Me"; } }
		#endregion

		#region  INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		public void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}


		#region INotifyPropertyChanging implementation
		public event PropertyChangingEventHandler PropertyChanging;
		#endregion

		public void OnPropertyChanging(string propertyName)
		{
			if (PropertyChanging == null)
				return;

			PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
		}
	}
}
