using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talaria.Models;

namespace Talaria.Services
{
    public class SensorDataRepository
    {
        private readonly PortTestContextDb _context;

        public SensorDataRepository(PortTestContextDb context)
        {
            _context = context;
        }

        public void AddSensorData(SensorData sensorData)
        {
            _context.SensorDatas.Add(sensorData);
            _context.SaveChanges();
        }
        public List<SensorData> GetList()
        {
            return _context.SensorDatas.ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
