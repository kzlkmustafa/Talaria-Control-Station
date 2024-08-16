using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using Talaria.Services;
using Talaria.Windows;
using System.IO;

namespace Talaria
{
    /// <summary>
    /// Interaction logic for SatelliteInfoControl.xaml
    /// </summary>
    public partial class SatelliteInfoControl : UserControl
    {
        private static string[] MyStatus = { "Uçuşa Hazır", "Yükselme", "Model Uydu İniş", "Ayrılma", "Görev Yükü İniş", "Kurtarma" };
        
        private PortTestContextDb _dbContext;
        private SensorDataRepository _repository;
        private SensorDataForExcel _forExcel;
        public SatelliteInfoControl()
        {
            _dbContext = new PortTestContextDb();
            _repository = new SensorDataRepository(_dbContext);

            InitializeComponent();

        }
        public MySatellite satelliteInfoData
        {
            get { return (MySatellite)GetValue(satelliteInfoDataProperty); }
            set {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(() => SetValue(satelliteInfoDataProperty, value));
                }
                else
                {
                    SetValue(satelliteInfoDataProperty, value);
                    //deneme
                }
            }
        }

        public static readonly DependencyProperty satelliteInfoDataProperty =
            DependencyProperty.Register("PersonData", typeof(MySatellite), typeof(SatelliteInfoControl), new PropertyMetadata(null, OnSatelliteChanged));

        private static void OnSatelliteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SatelliteInfoControl control)
            {
                var info = e.NewValue as MySatellite;
                control.Dispatcher.Invoke(() =>
                {   
                    control.TeamNumber.Text = "Takım numarası: " + info.TeamNumber;
                    if (info.satelliteStatus >= 0 && info.satelliteStatus < MyStatus.Length)
                    {
                        control.SatelliteStatus.Text = "Uydu Statüsü: " + MyStatus[info.satelliteStatus];
                    }
                    else
                    {
                        control.SatelliteStatus.Text = "Uydu Statüsü: Geçersiz ";
                    }
                        
                control.LastCode.Text = "Son Gelen Veri Kodu: " + info.packageNumber;
                control.LastCodeTime.Text = "Veri Zamanı: " + info.sendTime;
                });
            }
        }
        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
        private void DataButton_Click(object sender, RoutedEventArgs e)
        {
            DataShowWindow dataShowWindow = new DataShowWindow();
            dataShowWindow.Show();
        }
        private void SaveDataButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //List<SensorData> sensorDataList = _repository.GetList();
                //DataTable dataTable = ConvertToDataTable(sensorDataList);
                //SaveToExcel(dataTable);
                string filePath = @"C:\Users\mstfm\Desktop\deneme\sensorData.xlsx"; // Excel Yolu
                //OpenExcelFile(filePath);
                if (File.Exists(filePath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    };

                    Process.Start(startInfo);
                }

                // Uygulamayı kapat
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void OpenExcelFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true // Dosyanın varsayılan uygulamayla açılmasını sağlar.
                };

                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show("Dosya bulunamadı.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private DataTable ConvertToDataTable(List<SensorData> data)
        {
            DataTable dataTable = new DataTable("SensorData");

            // DataTable sütunlarını oluşturun
            dataTable.Columns.Add("PackageNumber", typeof(int));
            dataTable.Columns.Add("SatelliteStatus", typeof(int));
            dataTable.Columns.Add("ErrorCode", typeof(string));
            dataTable.Columns.Add("SendTime", typeof(DateTime));
            dataTable.Columns.Add("Pressure1", typeof(float));
            dataTable.Columns.Add("Pressure2", typeof(float));
            dataTable.Columns.Add("Height1", typeof(int));
            dataTable.Columns.Add("Height2", typeof(int));
            dataTable.Columns.Add("AltitudeDif", typeof(int));
            dataTable.Columns.Add("DescentSpeed", typeof(int));
            dataTable.Columns.Add("Tempature", typeof(int));
            dataTable.Columns.Add("BatteryVoltage", typeof(float));
            dataTable.Columns.Add("Gps1Latitude", typeof(float));
            dataTable.Columns.Add("Gps1Longitude", typeof(float));
            dataTable.Columns.Add("Gps1Altitude", typeof(float));
            dataTable.Columns.Add("Roll", typeof(float));
            dataTable.Columns.Add("Pitch", typeof(float));
            dataTable.Columns.Add("Yaw", typeof(float));
            dataTable.Columns.Add("Rhrh", typeof(string));
            dataTable.Columns.Add("IoTData", typeof(string));
            dataTable.Columns.Add("TeamNumber", typeof(int));

            // DataTable'a veri ekleyin
            foreach (var item in data)
            {
                dataTable.Rows.Add(
                    item.packageNumber,
                    item.satelliteStatus,
                    item.ErrorCode,
                    item.sendTime,
                    item.pressure1,
                    item.pressure2,
                    item.height1,
                    item.height2,
                    item.altitudeDif,
                    item.descentSpeed,
                    item.tempature,
                    item.batteryVoltage,
                    item.gps1Latitude,
                    item.gps1Longitude,
                    item.gps1altitude,
                    item.roll,
                    item.pitch,
                    item.yaw,
                    item.rhrh,
                    item.IoTData,
                    item.TeamNumber
                );
            }

            return dataTable;
        }

        private void SaveToExcel(DataTable dataTable)
        {
            try
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    workbook.Worksheets.Add(dataTable, "Sheet1");
                    string folderPath = "C:\\Users\\mstfm\\Documents"; //  C:\Users\mstfm\Documents
                    string filePath = System.IO.Path.Combine(folderPath, "output.xlsx");
                    workbook.SaveAs(filePath);
                }
                MessageBox.Show("Data exported to Excel successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving to Excel: " + ex.Message);
            }
        }
    }
}
