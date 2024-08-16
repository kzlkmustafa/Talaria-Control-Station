using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Talaria.Models;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;

namespace Talaria
{
    /// <summary>
    /// Interaction logic for ArasControl.xaml
    /// </summary>
    public partial class ArasControl : UserControl
    {
        private static Brush[] ArasBackground = {
            (Brush)new BrushConverter().ConvertFromString("#23FB00"),
            (Brush)new BrushConverter().ConvertFromString("#f7716e")
        };

        public static string[] myerror =
        {
            "Uydu iniş hızı 12-14 m/s dışında",
            "Görev yükü iniş hızı 6-8 m/s dışında",
            "Taşıyıcı basınç verisi alınamıyor",
            "Görev yükü konum verisi alınamıyor",
            "Ayrılma gerçekleşmiyor"
        };

        public ArasControl()
        {
            InitializeComponent();
        }
        public MyAras ArasData
        {
            get { return (MyAras)GetValue(ArasDataProperty); }
            set
            {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(() => SetValue(ArasDataProperty, value));
                }
                else
                {
                    SetValue(ArasDataProperty, value);
                }
            }
        }

        public static readonly DependencyProperty ArasDataProperty =
            DependencyProperty.Register("PersonData", typeof(MyAras), typeof(ArasControl), new PropertyMetadata(null, OnArasChanged));

        private static void OnArasChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ArasControl control)
            {
                var info = e.NewValue as MyAras;

                if (info == null || info.ErrorCode == null || info.ErrorCode.Length != 5 || !info.ErrorCode.All(c => c == '0' || c == '1'))
                {
                    control.Dispatcher.Invoke(() =>
                    {
                        control.ArasError.Text = "Geçersiz ARAS hata kodu formatı. Lütfen porttan gelen veriyi kontrol edin.";
                        control.FirstArasColor.Background = Brushes.Transparent;
                        control.SecondArasColor.Background = Brushes.Transparent;
                        control.ThirdArasColor.Background = Brushes.Transparent;
                        control.FourthArasColor.Background = Brushes.Transparent;
                        control.FifthArasColor.Background = Brushes.Transparent;
                        control.ManuelLeaving.IsEnabled = false;
                    });
                    return;
                }


                int[] result = new int[info.ErrorCode.Length];

                for (int i = 0; i < info.ErrorCode.Length; i++)
                {
                    result[i] = int.Parse(info.ErrorCode[i].ToString());
                }


                control.Dispatcher.Invoke(() =>
                {
                    if (result != null && result.Length > 0)
                    {
                        var backgroundControls = new[]
                        {
                        control.FirstArasColor,
                        control.SecondArasColor,
                        control.ThirdArasColor,
                        control.FourthArasColor,
                        control.FifthArasColor
                        };
                        for (int i = 0; i < result.Length && i < backgroundControls.Length; i++)
                        {
                            backgroundControls[i].Background = ArasBackground[result[i]];
                        }

                        string errors = "";

                        for (int i = 0; i < result.Length && i < myerror.Length; i++)
                        {
                            if (result[i] == 1)
                            {
                                if (errors != "")
                                {
                                    errors += ", ";
                                }
                                errors += myerror[i];
                            }
                        }

                        control.ManuelLeaving.IsEnabled = (result[4] == 1);

                        if (string.IsNullOrEmpty(errors))
                        {
                            errors = "Tüm sistemler normal.";
                        }

                        control.ArasError.Text = errors;
                    }
                });
                
            }
        }




        private void ManuelLeaving_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
