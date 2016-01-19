using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace XamarinProfile
{
	public static class ServiceHandler
	{
		static object lockObj;
		static ServiceHandler()
		{
		}

		#region Process Request Async : MAIN

		public static async Task<ObservableCollection<T>> ProcessRequestCollectionAsync<T>(string resource, T value = default(T)) where T : class, new()
		{
			HttpWebRequest request;
			request = (HttpWebRequest)WebRequest.Create(CreateURI(resource));
			request.Method = "GET";
			string responseStr;
			try
			{
				using (var response = await request.GetResponseAsync())
				{
					using (Stream data = response.GetResponseStream())
					{
						responseStr = new StreamReader(data).ReadToEnd();
						ObservableCollection<T> resData = JsonConvert.DeserializeObject<ObservableCollection<T>>(responseStr);
						return resData;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("An error occured in Service Handler {0}",ex.Message);
				return null;
			}
		}

		public static async Task<T> ProcessRequestItemAsync<T>(string resource, T value = default(T)) where T : class, new()
		{
			HttpWebRequest request;
			request = (HttpWebRequest)WebRequest.Create(CreateURI(resource));
			request.Method = "GET";
			string responseStr;
			try
			{
				using (var response = await request.GetResponseAsync())
				{
					using (Stream data = response.GetResponseStream())
					{
						responseStr = new StreamReader(data).ReadToEnd();
						T resData = JsonConvert.DeserializeObject<T>(responseStr);
						return resData;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("An error occured in Service Handler", ex.Message);
				return null;
			}
		}

		static string CreateURI(string res)
		{
			return Constants.APIUrl + res;
		}
		#endregion

		#region Post Request Async : MAIN

		/// <summary>
		/// Post data on server asynchronously
		/// </summary>
		/// <typeparam name="Tr">Object to get response</typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="resource"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static async Task<Tr> PostDataAsync<Tr, T>(T data, string resource, T value = default(T)) //where T : class, new()
		{
			string reqUrl = CreateURI(resource);
			var jData = JsonConvert.SerializeObject(data);
			byte[] postdata = Encoding.UTF8.GetBytes(jData);
			HttpWebRequest request;
			request = (HttpWebRequest)WebRequest.Create(reqUrl);
			request.Method = "POST";
			request.ContentType = "application/json";

			//Write data
			using (Stream myStream = await request.GetRequestStreamAsync())
			{
				myStream.Write(postdata, 0, postdata.Length);
				myStream.Dispose();
			}

			// Get response
			Tr responseStr = default(Tr);
			try
			{
				using (var response = await request.GetResponseAsync())
				{
					using (Stream stData = response.GetResponseStream())
					{
						responseStr = JsonConvert.DeserializeObject<Tr>(new StreamReader(stData).ReadToEnd());
						//return Convert.Int32(responseStr.);
					}
				}
				return responseStr;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Exception in Service Handler : " + ex.Message);
				return responseStr;
			}
		}

		#endregion
	}

	#region WebRequest Extention Method


	public static class WebRequestExtensions
	{
		public static Task<HttpWebResponse> GetResponseAsync(this HttpWebRequest request)
		{
			var tcs = new TaskCompletionSource<HttpWebResponse>();

			try
			{
				request.BeginGetResponse(iar =>
					{
						try
						{
							var response = (HttpWebResponse)request.EndGetResponse(iar);
							tcs.SetResult(response);
						}
						catch (Exception exc)
						{
							tcs.SetException(exc);
						}
					}, null);
			}
			catch (Exception exc)
			{
				tcs.SetException(exc);
			}

			return tcs.Task;
		}

		public static Task<Stream> GetRequestStreamAsync(this HttpWebRequest request)
		{
			var tcs = new TaskCompletionSource<Stream>();

			try
			{
				request.BeginGetRequestStream(iar =>
					{
						try
						{
							var response = request.EndGetRequestStream(iar);
							tcs.SetResult(response);
						}
						catch (Exception exc)
						{
							tcs.SetException(exc);
						}
					}, null);
			}
			catch (Exception exc)
			{
				tcs.SetException(exc);
			}

			return tcs.Task;
		}
	}

	#endregion
}
