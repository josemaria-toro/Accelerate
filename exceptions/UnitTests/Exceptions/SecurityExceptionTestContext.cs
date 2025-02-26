using Accelerate.Testing;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate;

/// <summary>
/// Context for unit tests of SecurityException class.
/// </summary>
[ExcludeFromCodeCoverage]
public class SecurityExceptionTestContext : xUnitTestContext
{
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    public SecurityExceptionTestContext() : base()
    {
    }
}