using Accelerate.Testing;
using Moq;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Logging;

/// <summary>
/// Context for tests based on database logger provider.
/// </summary>
[ExcludeFromCodeCoverage]
public class DatabaseLoggerTestContext : xUnitTestContext
{
    private IDbConnection _connection;
    private DatabaseLoggerServiceOptions _loggerServiceOptions;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    public DatabaseLoggerTestContext()
    {
        _connection = CreateIDbConnection();
        _loggerServiceOptions = new DatabaseLoggerServiceOptions
        {
            AppName = "Database",
            Critical = true,
            Debug = false,
            Error = true,
            Information = true,
            Warning = true
        };
    }

    internal IDbConnection Connection => _connection;
    internal DatabaseLoggerServiceOptions LoggerServiceOptions => _loggerServiceOptions;

    private static IDbCommand CreateIDbCommand()
    {
        var mock = new Mock<IDbCommand>();

        mock.SetupGet(x => x.CommandText)
            .Returns($"{Guid.NewGuid()}");
        mock.SetupGet(x => x.CommandTimeout)
            .Returns(30);
        mock.SetupGet(x => x.CommandType)
            .Returns(CommandType.Text);
        mock.SetupGet(x => x.Parameters)
            .Returns(CreateIDataParameterCollection);
        mock.Setup(x => x.CreateParameter())
            .Returns(CreateIDataParameter);
        mock.Setup(x => x.ExecuteNonQuery())
            .Returns(1);

        return mock.Object;
    }
    private static IDbConnection CreateIDbConnection()
    {
        var mock = new Mock<IDbConnection>();

        mock.SetupGet(x => x.State)
            .Returns(ConnectionState.Open);

        mock.Setup(x => x.CreateCommand())
            .Returns(CreateIDbCommand);

        return mock.Object;
    }
    private static IDataParameterCollection CreateIDataParameterCollection()
    {
        var mock = new Mock<IDataParameterCollection>();

        mock.Setup(x => x.Add(It.IsAny<object>()))
            .Returns(1);

        return mock.Object;
    }
    private static IDbDataParameter CreateIDataParameter()
    {
        var mock = new Mock<IDbDataParameter>();

        mock.SetupGet(x => x.Direction)
            .Returns(ParameterDirection.Input);
        mock.SetupGet(x => x.ParameterName)
            .Returns($"{Guid.NewGuid()}");
        mock.SetupGet(x => x.Value)
            .Returns($"{Guid.NewGuid()}");

        return mock.Object;
    }
}
