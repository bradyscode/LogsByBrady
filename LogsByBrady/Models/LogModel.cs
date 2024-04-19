using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LogsByBrady.Models
{
    public class LogModel
    {
        public string? Message { get; set; }
        public DateTime DateAndTime { get; set; } = DateTime.UtcNow;
        public string? LogLevel { get; set; }
        public string? CallingClass { get; set; } = new StackTrace().GetFrame(2)?.GetMethod()?.ReflectedType?.Name;
        public string? CallingProject { get; set; } = Assembly.GetEntryAssembly()?.GetName().Name;
        public int? ManagedThreadId { get; set; } = System.Threading.Thread.CurrentThread.ManagedThreadId;
    }
}
