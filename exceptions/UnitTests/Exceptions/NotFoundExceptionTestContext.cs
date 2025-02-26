using Accelerate.Testing;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate;

/// <summary>
/// Context for unit tests of NotFoundException class.
/// </summary>
[ExcludeFromCodeCoverage]
public class NotFoundExceptionTestContext : xUnitTestContext
{
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    public NotFoundExceptionTestContext() : base()
    {
    }
}