﻿using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace RosyChopper.Converters
{
	public class InverseBooleanToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return !(bool)value ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return (Visibility)value != Visibility.Visible;
		}
	}
}