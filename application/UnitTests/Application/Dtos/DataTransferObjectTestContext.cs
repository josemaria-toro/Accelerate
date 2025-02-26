using Accelerate.Testing;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Application.Dtos;

/// <summary>
/// Context for unit tests of DataTransferObject class.
/// </summary>
[ExcludeFromCodeCoverage]
public class DataTransferObjectTestContext : xUnitTestContext
{
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    public DataTransferObjectTestContext() : base()
    {
    }
}