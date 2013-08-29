using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RosyChopper.MicrosoftSucks
{
	public class ImmediateUpdateSourceTextBox : TextBox
	{
		public static readonly new DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof(string),
				typeof(ImmediateUpdateSourceTextBox),
				new PropertyMetadata(default(string), OnTextChanged));

		public ImmediateUpdateSourceTextBox()
		{
			base.TextChanged += (s, e) =>
			{
				this.Text = base.Text;
			};
		}

		public new string Text
		{
			get
			{
				return (string)this.GetValue(TextProperty);
			}
			set
			{
				this.SetValue(TextProperty, value);
			}
		}

		private static void OnTextChanged(DependencyObject d,
			DependencyPropertyChangedEventArgs e)
		{
			var txt = d as TextBox;
			txt.Text = (string)e.NewValue;
		}
	}
}