using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace repository.Models
{
    public class Device : BaseModel
    {
        public string? DeviceName { get; set; }
        public int UserId { get; set; }
    }
}