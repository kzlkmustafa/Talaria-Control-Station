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

namespace Talaria
{
    /// <summary>
    /// Interaction logic for GMapTabloControl.xaml
    /// </summary>
    public partial class GMapTabloControl : UserControl
    {
        public MapValueModel myMapValue { get; set; }
        public GMapTabloControl()
        {
            InitializeComponent();

            myMapValue = new MapValueModel
            {
                latitude = 39.9384,
                longitude = 32.8189,
                height = 200,
            };

            InitializeMap();
            UpdateMapInfoValue(myMapValue);
        }
        private void InitializeMap()
        {
            MainMap.MapProvider = GMapProviders.GoogleMap;
            MainMap.Position = new PointLatLng(myMapValue.latitude, myMapValue.longitude); // Statik konum (Ankara)
            MainMap.MinZoom = 2;
            MainMap.MaxZoom = 18;
            MainMap.Zoom = 14;
            MainMap.ShowCenter = false;

            // Statik konum için marker ekleme
            var marker = new GMapMarker(new PointLatLng(myMapValue.latitude, myMapValue.longitude))
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

        public void UpdateMapInfoValue(MapValueModel mapValueModel)
        {
            latitudetxt.Text = $"Enlem: {mapValueModel.latitude}";
            longitudetxt.Text = $"Boylam: {mapValueModel.longitude}";
            heighttxt.Text = $"Yükseklik: {mapValueModel.height}";
        }
    }
}
