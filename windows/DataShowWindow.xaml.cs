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
using System.Windows.Shapes;
using Talaria.Services;

namespace Talaria.Windows
{
    /// <summary>
    /// Interaction logic for DataShowWindow.xaml
    /// </summary>
    public partial class DataShowWindow : Window
    {
        
        private SerialPortServices _serialPortServices;
        string MyPortName = Application.Current.Properties["myPortName"] as string;

        private PortTestContextDb _dbContext;
        private SensorDataRepository _repository;
        public DataShowWindow()
        {
            InitializeComponent();
            LoadData();

            _dbContext = new PortTestContextDb();
            _repository = new SensorDataRepository(_dbContext);
        }
        private void LoadData()
        {
            _serialPortServices = new SerialPortServices(MyPortName, _repository);
            try
            {
                _serialPortServices.Open();
                MessageShow.Text += $"Switched to port {MyPortName}\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening port {MyPortName}: {ex.Message}");
            }
            _serialPortServices.DataReceivedString += OnDataReceived;
        }
        private void OnDataReceived(string myData)
        {
            Dispatcher.Invoke(() =>
            {
                PortDataShow.Text += myData;
            });
        }
        protected override void OnClosed(EventArgs e)
        {
            if (_serialPortServices != null)
            {
                _serialPortServices.Close();
            }


            base.OnClosed(e);
        }
    }
}
