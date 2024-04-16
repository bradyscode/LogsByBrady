using LogsByBrady.Enums;
using LogsByBrady.Interfaces;
using LogsByBrady.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace LogsByBrady.Sql
{
    internal class SqlLogger : ILogger
    {
        public object GenerateMessage(string logLevel, string message, BradysFormatProvider bradysFormatProvider)
        {
            return new LogModel()
            {
                DateAndTime = System.DateTime.UtcNow,
                LogLevel = logLevel,
                Message = message
            };
        }

        public Task Log(object message, string path)
        {
            var loggingInfo = (LogModel)message;
            using (SqlConnection connection = new SqlConnection(path))
            {
                // Define your insert query for logs
                string insertQuery = "INSERT INTO Logs (LogDate, LogLevel, LogMessage) VALUES (@LogDate, @LogLevel, @LogMessage)";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@LogDate", DateTime.Now);
                command.Parameters.AddWithValue("@LogLevel", loggingInfo.LogLevel);
                command.Parameters.AddWithValue("@LogMessage", loggingInfo.Message);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Log inserted successfully!");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error writing to sql.", ex);
                }
            }
            return Task.CompletedTask;
        }
    }
}
