using System;

namespace Accelerate.Logging
{
    /// <summary>
    /// Configuration options for system console logger service.
    /// </summary>
    public sealed class ConsoleLoggerServiceOptions : LoggerServiceOptions
    {
        /// <summary>
        /// Console color when write critical traces.
        /// </summary>
        public ConsoleColor CriticalColor { get; set; }
        /// <summary>
        /// Console color when write debug traces.
        /// </summary>
        public ConsoleColor DebugColor { get; set; }
        /// <summary>
        /// Console color when write error traces.
        /// </summary>
        public ConsoleColor ErrorColor { get; set; }
        /// <summary>
        /// Console color when write information traces.
        /// </summary>
        public ConsoleColor InformationColor { get; set; }
        /// <summary>
        /// Console color when write warning traces.
        /// </summary>
        public ConsoleColor WarningColor { get; set; }
    }
}