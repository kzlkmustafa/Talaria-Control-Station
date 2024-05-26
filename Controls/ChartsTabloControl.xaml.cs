using LiveCharts;
using LiveCharts.Wpf;
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
using System.Windows.Threading;
using Talaria.Models;

namespace Talaria
{
    /// <summary>
    /// Interaction logic for ChartsTabloControl.xaml
    /// </summary>
    public partial class ChartsTabloControl : UserControl
    {
        public SeriesCollection Chart1Series { get; set; }
        public SeriesCollection Chart2Series { get; set; }
        public SeriesCollection Chart3Series { get; set; }
        public SeriesCollection Chart4Series { get; set; }
        public SeriesCollection Chart5Series { get; set; }
        public SeriesCollection Chart6Series { get; set; }
        public SeriesCollection Chart7Series { get; set; }
        public SeriesCollection Chart8Series { get; set; }
        public ChartLastValueModel Tempature { get; set; }
        public ChartLastValueModel BatteryVoltage { get; set; }
        public ChartLastValueModel Pressure1 { get; set; }
        public ChartLastValueModel Pressure2 { get; set; }
        public ChartLastValueModel DescentSpeed { get; set; }
        public ChartLastValueModel Height1 { get; set; }
        public ChartLastValueModel AltitudeDif { get; set; }
        public ChartLastValueModel Height2 { get; set; }
        public ChartsTabloControl()
        {
            InitializeComponent();

            DataContext = this;


            Chart1Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { },
                    Fill = null
                }
            };
            Chart2Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { },
                    Fill = null
                }
            };
            Chart3Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { },
                    Fill = null
                }
            };
            Chart4Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { },
                    Fill = null
                }
            };
            Chart5Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { },
                    Fill = null
                }
            };
            Chart6Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { },
                    Fill = null
                }
            };
            Chart7Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { },
                    Fill = null
                }
            };
            Chart8Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { },
                    Fill = null
                }
            };


            Tempature = new ChartLastValueModel
            {
                value = "Sıcaklık",
                type = "°C"
            };
            BatteryVoltage = new ChartLastValueModel
            {
                value = "Pil Gerilimi",
                type = "V"
            };
            Pressure1 = new ChartLastValueModel
            {
                value = "Taşıyıcı Basıncı",
                type = "Pa"
            };
            Pressure2 = new ChartLastValueModel
            {
                value = "Görev Yükü Basıncı",
                type = "Pa"
            };
            DescentSpeed = new ChartLastValueModel
            {
                value = "İniş Hızı",
                type = "m/s"
            };
            Height1 = new ChartLastValueModel
            {
                value = "Taşıyıcı Yüksekliği",
                type = "m"
            };
            AltitudeDif = new ChartLastValueModel
            {
                value = "İrtifa Farkı",
                type = "m"
            };
            Height2 = new ChartLastValueModel
            {
                value = "Görev Yükü Yüksekliği",
                type = "m"
            };
            


            var chartControls = new ChartControl[]
            {
                new ChartControl { MySeries = Chart1Series },
                new ChartControl { MySeries = Chart2Series },
                new ChartControl { MySeries = Chart3Series },
                new ChartControl { MySeries = Chart4Series },
                new ChartControl { MySeries = Chart5Series },
                new ChartControl { MySeries = Chart6Series },
                new ChartControl { MySeries = Chart7Series },
                new ChartControl { MySeries = Chart8Series },
            };

            foreach (var chartControl in chartControls)
            {
                chartControl.DataContext = chartControl.MySeries;
            }

        }


        


        public MyChartTablo ChartTabloData
        {
            get { return (MyChartTablo)GetValue(ChartTabloProperty); }
            set
            {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(() => SetValue(ChartTabloProperty, value));
                }
                else
                {
                    SetValue(ChartTabloProperty, value);
                }
            }
        }
        public static readonly DependencyProperty ChartTabloProperty =
            DependencyProperty.Register("PersonData", typeof(MyChartTablo), typeof(ChartsTabloControl), new PropertyMetadata(null, OnChartTabloChanged));

        private static void OnChartTabloChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is ChartsTabloControl control)
            {
                var info = e.NewValue as MyChartTablo;
                control.UpdateSeries(control.Chart1Series, control.ChartControl1, control.Tempature, info.tempature);
                control.UpdateSeries(control.Chart2Series, control.ChartControl2, control.BatteryVoltage, info.batteryVoltage);
                control.UpdateSeries(control.Chart3Series, control.ChartControl3, control.Pressure1, info.pressure1);
                control.UpdateSeries(control.Chart4Series, control.ChartControl4, control.Pressure2, info.pressure2);
                control.UpdateSeries(control.Chart5Series, control.ChartControl5, control.DescentSpeed, info.descentSpeed);
                control.UpdateSeries(control.Chart6Series, control.ChartControl6, control.Height1, info.height1);
                control.UpdateSeries(control.Chart7Series, control.ChartControl7, control.AltitudeDif, info.altitudeDif);
                control.UpdateSeries(control.Chart8Series, control.ChartControl8, control.Height2, info.height2);
            }
        }
        private void UpdateSeries(SeriesCollection seriesCollection, ChartControl chartControl, ChartLastValueModel chartModel, object newData)
        {
            var lineSeries = seriesCollection[0] as LineSeries;
            if (lineSeries != null)
            {
                var values = lineSeries.Values as ChartValues<double>;
                if (values != null)
                {
                    double newValue = Convert.ToDouble(newData); // Gelen yeni veriyi double'a dönüştürün
                    values.Add(newValue); // Yeni değeri ekleyin
                    if (values.Count > 20) // Maksimum veri noktası sayısını sınırlayın
                    {
                        values.RemoveAt(0);
                    }
                    chartControl.UpdateLatestValue(chartModel.value, newValue, chartModel.type);
                }
            }

        }
    }
}
