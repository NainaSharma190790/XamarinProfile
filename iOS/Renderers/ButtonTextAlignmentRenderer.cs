using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using XamarinProfile;
using XamarinProfile.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(ButtonTextAlignment), typeof(ButtonTextAlignmentRenderer))]
namespace XamarinProfile.iOS
{
	public class ButtonTextAlignmentRenderer:ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
			{
				Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
				Control.ContentEdgeInsets = new UIEdgeInsets(0, 10, 0, 10);
			}
		}
	}
}


