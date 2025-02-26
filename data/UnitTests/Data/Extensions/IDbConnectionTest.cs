using Accelerate.Data.Entities;
using Accelerate.Testing;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Accelerate.Data.Extensions;

/// <summary>
/// Class to perform unit tests of IDbConnection extension methods.
/// </summary>
[ExcludeFromCodeCoverage]
public class IDbConnectionTest : xUnitTest<IDbConnectionTestContext>
{
    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    /// <param name="context">
    /// Context binded to a test.
    /// </param>
    public IDbConnectionTest(IDbConnectionTestContext context) : base(context)
    {
    }

    /// <summary>
    /// Method to perform test of delete method.
    /// </summary>
    [Fact]
    public void Delete()
    {
        var queryString = "DELETE FROM 'database'.'table'";
        var affectedRows = Context.Connection.Delete(queryString);

        Assert.True(affectedRows > 0);
    }
    /// <summary>
    /// Method to perform test of delete method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Delete_Throwing_Exception_By_Invalid_Connection()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            IDbConnectionExtensions.Delete(null, String.Empty);
        });
    }
    /// <summary>
    /// Method to perform test of delete method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Delete_Throwing_Exception_By_Invalid_Query_String()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Context.Connection.Delete(String.Empty);
        });
    }
    /// <summary>
    /// Method to perform test of delete method passing parameters.
    /// </summary>
    [Fact]
    public void Delete_With_Parameters()
    {
        var queryString = "DELETE FROM 'database'.'table'";
        var queryParameters = new Dictionary<String, Object>
        {
            { "@parameter", "lorem ipsum" }
        };
        var affectedRows = Context.Connection.Delete(queryString, queryParameters);

        Assert.True(affectedRows > 0);
    }
    /// <summary>
    /// Method to perform test of delete method passing parameters and timeout.
    /// </summary>
    [Fact]
    public void Delete_With_Parameters_And_Timeout()
    {
        var queryString = "DELETE FROM 'database'.'table'";
        var queryParameters = new Dictionary<String, Object>
        {
            { "@parameter", "lorem ipsum" }
        };
        var affectedRows = Context.Connection.Delete(queryString, queryParameters, 30);

        Assert.True(affectedRows > 0);
    }
    /// <summary>
    /// Method to perform test of insert method.
    /// </summary>
    [Fact]
    public void Insert()
    {
        var queryString = "INSERT INTO 'database'.'table'";
        var affectedRows = Context.Connection.Insert(queryString);

        Assert.True(affectedRows > 0);
    }
    /// <summary>
    /// Method to perform test of insert method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Insert_Throwing_Exception_By_Invalid_Connection()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            IDbConnectionExtensions.Insert(null, String.Empty);
        });
    }
    /// <summary>
    /// Method to perform test of insert method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Insert_Throwing_Exception_By_Invalid_Query_String()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Context.Connection.Insert(String.Empty);
        });
    }
    /// <summary>
    /// Method to perform test of insert method passing parameters.
    /// </summary>
    [Fact]
    public void Insert_With_Parameters()
    {
        var queryString = "INSERT INTO 'database'.'table'";
        var queryParameters = new Dictionary<String, Object>
        {
            { "@parameter", "lorem ipsum" }
        };
        var affectedRows = Context.Connection.Insert(queryString, queryParameters);

        Assert.True(affectedRows > 0);
    }
    /// <summary>
    /// Method to perform test of insert method passing parameters and timeout.
    /// </summary>
    [Fact]
    public void Insert_With_Parameters_And_Timeout()
    {
        var queryString = "INSERT INTO 'database'.'table'";
        var queryParameters = new Dictionary<String, Object>
        {
            { "@parameter", "lorem ipsum" }
        };
        var affectedRows = Context.Connection.Insert(queryString, queryParameters, 30);

        Assert.True(affectedRows > 0);
    }
    /// <summary>
    /// Method to perform test of select method.
    /// </summary>
    [Fact]
    public void Select()
    {
        var queryString = "SELECT * FROM 'database'.'table'";
        var queryResults = Context.Connection.Select<EntityClass>(queryString);

        Assert.NotNull(queryResults);
    }
    /// <summary>
    /// Method to perform test of select method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Select_Throwing_Exception_By_Invalid_Connection()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            IDbConnectionExtensions.Select<EntityClass>(null, String.Empty);
        });
    }
    /// <summary>
    /// Method to perform test of select method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Select_Throwing_Exception_By_Invalid_Query_String()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Context.Connection.Select<EntityClass>(String.Empty);
        });
    }
    /// <summary>
    /// Method to perform test of select method passing parameters.
    /// </summary>
    [Fact]
    public void Select_With_Parameters()
    {
        var queryString = "SELECT * FROM 'database'.'table'";
        var queryParameters = new Dictionary<String, Object>
        {
            { "@parameter", "lorem ipsum" }
        };
        var queryResults = Context.Connection.Select<EntityClass>(queryString, queryParameters);

        Assert.NotNull(queryResults);
    }
    /// <summary>
    /// Method to perform test of select method passing parameters and timeout.
    /// </summary>
    [Fact]
    public void Select_With_Parameters_And_Timeout()
    {
        var queryString = "SELECT * FROM 'database'.'table'";
        var queryParameters = new Dictionary<String, Object>
        {
            { "@parameter", "lorem ipsum" }
        };
        var queryResults = Context.Connection.Select<EntityClass>(queryString, queryParameters, 30);

        Assert.NotNull(queryResults);
    }
    /// <summary>
    /// Method to perform test of update method.
    /// </summary>
    [Fact]
    public void Update()
    {
        var queryString = "UPDATE 'database'.'table'";
        var affectedRows = Context.Connection.Update(queryString);

        Assert.True(affectedRows > 0);
    }
    /// <summary>
    /// Method to perform test of update method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Update_Throwing_Exception_By_Invalid_Connection()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            IDbConnectionExtensions.Update(null, String.Empty);
        });
    }
    /// <summary>
    /// Method to perform test of update method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Update_Throwing_Exception_By_Invalid_Query_String()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Context.Connection.Update(String.Empty);
        });
    }
    /// <summary>
    /// Method to perform test of update method passing parameters.
    /// </summary>
    [Fact]
    public void Update_With_Parameters()
    {
        var queryString = "UPDATE 'database'.'table'";
        var queryParameters = new Dictionary<String, Object>
        {
            { "@parameter", "lorem ipsum" }
        };
        var affectedRows = Context.Connection.Update(queryString, queryParameters);

        Assert.True(affectedRows > 0);
    }
    /// <summary>
    /// Method to perform test of update method passing parameters and timeout.
    /// </summary>
    [Fact]
    public void Update_With_Parameters_And_Timeout()
    {
        var queryString = "UPDATE 'database'.'table'";
        var queryParameters = new Dictionary<String, Object>
        {
            { "@parameter", "lorem ipsum" }
        };
        var affectedRows = Context.Connection.Update(queryString, queryParameters, 30);

        Assert.True(affectedRows > 0);
    }
}