using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogsByBrady
{
    public class LogModel
    {
        public string? Message { get; set; }
        public DateTime DateAndTime { get; set; } = DateTime.UtcNow;
        public string? LogLevel { get; set; }
    }
}
