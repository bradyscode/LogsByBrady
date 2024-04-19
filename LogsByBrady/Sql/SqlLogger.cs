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
                Message = message, 
                CallingClass = className
            };
        }

        public Task Log(object message, string path)
        {

            var loggingInfo = (LogModel)message;
            using (SqlConnection connection = new SqlConnection(path))
            {
                // Define your insert query for logs
                string insertQuery = "INSERT INTO Logs (LogDate, LoggingClass, ThreadId, LogProject, LogLevel, LogMessage) VALUES (@LogDate, @LoggingClass, @ThreadId, @LogProject, @LogLevel, @LogMessage)";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@LogDate", DateTime.Now);
                command.Parameters.AddWithValue("@LogLevel", loggingInfo.LogLevel);
                command.Parameters.AddWithValue("@LoggingClass", loggingInfo.CallingClass);
                command.Parameters.AddWithValue("@LogMessage", loggingInfo.Message);
                command.Parameters.AddWithValue("@LogProject", loggingInfo.CallingProject);
                command.Parameters.AddWithValue("@ThreadId", loggingInfo.ManagedThreadId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
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
