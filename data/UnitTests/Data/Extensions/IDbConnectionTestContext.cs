using Accelerate.Testing;
using Moq;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Data.Extensions;

/// <summary>
/// Context for tests based on IDbConnection extension methods.
/// </summary>
[ExcludeFromCodeCoverage]
public class IDbConnectionTestContext : xUnitTestContext
{
    private IDbConnection _connection;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    public IDbConnectionTestContext()
    {
        _connection = CreateIDbConnection();
    }

    internal IDbConnection Connection => _connection;

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
            .Returns(5);
        mock.Setup(x => x.ExecuteReader())
            .Returns(CreateIDataReader);

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

        mock.Setup(x => x.Add(It.IsAny<Object>()))
            .Returns(1);

        return mock.Object;
    }
    private static IDataReader CreateIDataReader()
    {
        var mock = new Mock<IDataReader>();
        var random = new Random(DateTime.UtcNow.Millisecond);

        mock.SetupGet(x => x.FieldCount)
            .Returns(10);

        mock.Setup(x => x.GetName(0))
            .Returns("Boolean");
        mock.Setup(x => x.GetName(1))
            .Returns("DateTime");
        mock.Setup(x => x.GetName(2))
            .Returns("Decimal");
        mock.Setup(x => x.GetName(3))
            .Returns("Double");
        mock.Setup(x => x.GetName(4))
            .Returns("Guid");
        mock.Setup(x => x.GetName(5))
            .Returns("Int16");
        mock.Setup(x => x.GetName(6))
            .Returns("Int32");
        mock.Setup(x => x.GetName(7))
            .Returns("Int64");
        mock.Setup(x => x.GetName(8))
            .Returns("Single");
        mock.Setup(x => x.GetName(9))
            .Returns("String");
        mock.Setup(x => x.GetName(10))
            .Returns("property_with_attribute");

        mock.Setup(x => x.GetValue(0))
            .Returns(true);
        mock.Setup(x => x.GetValue(1))
            .Returns(DateTime.UtcNow);
        mock.Setup(x => x.GetValue(2))
            .Returns(1M);
        mock.Setup(x => x.GetValue(3))
            .Returns(1D);
        mock.Setup(x => x.GetValue(4))
            .Returns(Guid.NewGuid());
        mock.Setup(x => x.GetValue(5))
            .Returns(1);
        mock.Setup(x => x.GetValue(6))
            .Returns(1);
        mock.Setup(x => x.GetValue(7))
            .Returns(1L);
        mock.Setup(x => x.GetValue(8))
            .Returns(1F);
        mock.Setup(x => x.GetValue(9))
            .Returns("lorem ipsum");
        mock.Setup(x => x.GetValue(10))
            .Returns("lorem ipsum");

        mock.Setup(x => x.IsDBNull(It.IsAny<Int32>()))
            .Returns(() => { return random.Next(0, 100) <= 10; });
        mock.Setup(x => x.Read())
            .Returns(() => { return random.Next(0, 100) <= 50; });

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