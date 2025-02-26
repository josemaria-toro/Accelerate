using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Data.Repositories
{
    /// <summary>
    /// Configuration options for data repositories based on SQLite.
    /// </summary>
#if NETSTANDARD
    [ExcludeFromCodeCoverage]
#else
    [ExcludeFromCodeCoverage(Justification = "Unit tests for entity framework are unavailable.")]
#endif
    public abstract class SQLiteRepositoryOptions : EntityFrameworkRepositoryOptions
    {
    }
}