using System;

namespace Accelerate.Logging
{
    /// <summary>
    /// Configuration options for file system logger service.
    /// </summary>
    public sealed class FileSystemLoggerServiceOptions : LoggerServiceOptions
    {
        /// <summary>
        /// Traces file name.
        /// </summary>
        public String FileName { get; set; }
        /// <summary>
        /// Max size of file.
        /// </summary>
        public Int64 MaxSize { get; set; }
        /// <summary>
        /// Path of files.
        /// </summary>
        public String Path { get; set; }
    }
}