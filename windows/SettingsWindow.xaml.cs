using System;
using System.Collections.Generic;
using System.IO.Ports;
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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SerialPort _serialPort;
        private SerialPortServices _serialPortServices;

        private PortTestContextDb _dbContext;
        private SensorDataRepository _repository;
        public SettingsWindow()
        {
            InitializeComponent();
            LoadAvailablePorts();

            _dbContext = new PortTestContextDb();
            _repository = new SensorDataRepository(_dbContext);
        }
        private void LoadAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();
            PortComboBox.ItemsSource = ports;
            if (ports.Any())
            {
                PortComboBox.SelectedIndex = 0; 
            }
        }
        private void SwitchPortButton_Click(object sender, RoutedEventArgs e)
        {

            string selectedPort = PortComboBox.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedPort))
            {
                //_serialPortServices = new SerialPortServices(selectedPort, _repository);
                
                try
                {
                   // _serialPortServices.Open();
                    Application.Current.Properties["myPortName"] = selectedPort;
                    ErrorTextBox.Text += $"Switched to port {selectedPort}\n";
                    if (_serialPortServices != null)
                    {

                        _serialPortServices.Close();
                        getUi();
                    }
                    else
                    {
                        getUi();
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening port {selectedPort}: {ex.Message}");
                }
            }
            
        }
        private void getUi()
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow == null)
            {
                mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                this.Close();
            }
            this.Close();
        }

        
    }
}
