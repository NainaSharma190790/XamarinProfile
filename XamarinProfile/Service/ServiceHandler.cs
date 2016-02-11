using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinProfile
{
	public class ServiceHandler
	{
		internal class Constant
		{
		public const string HostName = "http://research.demo.netsmartz.us/webservices/?action=";
			public const string RegisterUser = "register";
			public const string Login = "login";
			public const string Countries = "countries";

		}

		public static async Task<Tr> PostGetData<Tr, T>(string endpoint, HttpMethod method,
			T content)
		{
			Tr returnResult = default(Tr);

			HttpClient client = new HttpClient();
			try
			{
				client.BaseAddress = new Uri(Constant.HostName);
				client.DefaultRequestHeaders.Add("Accept", "application/json");

				client.Timeout = new TimeSpan(0, 0, 15);

				HttpResponseMessage result = null;

				StringContent data = null;
				if (content != null)
					data = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

				var apiUri = new Uri(string.Format("{0}{1}", Constant.HostName, endpoint));

				if (method == HttpMethod.Get)
					result = await client.GetAsync(apiUri);

				if (method == HttpMethod.Put)
					result = await client.PutAsync(apiUri, data);

				if (method == HttpMethod.Delete)
					result = await client.DeleteAsync(endpoint);

				if (method == HttpMethod.Post)
					result = await client.PostAsync(apiUri, data);

				if (result != null)
				{
					if (result.IsSuccessStatusCode
						&& result.StatusCode == System.Net.HttpStatusCode.OK)
					{
						var json = result.Content.ReadAsStringAsync().Result;
						returnResult = JsonConvert.DeserializeObject<Tr>(json);
					}
				}

			}
			catch (Exception ex)
			{
				DependencyService.Get<IProgressView>().Hide();
				Debug.WriteLine("Error fetching data: " + ex.Message);
			}
			finally
			{
				if (client != null)
					client.Dispose();
			}

			return returnResult;
		}
	}
}
