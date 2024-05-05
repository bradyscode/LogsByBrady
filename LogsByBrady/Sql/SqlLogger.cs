using Dapper;
using LogsByBrady.Enums;
using LogsByBrady.Interfaces;
using LogsByBrady.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Reflection;

namespace LogsByBrady.Sql
{
    internal class SqlLogger : ILogger
    {
        public object GenerateMessage(string logLevel, string message, BradysFormatProvider bradysFormatProvider)
        {
            var methodInfo = new StackTrace().GetFrame(2)?.GetMethod();
            var className = methodInfo?.ReflectedType?.Name;
            return new LogModel()
            {
                DateAndTime = System.DateTime.UtcNow,
                LogLevel = logLevel.ToUpper(),
                LogMessage = message, 
                LoggingClass = className
            };
        }

        public Task Log(object message, string path)
        {

            var loggingInfo = (LogModel)message;
            using (SqlConnection connection = new SqlConnection(path))
            {
                // Define your insert query for logs
                string insertQuery = "INSERT INTO Logs (LogDate, LoggingClass, ThreadId, LogProject, LogLevel, LogMessage) VALUES (@LogDate, @LoggingClass, @ThreadId, @LogProject, @LogLevel, @LogMessage)";

                var paramaters = new DynamicParameters();
                paramaters.Add("@LogDate", DateTime.Now);
                paramaters.Add("@LogLevel", loggingInfo.LogLevel);
                paramaters.Add("@LoggingClass", loggingInfo.LoggingClass);
                paramaters.Add("@LogMessage", loggingInfo.LogMessage);
                paramaters.Add("@LogProject", loggingInfo.LogProject);
                paramaters.Add("@ThreadId", loggingInfo.ManagedThreadId);

                try
                {
                    connection.Open();
                    connection.Execute(insertQuery, paramaters);
                    Console.WriteLine("Log inserted successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error writing to sql.", ex);
                }
            }
            return Task.CompletedTask;
        }
    }
}
