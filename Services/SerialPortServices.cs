using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talaria.Models;
using Talaria.Windows;

namespace Talaria.Services
{
    public class SerialPortServices
    {
        private static readonly string filePath = "C:\\Users\\mstfm\\Desktop\\deneme\\output.txt"; // txt Dosya yolu
        private static readonly string excelFilePath = "C:\\Users\\mstfm\\Desktop\\deneme\\sensorData.xlsx"; // Excel Dosya yolu
        private Timer _timer;


        private SerialPort _serialPort;
        public event Action<SensorData> DataReceived;
        public event Action<string> DataReceivedString;
        private readonly SensorDataRepository _repository;
        private SensorDataForExcel _forExcel;

        //public SerialPortServices(string portName, SensorDataRepository repository, int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        //{
        //    _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        //    _serialPort.DataReceived += OnDataReceived;
        //    _repository = repository;
        //}
        public void getTextDataServices()
        {
            _timer = new Timer(OnTimerElapsed, null, 0, 2000);
        }
        private void OnTimerElapsed(object state)
        {
            OnDataReceived(null, null);
        }
        public void Open()
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
            }
        }

        public void Close()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

        private static async Task<string> ReadFileAsync(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private async void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //string data = _serialPort.ReadExisting();
            string data = await ReadFileAsync(filePath);
            ProcessData(data);
            DataReceivedString?.Invoke(data);
        }
        private void ProcessData(string data)
        {
            string[] dataArray = data.Split(';');
            if (dataArray.Length >= 21)
            {
                var sensorData = new SensorData();

                if (int.TryParse(dataArray[0], out int packageNumber))
                    sensorData.packageNumber = packageNumber;

                if (int.TryParse(dataArray[1], out int satelliteStatus))
                    sensorData.satelliteStatus = satelliteStatus;
                
                    sensorData.ErrorCode = dataArray[2];

                if (DateTime.TryParseExact(dataArray[3].Replace(",", "").Trim(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sendTime))
                    sensorData.sendTime = sendTime;

                if (float.TryParse(dataArray[4], NumberStyles.Any, CultureInfo.InvariantCulture, out float pressure1))
                    sensorData.pressure1 = pressure1;

                if (float.TryParse(dataArray[5], NumberStyles.Any, CultureInfo.InvariantCulture, out float pressure2))
                    sensorData.pressure2 = pressure2;

                if (int.TryParse(dataArray[6], out int height1))
                    sensorData.height1 = height1;

                if (int.TryParse(dataArray[7], out int height2))
                    sensorData.height2 = height2;

                if (int.TryParse(dataArray[8], out int altitudeDif))
                    sensorData.altitudeDif = altitudeDif;

                if (int.TryParse(dataArray[9], out int descentSpeed))
                    sensorData.descentSpeed = descentSpeed;

                if (int.TryParse(dataArray[10], out int tempature))
                    sensorData.tempature = tempature;

                if (float.TryParse(dataArray[11], NumberStyles.Any, CultureInfo.InvariantCulture, out float batteryVoltage))
                    sensorData.batteryVoltage = batteryVoltage;

                if (float.TryParse(dataArray[12], NumberStyles.Any, CultureInfo.InvariantCulture, out float gps1Latitude))
                    sensorData.gps1Latitude = gps1Latitude;

                if (float.TryParse(dataArray[13], NumberStyles.Any, CultureInfo.InvariantCulture, out float gps1Longitude))
                    sensorData.gps1Longitude = gps1Longitude;

                if (float.TryParse(dataArray[14], NumberStyles.Any, CultureInfo.InvariantCulture, out float gps1altitude))
                    sensorData.gps1altitude = gps1altitude;

                if (float.TryParse(dataArray[15], NumberStyles.Any, CultureInfo.InvariantCulture, out float roll))
                    sensorData.roll = roll;

                if (float.TryParse(dataArray[16], NumberStyles.Any, CultureInfo.InvariantCulture, out float pitch))
                    sensorData.pitch = pitch;

                if (float.TryParse(dataArray[17], NumberStyles.Any, CultureInfo.InvariantCulture, out float yaw))
                    sensorData.yaw = yaw;

                sensorData.rhrh = dataArray[18];
                sensorData.IoTData = dataArray[19];

                if (int.TryParse(dataArray[20], out int teamNumber))
                    sensorData.TeamNumber = teamNumber;

                //_repository.AddSensorData(sensorData);

                _forExcel = new SensorDataForExcel(excelFilePath);
                _forExcel.AddSensorData(sensorData);

                DataReceived?.Invoke(sensorData);
            }
        }

    }
}
