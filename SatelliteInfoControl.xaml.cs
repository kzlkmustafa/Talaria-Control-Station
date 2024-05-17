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

namespace Talaria
{
    /// <summary>
    /// Interaction logic for SatelliteInfoControl.xaml
    /// </summary>
    public partial class SatelliteInfoControl : UserControl
    {
        private string myTeamNumber = "112343";
        public SatelliteInfoControl()
        {
            InitializeComponent();

            TeamNumber.Text = "Takım numarası: " + myTeamNumber;
            SatelliteStatus.Text = "Uydu Statüsü: ";
            LastCode.Text = "Son Gelen Veri Kodu: AS-123-XS";
            LastCodeTime.Text = "Veri Zamanı: 12:02 01/01/2024";
        }
    }
}
