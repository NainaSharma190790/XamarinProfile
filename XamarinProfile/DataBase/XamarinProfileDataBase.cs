using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace XamarinProfile
{
	public class XamarinProfileDataBase
	{
		#region Private Declarations

		static object locker = new object();

		SQLiteConnection iiDatabase; //To use gllobally in this class, create tabel, add update data.

		#endregion

		#region Database path

		string DatabasePath
		{
			get
			{
				var sqliteFilename = "XamarinProfile.db3";
				#if __IOS__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder C:\ddddd
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder C:\dddd\...\library
				var path = Path.Combine(libraryPath, sqliteFilename); //c:\ddddd\...\library\XamarinProfile.db3
				#else
				#if __ANDROID__
				string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
				var path = Path.Combine(documentsPath, sqliteFilename);
				#else
				// WinPhone
				var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);;
				#endif
				#endif
				return path;
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the  XamarinProfile Database. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public XamarinProfileDataBase()
		{
			iiDatabase = new SQLiteConnection(DatabasePath);
		}

		#endregion

		#region Create Tables

		public void CreateTables<T>()
		{
			iiDatabase.CreateTable<T>();
		}

		#endregion

		#region Get All Items

		public ObservableCollection<T> GetItems<T>() where T : IBusinessBase, new()
		{
			ObservableCollection<T> data = new ObservableCollection<T>();
			lock (locker)
			{
				var dt = (from i in iiDatabase.Table<T>()
					select i).ToList();
				dt.ForEach((x) =>
					{
						data.Add(x);
					});
				return data;
			}
		}

		#endregion

		#region Get Single Item

		public T GetItem<T>(int id) where T : IBusinessBase, new()
		{
			lock (locker)
			{
				return iiDatabase.Table<T>().FirstOrDefault(x => x.ItemID == id);
			}
		}

		#endregion

		#region Save Record in DB

		public int SaveItem<T>(T item) where T : IBusinessBase, new()
		{
			lock (locker)
			{
				if (item.ItemID > 0)
				{
					iiDatabase.Update(item);
					return item.ItemID;
				}
				else
				{
					return iiDatabase.Insert(item);
				}
			}
		}

		#endregion

		#region Delete Record From DB

		public int DeleteItem<T>(int id) where T : IBusinessBase, new()
		{
			lock (locker)
			{
				return iiDatabase.Delete<T>(id);
			}
		}

		#endregion
	}
}
