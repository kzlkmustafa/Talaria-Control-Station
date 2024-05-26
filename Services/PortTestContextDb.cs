using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talaria.Models;

namespace Talaria.Services
{
    public class PortTestContextDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=192.168.135.129;Database=TalariaDb;User Id=SA; Password=Password1; Integrated Security=false; Encrypt = False;"); //Mustafa'nın Database
        }

        public DbSet<SensorData> SensorDatas { get; set; }
    }
}
