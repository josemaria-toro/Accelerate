using System;

namespace Accelerate.Logging
{
    /// <summary>
    /// Configuration options for logger service.
    /// </summary>
    public class LoggerServiceOptions
    {
        /// <summary>
        /// Application name.
        /// </summary>
        public String AppName { get; set; }
        /// <summary>
        /// Enable or disable critical traces writing.
        /// </summary>
        public Boolean Critical { get; set; }
        /// <summary>
        /// Enable or disable debug traces writing.
        /// </summary>
        public Boolean Debug { get; set; }
        /// <summary>
        /// Enable or disable error traces writing.
        /// </summary>
        public Boolean Error { get; set; }
        /// <summary>
        /// Enable or disable information traces writing.
        /// </summary>
        public Boolean Information { get; set; }
        /// <summary>
        /// Enable or disable warning traces writing.
        /// </summary>
        public Boolean Warning { get; set; }
    }
}