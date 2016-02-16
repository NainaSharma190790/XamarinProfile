using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using SQLite;

namespace XamarinProfile
{
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

	public class Payload<T>
	{
		public Payload()
		{
			Countries = new ObservableCollection<T>();
			GetUser = new ObservableCollection<T>();
		}
		[JsonProperty("countries")]
		public ObservableCollection<T> Countries { get; set; }
		[JsonProperty("allUsers")]
		public ObservableCollection<T> GetUser { get; set; }
	}
	public class CountriesResponse<T>
	{
		public CountriesResponse()
		{
			PayloadData = new Payload<T>();
		}
		[JsonProperty("status")]
		public string Status { get; set; }
		[JsonProperty("statusCode")]
		public int StatusCode { get; set; }
		[JsonProperty("message")]
		public string Message { get; set; }
		[JsonProperty("payload")]
		public Payload<T> PayloadData { get; set; }
	}

	public class UserRegistration<T>
	{
		public UserRegistration()
		{
			PayloadData = new Payload<T>();
		}
		[JsonProperty("status")]
		public string Status { get; set; }
		[JsonProperty("statusCode")]
		public int StatusCode { get; set; }
		[JsonProperty("message")]
		public string Message { get; set; }
		[JsonProperty("payload")]
		public Payload<T> PayloadData { get; set; }
	}


	public class UserRegistrationRequest : BaseModel
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string name { get; set; }

		[JsonProperty("email")]
		public string email { get; set; }

		[JsonProperty("location")]
		public string location { get; set; }

		[JsonProperty("image")]
		public string image { get; set; }

		[JsonProperty("experts")]
		public string experts { get; set; }

		[JsonProperty("password")]
		public string password { get; set; }
	}
	public class CountryPicker : BaseModel
	{
		[JsonProperty("id")]
		public int id { get; set; }

		[JsonProperty("name")]
		public string name { get; set; }

		[JsonProperty("abbr")]
		public string abbr { get; set; }

	}


}