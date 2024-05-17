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
using Talaria.Models;

namespace Talaria
{
    /// <summary>
    /// Interaction logic for ChartControl.xaml
    /// </summary>
    public partial class ChartControl : UserControl
    {
        public SeriesCollection MySeries { get; set; }
        public ChartControl()
        {
            InitializeComponent();
            DataContext = this;
        }
        public void UpdateLatestValue(string Name,double value, string type)
        {
            LatestValueTextBlock.Text = $"{Name}: {value}  {type}";
        }
        

    }
}
