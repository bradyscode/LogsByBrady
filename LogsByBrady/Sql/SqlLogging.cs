using LogsByBrady.Enums;
using LogsByBrady.Interfaces;
using Microsoft.Data.SqlClient;

namespace LogsByBrady.Sql
{
    public class SqlLogging : IBradysLogger
    {
        ILogger logger = new SqlLogger();
        public static string ConnectionString { get; set; }

        public ILogger Critical(string message)
        {
            logger.Log(logger.GenerateMessage("critical", message, BradysFormatProvider.Text), ConnectionString);
            return logger;
        }

        public ILogger Debug(string message)
        {
            logger.Log(logger.GenerateMessage("debug", message, BradysFormatProvider.Text), ConnectionString);
            return logger;
        }

        public ILogger Error(string message)
        {
            logger.Log(logger.GenerateMessage("error", message, BradysFormatProvider.Text), ConnectionString);
            return logger;
        }

        public ILogger Exception(string message)
        {
            logger.Log(logger.GenerateMessage("exception", message, BradysFormatProvider.Text), ConnectionString);
            return logger;
        }

        public ILogger Info(string message)
        {
            logger.Log(logger.GenerateMessage("information", message, BradysFormatProvider.Text), ConnectionString);
            return logger;
        }

        public ILogger Notice(string message)
        {
            logger.Log(logger.GenerateMessage("notice", message, BradysFormatProvider.Text), ConnectionString);
            return logger;
        }

        public ILogger Success(string message)
        {
            logger.Log(logger.GenerateMessage("success", message, BradysFormatProvider.Text), ConnectionString);
            return logger;
        }

        public ILogger Trace(string message)
        {
            logger.Log(logger.GenerateMessage("trace", message, BradysFormatProvider.Text), ConnectionString);
            return logger;
        }

        public ILogger Warning(string message)
        {
            logger.Log(logger.GenerateMessage("warning", message, BradysFormatProvider.Text), ConnectionString);
            return logger;
        }

        internal static void CreateLogsTable()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // Check if the table already exists
                string checkTableQuery = "SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Logs'";
                SqlCommand checkTableCommand = new SqlCommand(checkTableQuery, connection);

                connection.Open();
                bool tableExists = checkTableCommand.ExecuteScalar() != null;

                if (!tableExists)
                {
                    // Define your create table query for logs
                    string createTableQuery = @"
                    CREATE TABLE Logs (
                        LogID INT IDENTITY(1,1) PRIMARY KEY,
                        LogDate DATETIME,
                        LoggingClass NVARCHAR(100),
                        LogProject NVARCHAR(100),
                        LogLevel NVARCHAR(20),
                        LogMessage NVARCHAR(MAX)
                    )";

                    SqlCommand createTableCommand = new SqlCommand(createTableQuery, connection);

                    try
                    {
                        // Execute the create table query
                        createTableCommand.ExecuteNonQuery();
                        Console.WriteLine("Logs table created successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error creating logs table: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Logs table already exists.");

                    // Delete records older than one week
                    string deleteOldLogsQuery = "DELETE FROM Logs WHERE LogDate < DATEADD(DAY, -7, GETDATE())";
                    SqlCommand deleteOldLogsCommand = new SqlCommand(deleteOldLogsQuery, connection);

                    try
                    {
                        // Execute the delete query
                        int rowsAffected = deleteOldLogsCommand.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} old log(s) deleted.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error deleting old logs: " + ex.Message);
                    }
                }
            }
        }
    }
}