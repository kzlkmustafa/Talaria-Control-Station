using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talaria.Models
{
    public class SensorData
    {
        [Key]
        public int MyDataId { get; set; }
        public int packageNumber { get; set; }
        public int satelliteStatus { get; set; }
        public string ErrorCode { get; set; }
        public DateTime sendTime { get; set; }
        public float pressure1 { get; set; }
        public float pressure2 { get; set; }
        public int height1 { get; set; }
        public int height2 { get; set; }
        public int altitudeDif { get; set; }
        public int descentSpeed { get; set; }
        public int tempature { get; set; }
        public float batteryVoltage { get; set; }
        public float gps1Latitude { get; set; }
        public float gps1Longitude { get; set; }
        public float gps1altitude { get; set; }
        public float roll { get; set; }
        public float pitch { get; set; }
        public float yaw { get; set; }
        public string rhrh { get; set; }
        public string IoTData { get; set; }
        public int TeamNumber { get; set; }
    }
}
