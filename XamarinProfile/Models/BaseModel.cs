using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;

namespace XamarinProfile
{
	public class BaseModel : IBusinessBase, INotifyPropertyChanged
	{
		#region IBusinessBase implementation

		[PrimaryKey, AutoIncrement]
		public virtual int ItemID
		{
			get;
			set;
		}
		#endregion

		#region Property Changed Implementation

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}
}