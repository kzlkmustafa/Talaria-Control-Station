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

namespace Talaria
{
    /// <summary>
    /// Interaction logic for ArasControl.xaml
    /// </summary>
    public partial class ArasControl : UserControl
    {
        private static Brush[] ArasBackground = {
            (Brush)new BrushConverter().ConvertFromString("#23FB00"),
            (Brush)new BrushConverter().ConvertFromString("#f7716e")};

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
                int[] result = new int[info.ErrorCode.Length];

                for (int i = 0; i < info.ErrorCode.Length; i++)
                {
                    result[i] = int.Parse(info.ErrorCode[i].ToString());
                }
                control.Dispatcher.Invoke(() =>
                {

                    if (result != null && result.Length > 0)
                    {
                        control.FirstArasColor.Background = ArasBackground[result[0]];
                        if (result.Length > 1) control.SecondArasColor.Background = ArasBackground[result[1]];
                        if (result.Length > 2) control.ThirdArasColor.Background = ArasBackground[result[2]];
                        if (result.Length > 3) control.FourthArasColor.Background = ArasBackground[result[3]];
                        if (result.Length > 4) control.FifthArasColor.Background = ArasBackground[result[4]];

                        string errors = "";
                        
                        if (result[0] == 0) errors += myerror[0];
                        if (result.Length > 1 && result[1] == 0) errors += (errors == "" ? "" : ", ") + myerror[1];
                        if (result.Length > 2 && result[2] == 0) errors += (errors == "" ? "" : ", ") + myerror[2];
                        if (result.Length > 3 && result[3] == 0) errors += (errors == "" ? "" : ", ") + myerror[3];
                        if (result.Length > 4 && result[4] == 0) errors += (errors == "" ? "" : ", ") + myerror[4];

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
