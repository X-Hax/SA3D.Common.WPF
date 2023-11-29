using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SA3D.Common.WPF.Utilities
{
	/// <summary>
	/// Interaction logic for ColorPicker.xaml
	/// </summary>
	public partial class ColorPickerControl : UserControl
	{
		/// <summary>
		/// Currently picked color property.
		/// </summary>
		public static DependencyProperty ColorProperty
			= DependencyProperty.Register(
				nameof(Color),
				typeof(Color),
				typeof(ColorPickerDialog),
				new((d, p) =>
				{
					ColorPickerControl picker = (ColorPickerControl)d;
					Color oldColor = (Color)p.OldValue;
					Color newColor = (Color)p.NewValue;

					picker.ColorChangedEvent?.Invoke(picker, oldColor, newColor);
				}));

		private readonly SolidColorBrush _pink = new(Colors.Pink);
		private readonly SolidColorBrush _white = new(Colors.White);

		private bool _selectingColor;
		private bool _manual;

		/// <summary>
		/// Currently picked color.
		/// </summary>
		public Color Color
		{
			get => (Color)GetValue(ColorProperty);
			set
			{
				SetValue(ColorProperty, value);
				ManualUpdateFromSet();
			}
		}


		/// <summary>
		/// Event that gets invoked when the color changes.
		/// </summary>
		public ColorChangedEventHandler? ColorChangedEvent;

		/// <summary>
		/// Creates a new color picker with a starting color.
		/// </summary>
		/// <param name="color">The color to start with.</param>
		public ColorPickerControl(Color color)
		{
			InitializeComponent();

			_manual = true;

			Color = color;

			RedSlider.Value = color.R;
			GreenSlider.Value = color.G;
			BlueSlider.Value = color.B;
			AlphaSlider.Value = color.A;

			RedNumber.Value = color.R;
			GreenNumber.Value = color.G;
			BlueNumber.Value = color.B;
			AlphaNumber.Value = color.A;

			UpdateHSVFromRGB();
			UpdateText();
			UpdateVisualPicker();

			HueNumber.Value = HueSlider.Value;
			SaturationNumber.Value = SaturationSlider.Value;
			ValueNumber.Value = ValueSlider.Value;


			RedSlider.ValueChanged += (s, e) =>
			{
				Color c = Color;
				c.R = (byte)e.NewValue;
				Color = c;
				RedNumber.Value = c.R;
				ManualUpdateHSVFromRGB();
			};

			GreenSlider.ValueChanged += (s, e) =>
			{
				Color c = Color;
				c.G = (byte)e.NewValue;
				Color = c;
				GreenNumber.Value = c.G;
				ManualUpdateHSVFromRGB();
			};

			BlueSlider.ValueChanged += (s, e) =>
			{
				Color c = Color;
				c.B = (byte)e.NewValue;
				Color = c;
				BlueNumber.Value = c.B;
				ManualUpdateHSVFromRGB();
			};

			AlphaSlider.ValueChanged += (s, e) =>
			{
				if(_manual)
				{
					return;
				}

				_manual = true;

				Color c = Color;
				c.A = (byte)e.NewValue;
				Color = c;
				AlphaNumber.Value = c.A;

				UpdateVisualPicker();
				UpdateText();

				_manual = false;
			};

			HueSlider.ValueChanged += (s, e) =>
			{
				ManualUpdateRGBFromHSV();
				HueNumber.Value = e.NewValue;
			};

			SaturationSlider.ValueChanged += (s, e) =>
			{
				ManualUpdateRGBFromHSV();
				SaturationNumber.Value = e.NewValue;
			};

			ValueSlider.ValueChanged += (s, e) =>
			{
				ManualUpdateRGBFromHSV();
				ValueNumber.Value = e.NewValue;
			};

			RedNumber.ValueChanged += (s, e) =>
			{
				byte value = e.NewValue ?? 0;
				if(value != RedSlider.Value)
				{
					RedSlider.Value = value;
				}
			};

			GreenNumber.ValueChanged += (s, e) =>
			{
				byte value = e.NewValue ?? 0;
				if(value != GreenSlider.Value)
				{
					GreenSlider.Value = value;
				}
			};

			BlueNumber.ValueChanged += (s, e) =>
			{
				byte value = e.NewValue ?? 0;
				if(value != BlueSlider.Value)
				{
					BlueSlider.Value = value;
				}
			};

			AlphaNumber.ValueChanged += (s, e) =>
			{
				byte value = e.NewValue ?? 0;
				if(value != AlphaSlider.Value)
				{
					AlphaSlider.Value = value;
				}
			};

			HueNumber.ValueChanged += (s, e) =>
			{
				double value = e.NewValue ?? 0;
				if(value != HueSlider.Value)
				{
					HueSlider.Value = value;
				}
			};

			SaturationNumber.ValueChanged += (s, e) =>
			{
				double value = e.NewValue ?? 0;
				if(value != SaturationSlider.Value)
				{
					SaturationSlider.Value = value;
				}
			};

			ValueNumber.ValueChanged += (s, e) =>
			{
				double value = e.NewValue ?? 0;
				if(value != ValueSlider.Value)
				{
					ValueSlider.Value = value;
				}
			};

			HexTextBox.TextChanged += (s, e) => UpdateFromText();

			_manual = false;
		}

		/// <summary>
		/// Creates a new color picker that starts with <see cref="Colors.Red"/>.
		/// </summary>
		public ColorPickerControl() : this(Colors.Red) { }

		private void ManualUpdateFromSet()
		{
			if(_manual)
			{
				return;
			}

			_manual = true;
			UpdateRGBSliders();
			UpdateAlphaSlider();
			UpdateHSVFromRGB();

			UpdateText();
			UpdateVisualPicker();
			_manual = false;
		}

		private void ManualUpdateRGBFromHSV()
		{
			if(_manual)
			{
				return;
			}

			_manual = true;
			UpdateRGBFromHSV();

			UpdateText();
			UpdateVisualPicker();
			_manual = false;
		}

		private void ManualUpdateHSVFromRGB()
		{
			if(_manual)
			{
				return;
			}

			_manual = true;
			UpdateHSVFromRGB();

			UpdateText();
			UpdateVisualPicker();
			_manual = false;
		}

		private void UpdateRGBSliders()
		{
			Color color = Color;

			RedSlider.Value = color.R;
			GreenSlider.Value = color.G;
			BlueSlider.Value = color.B;
		}

		private void UpdateAlphaSlider()
		{
			AlphaSlider.Value = Color.A;
		}

		private void UpdateRGBFromHSV()
		{
			float h60 = (float)HueSlider.Value * 6;
			int hi = Convert.ToInt32(Math.Floor(h60)) % 6;
			double f = h60 - Math.Floor(h60);

			float val = (float)ValueSlider.Value * 255;
			float s = (float)SaturationSlider.Value;

			byte v = (byte)val;
			byte p = (byte)(val * (1 - s));
			byte q = (byte)(val * (1 - (f * s)));
			byte t = (byte)(val * (1 - ((1 - f) * s)));

			byte a = Color.A;

			Color = hi switch
			{
				0 => Color.FromArgb(a, v, t, p),
				1 => Color.FromArgb(a, q, v, p),
				2 => Color.FromArgb(a, p, v, t),
				3 => Color.FromArgb(a, p, q, v),
				4 => Color.FromArgb(a, t, p, v),
				_ => Color.FromArgb(a, v, p, q),
			};

			UpdateRGBSliders();
		}

		private void UpdateHSVFromRGB()
		{
			Color oc = Color;
			System.Drawing.Color c = System.Drawing.Color.FromArgb(oc.A, oc.R, oc.G, oc.B);

			byte max = Math.Max(oc.R, Math.Max(oc.G, oc.B));
			byte min = Math.Min(oc.R, Math.Min(oc.G, oc.B));

			HueSlider.Value = c.GetHue() / 360f;
			SaturationSlider.Value = (max == 0) ? 0 : 1f - (min / (float)max);
			ValueSlider.Value = max / 255f;
		}

		private void UpdateText()
		{
			Color c = Color;
			HexTextBox.Text = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.R, c.G, c.B, c.A);
			HexTextBox.Background = _white;
		}

		private void UpdateFromText()
		{
			if(_manual)
			{
				return;
			}

			_manual = true;

			string hex = HexTextBox.Text.Replace(" ", "");
			hex = hex.StartsWith("#") ? hex[1..] : hex;

			// check if the format is valid
			if(hex.Length < 3 || hex.Length == 5 || hex.Length == 7 || hex.Length > 8
				|| !Regex.IsMatch(hex, "^[0-9a-fA-F]+$"))
			{
				HexTextBox.Background = _pink;
				_manual = false;
				return;
			}
			else
			{
				HexTextBox.Background = _white;
			}

			byte conv(char character)
			{
				return (byte)(character >= 'A' ? character - 'A' + 10 : character - '0');
			}

			byte comp(byte f, byte s)
			{
				return (byte)(f | (s << 4));
			}

			hex = hex.ToUpper();
			byte r, g, b, a;

			switch(hex.Length)
			{
				case 3: // RGB
					r = conv(hex[0]);
					g = conv(hex[1]);
					b = conv(hex[2]);
					a = byte.MaxValue;

					r = comp(r, r);
					g = comp(g, g);
					b = comp(b, b);
					break;
				case 4: // RGBA
					r = conv(hex[0]);
					g = conv(hex[1]);
					b = conv(hex[2]);
					a = conv(hex[3]);

					r = comp(r, r);
					g = comp(g, g);
					b = comp(b, b);
					a = comp(a, a);
					break;
				case 6: // RRGGBB
					r = comp(conv(hex[1]), conv(hex[0]));
					g = comp(conv(hex[3]), conv(hex[2]));
					b = comp(conv(hex[5]), conv(hex[4]));
					a = byte.MaxValue;
					break;
				case 8: // RRGGBBAA
					r = comp(conv(hex[1]), conv(hex[0]));
					g = comp(conv(hex[3]), conv(hex[2]));
					b = comp(conv(hex[5]), conv(hex[4]));
					a = comp(conv(hex[7]), conv(hex[6]));
					break;
				default:
					throw new InvalidOperationException();
			}

			Color = Color.FromArgb(a, r, g, b);

			AlphaSlider.Value = a;
			UpdateHSVFromRGB();
			UpdateRGBFromHSV();
			UpdateVisualPicker();

			_manual = false;
		}

		private void UpdateVisualPicker()
		{
			float h60 = (float)HueSlider.Value * 6;
			int hi = (int)Math.Floor(h60) % 6;
			double f = h60 - Math.Floor(h60);

			byte q = (byte)(255 * (1 - f));
			byte t = (byte)(255 * f);

			PickerBG.Background = new SolidColorBrush(hi switch
			{
				0 => Color.FromArgb(255, 255, t, 0),
				1 => Color.FromArgb(255, q, 255, 0),
				2 => Color.FromArgb(255, 0, 255, t),
				3 => Color.FromArgb(255, 0, q, 255),
				4 => Color.FromArgb(255, t, 0, 255),
				_ => Color.FromArgb(255, 255, 0, q),
			});

			int xOffset = (int)((SaturationSlider.Value * 255) + 7);
			int yOffset = (int)((ValueSlider.Value * 255) + 7);

			PickPos.Margin = new(xOffset, 0, 0, yOffset);

			Color transparent = Color;
			Color opaque = Color.FromArgb(255, transparent.R, transparent.G, transparent.B);

			ColorDisplay.Fill = new LinearGradientBrush(opaque, transparent, new(0.33d, 0), new(0.66d, 0));
		}


		private void PickerBG_MouseDown(object sender, MouseButtonEventArgs e)
		{
			_selectingColor = true;
		}

		private void PickerBG_MouseMove(object sender, MouseEventArgs e)
		{
			if(!_selectingColor)
			{
				return;
			}

			Point pos = Mouse.GetPosition(PickerBG);
			int posX = (int)Math.Clamp(pos.X, 0, 255);
			int posY = 255 - (int)Math.Clamp(pos.Y, 0, 255);

			SaturationSlider.Value = posX / 255f;
			ValueSlider.Value = posY / 255f;

			UpdateRGBFromHSV();
		}

		private void PickerBG_MouseUp(object sender, MouseButtonEventArgs e)
		{
			_selectingColor = false;
		}

		private void PickerBG_MouseLeave(object sender, MouseEventArgs e)
		{
			_selectingColor = false;
		}

	}
}
