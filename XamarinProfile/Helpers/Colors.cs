using System;

namespace XamarinProfile.Helpers
{
	public struct Color
	{
		public static readonly Color DarkGray = 0x2c3e50;
		public static readonly Color Green = 0x4d9f8c;
		public static readonly Color Blue = 0x2F91D3;

		public double R, G, B;

		public static Color FromHex(int hex)
		{
			Func<int, int> at = offset => (hex >> offset) & 0xFF;
			return new Color
			{
				R = at(16) / 255.0,
				G = at(8) / 255.0,
				B = at(0) / 255.0
			};
		}

		public static implicit operator Color(int hex)
		{
			return FromHex(hex);
		}

		public Xamarin.Forms.Color ToFormsColor()
		{
			return Xamarin.Forms.Color.FromRgb((int)(255 * R), (int)(255 * G), (int)(255 * B));
		}

		#if __ANDROID__
		public global::Android.Graphics.Color ToAndroidColor()
		{
		return global::Android.Graphics.Color.Rgb((int)(255 * R), (int)(255 * G), (int)(255 * B));
		}

		public static implicit operator global::Android.Graphics.Color(Color color)
		{
		return color.ToAndroidColor();
		}
		#endif
	}
}

