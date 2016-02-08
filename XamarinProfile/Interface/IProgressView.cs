using System;

namespace XamarinProfile
{

	public interface IProgressView
	{
		void Show(string message);
		void ShowToast(string message);
		void Hide();
	}
}
