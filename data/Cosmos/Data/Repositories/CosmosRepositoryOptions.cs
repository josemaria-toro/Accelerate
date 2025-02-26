using System;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Data.Repositories
{
    /// <summary>
    /// Configuration options for data repositories based on Cosmos DB.
    /// </summary>
#if NETSTANDARD
    [ExcludeFromCodeCoverage]
#else
    [ExcludeFromCodeCoverage(Justification = "Unit tests for entity framework are unavailable.")]
#endif
    public abstract class CosmosRepositoryOptions : EntityFrameworkRepositoryOptions
    {
#if NETSTANDARD
        /// <summary>
        /// Account key.
        /// </summary>
        public String AccountKey { get; set; }
#endif
        /// <summary>
        /// Database name.
        /// </summary>
        public String Database { get; set; }
#if NETSTANDARD
        /// <summary>
        /// Client url.
        /// </summary>
        public String Url { get; set; }
#endif
    }
}