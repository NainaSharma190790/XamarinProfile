using System;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace XamarinProfile
{
	public class CountryPicker : BaseModel
	{
		[JsonProperty("id")]
		public int id { get; set; }

		[JsonProperty("name")]
		public string name { get; set; }

		[JsonProperty("abbr")]
		public string abbr { get; set; }

	}

	public class Payload<T>
	{
		public Payload()
		{
			Countries = new ObservableCollection<T>();
		}
		[JsonProperty("countries")]
		public ObservableCollection<T> Countries { get; set; }
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


}
