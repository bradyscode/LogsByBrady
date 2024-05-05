using LogsByBrady.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogsByBrady.Interfaces
{
    public interface IDatabaseActions
    {
        Task<List<LogModel>> GetLogs();
        string GetConnectionString();
        void SetConnectionString(string connectionString);
    }
}
