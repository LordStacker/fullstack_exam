using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace repository.Models
{
    public class Sensor : BaseModel
    {
        public int DeviceId { get; set; }
        public decimal SoundLevel { get; set; }
        public decimal Tempreature { get; set; }
        public decimal Humidity { get; set; }
        public DateTime Date { get; set; }
    }
}