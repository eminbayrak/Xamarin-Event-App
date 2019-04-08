using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitialsView : ContentView
    {
        const float TextOffset = 1.0f;

        public InitialsView()
        {
            InitializeComponent();
        }

        void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;

            var canvas = surface.Canvas;

            canvas.Clear();

            var circleFill = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = GetColorForText()
            };

            canvas.DrawCircle((float)Height, (float)Height, (float)Height, circleFill);

            var textPaint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColors.White,
                TextSize = (float)Height / TextOffset,
                TextAlign = SKTextAlign.Center,
                Typeface = SKTypeface.FromFamilyName(FontFamily)
            };

            canvas.DrawText(Text, (float)Height, (float)Height * TextOffset, textPaint);
        }

        public SKColor GetColorForText()
        {
            //TODO: determine color based on Text property
            string Letters = "abcdefghijklmnopqrstvuvwxyz";
            Char[] ColorArray = Letters.ToCharArray();

            foreach (Char c in ColorArray)
            {
                switch (c)
                {
                    case 'a':
                        return SKColors.Orange;
                    case 'b':
                        return SKColors.MediumVioletRed;
                    case 'c':
                        return SKColors.CadetBlue;
                    case 'd':
                        return SKColors.GreenYellow;
                    case 'e':
                        return SKColors.PaleTurquoise;
                }
            }
            return SKColors.Gray;
        }

        #region Bindable properties

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(InitialsView), string.Empty,
                                    BindingMode.OneWay, null, (bindable, oldValue, newValue) =>
                                    {
                                        var view = (bindable as InitialsView);
                                        view.ForceLayout();
                                    });

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create("FontFamily", typeof(string), typeof(InitialsView), string.Empty,
                                    BindingMode.OneWay, null, (bindable, oldValue, newValue) =>
                                    {
                                        var view = (bindable as InitialsView);
                                        view.ForceLayout();
                                    });

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        #endregion
    }
}