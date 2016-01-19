using System;

namespace XamarinProfile
{
	public interface INetworkConnection
	{
		//Ref : http://www.codeproject.com/Tips/870548/Xamarin-forms-Check-network-connectivity-in-iOS-an
		bool IsConnected { get; }

		/// <summary>
		/// This method will check Network availablity on basis of platform 
		/// </summary>
		void CheckNetworkConnection();
	}
}