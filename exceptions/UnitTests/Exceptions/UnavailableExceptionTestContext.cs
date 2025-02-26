using Accelerate.Testing;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate;

/// <summary>
/// Context for unit tests of UnavailableException class.
/// </summary>
[ExcludeFromCodeCoverage]
public class UnavailableExceptionTestContext : xUnitTestContext
{
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    public UnavailableExceptionTestContext() : base()
    {
    }
}