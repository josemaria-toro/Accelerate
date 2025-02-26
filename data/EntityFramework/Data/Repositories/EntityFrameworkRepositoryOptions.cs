using System;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Data.Repositories
{
    /// <summary>
    /// Base class for configuration options for data repositories based on entity framework.
    /// </summary>
#if NETSTANDARD
    [ExcludeFromCodeCoverage]
#else
    [ExcludeFromCodeCoverage(Justification = "Unit tests for entity framework are unavailable.")]
#endif
    public abstract class EntityFrameworkRepositoryOptions : RepositoryOptions
    {
        /// <summary>
        /// Flag for enabling log detailed errors.
        /// </summary>
        public Boolean DetailedErrors { get; set; }
        /// <summary>
        /// Flag for enabling lazy loading of data.
        /// </summary>
        public Boolean LazyLoading { get; set; }
        /// <summary>
        /// Flag for enabling sensitive data logging.
        /// </summary>
        public Boolean SensitiveDataLogging { get; set; }
        /// <summary>
        /// Flag for enabling the changes tracking.
        /// </summary>
        public Boolean TrackChanges { get; set; }
    }
}