using Accelerate.Testing;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate;

/// <summary>
/// Context for unit tests of BusinessException class.
/// </summary>
[ExcludeFromCodeCoverage]
public class BusinessExceptionTestContext : xUnitTestContext
{
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    public BusinessExceptionTestContext() : base()
    {
    }
}