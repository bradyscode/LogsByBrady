using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogsByBrady.Models
{
    public abstract class DatabaseLogging
    {
        private string ConnectionString { get; set; } = "";
        public string GetConnectionString()
        {
            return ConnectionString;
        }
        public void SetConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }
        protected abstract void CreateLogsTable(string connectionString);
        public abstract Task<List<LogModel>> GetLogs();
    }
}
