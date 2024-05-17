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
        public ChartLastValueModel MyChartInfo1 { get; set; }
        public ChartLastValueModel MyChartInfo2 { get; set; }
        public ChartLastValueModel MyChartInfo3 { get; set; }
        public ChartLastValueModel MyChartInfo4 { get; set; }
        public ChartLastValueModel MyChartInfo5 { get; set; }
        public ChartLastValueModel MyChartInfo6 { get; set; }
        public ChartLastValueModel MyChartInfo7 { get; set; }
        public ChartLastValueModel MyChartInfo8 { get; set; }
        public ChartsTabloControl()
        {
            InitializeComponent();

            DataContext = this;


            Chart1Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 2, 1, 3, 5, 3, 4, 6 },
                    Fill = null
                }
            };
            Chart2Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 2, 1, 3, 5, 3, 4, 6 },
                    Fill = null
                }
            };
            Chart3Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 2, 1, 3, 5, 3, 4, 6 },
                    Fill = null
                }
            };
            Chart4Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 2, 1, 3, 5, 3, 4, 6 },
                    Fill = null
                }
            };
            Chart5Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 2, 1, 3, 5, 3, 4, 6 },
                    Fill = null
                }
            };
            Chart6Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 2, 1, 3, 5, 3, 4, 6 },
                    Fill = null
                }
            };
            Chart7Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 2, 1, 3, 5, 3, 4, 6 },
                    Fill = null
                }
            };
            Chart8Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 2, 1, 3, 5, 3, 4, 6 },
                    Fill = null
                }
            };


            MyChartInfo1 = new ChartLastValueModel
            {
                value = "Sıcaklık",
                type = "°C"
            };
            MyChartInfo2 = new ChartLastValueModel
            {
                value = "Pil Gerilimi",
                type = "V"
            };
            MyChartInfo3 = new ChartLastValueModel
            {
                value = "Taşıyıcı Basıncı",
                type = "Bar"
            };
            MyChartInfo4 = new ChartLastValueModel
            {
                value = "Görev Yükü Basıncı",
                type = "Bar"
            };
            MyChartInfo5 = new ChartLastValueModel
            {
                value = "İniş Hızı",
                type = "m/s"
            };
            MyChartInfo6 = new ChartLastValueModel
            {
                value = "Taşıyıcı Yüksekliği",
                type = "m"
            };
            MyChartInfo7 = new ChartLastValueModel
            {
                value = "İrtifa Farkı",
                type = "m"
            };
            MyChartInfo8 = new ChartLastValueModel
            {
                value = "Görev Yükü Yüksekliği",
                type = "m"
            };

            StartUpdatingSeries();


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


        private void StartUpdatingSeries()
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateSeries(Chart1Series, ChartControl1, MyChartInfo1);
            UpdateSeries(Chart2Series, ChartControl2, MyChartInfo2);
            UpdateSeries(Chart3Series, ChartControl3, MyChartInfo3);
            UpdateSeries(Chart4Series, ChartControl4, MyChartInfo4);
            UpdateSeries(Chart5Series, ChartControl5, MyChartInfo5);
            UpdateSeries(Chart6Series, ChartControl6, MyChartInfo6);
            UpdateSeries(Chart7Series, ChartControl7, MyChartInfo7);
            UpdateSeries(Chart8Series, ChartControl8, MyChartInfo8);
        }

        private void UpdateSeries(SeriesCollection seriesCollection, ChartControl chartControl, ChartLastValueModel chartModel)
        {
            var lineSeries = seriesCollection[0] as LineSeries;
            if (lineSeries != null)
            {
                var values = lineSeries.Values as ChartValues<double>;
                if (values != null)
                {
                    Random rnd = new Random();
                    double newValue = rnd.Next(0, 10);
                    values.Add(newValue); // Rastgele bir değer ekleyin
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
