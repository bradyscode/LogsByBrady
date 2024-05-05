
using LogsByBrady.Sql;
using Testcontainers.MsSql;

namespace LogsByBrady.Tests
{
    public class SqlLoggingTests : IAsyncLifetime
    {
        private static MsSqlContainer msSqlContainer = new MsSqlBuilder()
              .WithImage("mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04")
              .Build();
        SqlLogging sqlLogging;
        public async Task DisposeAsync()
        {
            await msSqlContainer.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            await msSqlContainer.StartAsync();
            sqlLogging = new SqlLogging(msSqlContainer.GetConnectionString());
        }

        [Fact]
        public async Task CanAddAndRetreiveLogsAsync()
        {
            // Given

            // When
            sqlLogging.Success("Test Message");
            sqlLogging.Success("Test Message1");
            var logs = await sqlLogging.GetLogs();

            // Then
            Assert.Equal("Test Message", logs[0].LogMessage);
            Assert.Equal("SUCCESS", logs[0].LogLevel);
            Assert.Equal("Test Message1", logs[1].LogMessage);
            Assert.Equal("SUCCESS", logs[1].LogLevel);
            Assert.Equal(2, logs.Count);
        }
    }
}