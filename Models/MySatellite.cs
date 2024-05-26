using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talaria.Models
{
    public class MySatellite
    {
        public int TeamNumber { get; set; }
        public int satelliteStatus { get; set; }
        public int packageNumber { get; set; }
        public DateTime sendTime { get; set; }
    }
}
