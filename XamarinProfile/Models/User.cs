using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using SQLite;

namespace XamarinProfile
{
	public class User
	{
		
	}


	public class UserRegistrationResponse
	{
		public string Status { get; set; }
		public string StatusCode { get; set; }
		public string Message { get; set; }
		public UserRegistrationPayload payload { get; set; }
	}
	public class UserRegistrationPayload
	{
		public string name { get; set; }
		public string email { get; set; }
		public string location { get; set; }
		public string image { get; set; }
		public string experts { get; set; }
		public string password { get; set; }

	}
	public class UserRegistrationRequest : BaseModel
	{
		public string name { get; set; }
		public string email { get; set; }
		public string location { get; set; }
		public string image { get; set; }
		public string experts { get; set; }
		public string password { get; set; }
	}



	public class CheckoutCountry : BaseModel
	{
		[PrimaryKey]
		public override int ItemID
		{
			get
			{
				return base.ItemID;
			}
			set
			{
				base.ItemID = value;
			}
		}
		public int id { get; set; }
		public string abbr { get; set; }
		public string name { get; set; }

	}
}

