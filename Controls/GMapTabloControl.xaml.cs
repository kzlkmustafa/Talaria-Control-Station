using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Net.NetworkInformation;

namespace Talaria
{
    /// <summary>
    /// Interaction logic for GMapTabloControl.xaml
    /// </summary>
    public partial class GMapTabloControl : UserControl
    {
        public GMapTabloControl()
        {
            InitializeComponent();


        }


        public MyGmap gmapData
        {
            get { return (MyGmap)GetValue(gmapDataProperty); }
            set
            {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(() => SetValue(gmapDataProperty, value));
                }
                else
                {
                    SetValue(gmapDataProperty, value);
                }
            }
        }

        public static readonly DependencyProperty gmapDataProperty =
            DependencyProperty.Register("PersonData", typeof(MyGmap), typeof(GMapTabloControl), new PropertyMetadata(null, OnGmapChanged));

        private static void OnGmapChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GMapTabloControl control)
            {
                var info = e.NewValue as MyGmap;
                control.Dispatcher.Invoke(() =>
                {
                    control.InitializeMap((double)info.gps1Latitude, (double)info.gps1Longitude);
                    control.UpdateMapInfoValue(info.gps1Latitude, info.gps1Longitude, info.gps1altitude);
                });
            }
        }




        private void InitializeMap(double mygps1Latitude, double mygps1Longitude)
        {
            MainMap.MapProvider = GMapProviders.GoogleMap;
            MainMap.Position = new PointLatLng(mygps1Latitude, mygps1Longitude); // Statik konum (Ankara)
            MainMap.MinZoom = 2;
            MainMap.MaxZoom = 18;
            MainMap.Zoom = 14;
            MainMap.ShowCenter = false;

            // Statik konum için marker ekleme
            var marker = new GMapMarker(new PointLatLng(mygps1Latitude, mygps1Longitude))
            {
                Shape = new System.Windows.Shapes.Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Stroke = System.Windows.Media.Brushes.Red,
                    StrokeThickness = 1.5,
                    Fill = System.Windows.Media.Brushes.Red
                }
            };
            MainMap.Markers.Add(marker);
        }

        public void UpdateMapInfoValue(float mylatitude, float mylongitude, float myheight)
        {
            latitudetxt.Text = $"Enlem: {mylatitude}";
            longitudetxt.Text = $"Boylam: {mylongitude}";
            heighttxt.Text = $"Yükseklik: {myheight}";
        }
    }
}
