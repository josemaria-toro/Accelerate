using System;

namespace Accelerate.Data.Repositories
{
    /// <summary>
    /// Base class for configuration options for data repositories.
    /// </summary>
    public abstract class RepositoryOptions
    {
        /// <summary>
        /// Database connection string.
        /// </summary>
        public String ConnectionString { get; set; }
        /// <summary>
        /// The wait time, in seconds, before terminating the execution of the query and throw an error.
        /// </summary>
        public Int32 Timeout { get; set; }
    }
}