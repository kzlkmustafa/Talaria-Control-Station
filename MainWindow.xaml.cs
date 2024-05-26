using AForge.Video.DirectShow;
using AForge.Video;
using System.Windows.Threading;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.Definitions.Charts;
using Talaria.Models;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GMap.NET;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Talaria.Services;

namespace Talaria
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPortServices _serialPortServices;
        string MyPortName = Application.Current.Properties["myPortName"] as string;

        private PortTestContextDb _dbContext;
        private SensorDataRepository _repository;
        public MainWindow()
        {
            InitializeComponent();

            _dbContext = new PortTestContextDb();
            _repository = new SensorDataRepository(_dbContext);

            LoadData();
        }
        private void LoadData()
        {
            _serialPortServices = new SerialPortServices(MyPortName, _repository);
            try
            {
                _serialPortServices.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening port {MyPortName}: {ex.Message}");
            }
            _serialPortServices.DataReceived += OnDataReceived;
        }
        private void OnDataReceived(SensorData myData)
        {
            MySatellite sensorSatellite = new MySatellite()
            {
                sendTime = myData.sendTime,
                TeamNumber = myData.TeamNumber,
                satelliteStatus = myData.satelliteStatus,
                packageNumber = myData.packageNumber
            };
            satelliteInfoControl.satelliteInfoData = sensorSatellite;
            
            MyAras sensorAras = new MyAras()
            {
                ErrorCode = myData.ErrorCode,
            };
            ArasDataControl.ArasData = sensorAras;

            MyChartTablo sensorChartTablo = new MyChartTablo()
            {
                tempature = myData.tempature,
                batteryVoltage = myData.batteryVoltage,
                pressure1 = myData.pressure1,
                pressure2 = myData.pressure2,
                descentSpeed = myData.descentSpeed,
                height1 = myData.height1,
                height2 = myData.height2,
                altitudeDif = myData.altitudeDif,
            };
            ChartsTabloDataControl.ChartTabloData = sensorChartTablo;

            MyGmap myGmap = new MyGmap()
            {
                gps1Latitude = myData.gps1Latitude,
                gps1Longitude = myData.gps1Longitude,
                gps1altitude = myData.gps1altitude
            };
            gmapDataControl.gmapData = myGmap;

            myRotationThreeD _myRotationThreeD = new myRotationThreeD()
            {
                roll = myData.roll,
                pitch = myData.pitch,
                yaw = myData.yaw,
            };
            threeDTabloDataControl.threeDTabloData = _myRotationThreeD;

            Dispatcher.Invoke(() =>
            {


                //DataTextBox.Text += $"{myData.packageNumber}: ";
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = 0;
            this.Top = 0;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
        }


    }
}