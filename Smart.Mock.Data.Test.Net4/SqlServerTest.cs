﻿namespace Smart.Mock
{
    using Dapper;

    using Smart.Mock.Data;
    using Smart.Mock.Data.SqlServer;

    using Xunit;

    /// <summary>
    /// SqlServerTest の概要の説明
    /// </summary>
    public class SqlServerTest
    {
        [Fact]
        public void ValidSqlExecute()
        {
            using (var connection = new MockDbConnection())
            {
                connection.SetupCommand(cmd => cmd.SetupResult(0));

                connection.Execute("UPDATE Employee SET Name = @Name WHERE Id = @Id", new { Id = 1, Name = "Employee1" });

                var result = connection.ValidateSql();
                Assert.True(result.Valid, result.ToString());
            }
        }

        [Fact]
        public void InvalidSqlExecute()
        {
            using (var connection = new MockDbConnection())
            {
                connection.SetupCommand(cmd => cmd.SetupResult(0));

                connection.Execute("UPDA TE Employee SET Name = @Name WHERE Id = @Id", new { Id = 1, Name = "Employee1" });

                var result = connection.ValidateSql();
                Assert.False(result.Valid, result.ToString());
            }
        }
    }
}
