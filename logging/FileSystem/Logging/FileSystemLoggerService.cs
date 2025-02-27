using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accelerate.Logging
{
    /// <summary>
    /// Represent a type to perform logging based on file system.
    /// </summary>
    public sealed class FileSystemLoggerService : LoggerService<FileSystemLoggerServiceOptions>
    {
        private Boolean _disposed;
        private SemaphoreSlim _semaphore;

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options for logger service.
        /// </param>
        public FileSystemLoggerService(IOptions<FileSystemLoggerServiceOptions> options) : base(options)
        {
            _semaphore = new SemaphoreSlim(1, 1);

            if (String.IsNullOrEmpty(Options.FileName))
            {
                Options.FileName = Options.AppName.ToLowerInvariant()
                                                  .Replace(' ', '_');
            }

            if (Options.MaxSize == 0)
            {
                Options.MaxSize = 5 * 1024 * 1024;
            }

            if (String.IsNullOrEmpty(Options.Path))
            {
                Options.Path = Path.Combine(Environment.CurrentDirectory, "logs");
            }
        }
        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options for logger service.
        /// </param>
        /// <param name="categoryName">
        /// The category name for messages produced by the logger.
        /// </param>
        public FileSystemLoggerService(IOptions<FileSystemLoggerServiceOptions> options, String categoryName) : base(options, categoryName)
        {
            _semaphore = new SemaphoreSlim(1, 1);
        }

        /// <inheritdoc />
        protected override void Dispose(Boolean disposing)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            base.Dispose(disposing);

            if (disposing)
            {
                _semaphore.Dispose();
                _semaphore = null;
            }

            _disposed = true;
        }
        /// <inheritdoc />
        public override void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, String> formatter)
        {
            Task.Run(() =>
            {
                if (IsEnabled(logLevel))
                {
                    _semaphore.Wait();

                    try
                    {
                        var dateTime = DateTime.UtcNow;
                        var template = $"{dateTime:o}~{Options.AppName}~{CategoryName}~{logLevel}~{eventId.Id}~{eventId.Name}";
                        var stringBuilder = new StringBuilder();

                        if (formatter == null)
                        {
                            stringBuilder.AppendLine($"{template}~{state}");
                        }
                        else
                        {
                            stringBuilder.AppendLine($"{template}~{formatter.Invoke(state, exception)}");
                        }

                        while (exception != null)
                        {
                            var stackTrace = exception.StackTrace.Replace("\n", String.Empty)
                                                                 .Replace("\r", String.Empty)
                                                                 .Replace("\t", String.Empty);

                            stringBuilder.AppendLine($"{template}~{exception.GetType().Name}~{exception.Message}");
                            stringBuilder.AppendLine($"{template}~{exception.GetType().Name}~{stackTrace}");

                            exception = exception.InnerException;
                        }

                        WriteLog(dateTime, $"{stringBuilder}");
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
                }
            });
        }
        /// <summary>
        /// Write a log entry to file system.
        /// </summary>
        /// <param name="dateTime">
        /// Log entry datetime.
        /// </param>
        /// <param name="message">
        /// Message of log entry.
        /// </param>
        private void WriteLog(DateTime dateTime, String message)
        {
            var fileName = $"{Options.FileName}_{dateTime:yyyyMMdd}";
            var fullName = Path.Combine(Options.Path, $"{fileName}.log");
            var fileInfo = new FileInfo(fullName);

            Directory.CreateDirectory(Options.Path);

            if (fileInfo.Exists && fileInfo.Length >= Options.MaxSize)
            {
                var bakFileName = $"{fileName}_{dateTime:HHmmss}";
                var bakFullName = Path.Combine(Options.Path, $"{bakFileName}.log");

                while (File.Exists(bakFullName))
                {
                    bakFileName = $"{fileName}_{DateTime.UtcNow:HHmmss}";
                    bakFullName = Path.Combine(Options.Path, $"{bakFileName}.log");
                }

                File.Move(fullName, bakFullName);
            }

            using (var streamWriter = new StreamWriter(fullName, true, Encoding.UTF8))
            {
                streamWriter.Write(message);
            }
        }
    }
}