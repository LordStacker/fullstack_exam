using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace repository.Models
{
    public class MonitorAlert : BaseModel
    {
        public int UserId { get; set; }
        public string? CreatedAt { get; set; }
        public string? Message { get; set; }
    }
}