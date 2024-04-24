using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace repository.Models
{
    public class User : BaseModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}