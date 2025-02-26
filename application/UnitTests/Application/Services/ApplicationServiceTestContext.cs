using Accelerate.Testing;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Application.Services;

/// <summary>
/// Context for unit tests of ApplicationService class.
/// </summary>
[ExcludeFromCodeCoverage]
public class ApplicationServiceTestContext : xUnitTestContext
{
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    public ApplicationServiceTestContext() : base()
    {
    }
}