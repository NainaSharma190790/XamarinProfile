﻿using System;
using Xamarin.Forms;
using System.Collections;
using System.Collections.Specialized;
using XLabs.Forms.Controls;

namespace XamarinProfile
{
	public class ExtendedPicker : Picker
	{
		public static readonly BindableProperty HasBorderProperty =
			BindableProperty.Create<ExtendedPicker, bool>(p => p.HasBorder, false);

		public bool HasBorder
		{
			get { return (bool)GetValue (HasBorderProperty); }
			set { SetValue (HasBorderProperty, value); }
		}

		public ExtendedPicker()
		{
			this.SelectedIndexChanged += OnSelectedIndexChanged;
		}

		public static BindableProperty ItemsSourceProperty =
			BindableProperty.Create<ExtendedPicker, IEnumerable>(o => o.ItemsSource, default(IEnumerable), propertyChanged: OnItemsSourceChanged);

		public static BindableProperty SelectedItemProperty =
			BindableProperty.Create<ExtendedPicker, object>(o => o.SelectedItem, default(object), propertyChanged: OnSelectedItemChanged);


		public IEnumerable ItemsSource
		{
			get { return (IEnumerable)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public object SelectedItem
		{
			get { return (object)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		private static void OnItemsSourceChanged(BindableObject bindable, IEnumerable oldvalue, IEnumerable newvalue)
		{
			var picker = bindable as ExtendedPicker;
			picker.Items.Clear();
			if (newvalue != null)
			{
				//now it works like "subscribe once" but you can improve
				foreach (var item in newvalue)
				{
					picker.Items.Add(item.ToString());
				}
			}
		}

		private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
		{
			if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
			{
				SelectedItem = null;
			}
			else
			{
				SelectedItem = Items[SelectedIndex];
			}
		}
		private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var picker = bindable as ExtendedPicker;
			if (newvalue != null)
			{
				picker.SelectedIndex = picker.Items.IndexOf(newvalue.ToString());
			}
		}
	}
}