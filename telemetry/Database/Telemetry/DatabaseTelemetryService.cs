using Accelerate.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Accelerate.Telemetry
{
    /// <summary>
    /// Telemetry service based on database.
    /// </summary>
    public sealed class DatabaseTelemetryService : TelemetryService<DatabaseTelemetryServiceOptions>
    {
        private readonly IDbConnection _connection;
        private Boolean _disposed;
        private JsonSerializerOptions _jsonSerializerOptions;
        private SemaphoreSlim _semaphore;

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options.
        /// </param>
        /// <param name="connection">
        /// Database connection.
        /// </param>
        public DatabaseTelemetryService(IOptions<DatabaseTelemetryServiceOptions> options, IDbConnection connection) : base(options)
        {
            _connection = connection ?? throw new ArgumentException("Database connection cannot be null", nameof(connection));

            if (connection.State == ConnectionState.Broken ||
                connection.State == ConnectionState.Closed)
            {
                throw new InvalidOperationException("Database connection has an invalid state");
            }

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                AllowTrailingCommas = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                IgnoreReadOnlyFields = false,
                IgnoreReadOnlyProperties = false,
                IncludeFields = false,
                MaxDepth = 32,
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
                PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReadCommentHandling = JsonCommentHandling.Skip,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
                WriteIndented = false
            };

            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
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
                _jsonSerializerOptions = null;
                _semaphore.Dispose();
                _semaphore = null;
            }

            _disposed = true;
        }
        /// <inheritdoc />
        public override void Track(Availability availability)
        {
            if (availability == null)
            {
                throw new ArgumentException("Availability information cannot be null", nameof(availability));
            }

            Task.Run(async () =>
            {
                var queryString = "INSERT INTO availability VALUES (@id, @timestamp, @appname, @name, @message, @success, @correlationid, @metadata);";

                await _semaphore.WaitAsync();

                try
                {
                    using (var command = _connection.CreateCommand(queryString, availability, _jsonSerializerOptions))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            });
        }
        /// <inheritdoc />
        public override void Track(Dependency dependency)
        {
            if (dependency == null)
            {
                throw new ArgumentException("Dependency information cannot be null", nameof(dependency));
            }

            Task.Run(async () =>
            {
                var queryString = "INSERT INTO dependency VALUES (@id, @timestamp, @appname, @name, @duration, @resultcode, @target, @type, @data, @success, @correlationid, @metadata);";

                await _semaphore.WaitAsync();

                try
                {
                    using (var command = _connection.CreateCommand(queryString, dependency, _jsonSerializerOptions))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            });
        }
        /// <inheritdoc />
        public override void Track(Event @event)
        {
            if (@event == null)
            {
                throw new ArgumentException("Event information cannot be null", nameof(@event));
            }

            Task.Run(async () =>
            {
                var queryString = "INSERT INTO events VALUES (@id, @timestamp, @appname, @name, @correlationid, @metadata);";

                await _semaphore.WaitAsync();

                try
                {
                    using (var command = _connection.CreateCommand(queryString, @event, _jsonSerializerOptions))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            });
        }
        /// <inheritdoc />
        public override void Track(Metric metric)
        {
            if (metric == null)
            {
                throw new ArgumentException("Metric information cannot be null", nameof(metric));
            }

            Task.Run(async () =>
            {
                var queryString = "INSERT INTO metrics VALUES (@id, @timestamp, @appname, @name, @dimension, @value, @correlationid, @metadata);";

                await _semaphore.WaitAsync();

                try
                {
                    using (var command = _connection.CreateCommand(queryString, metric, _jsonSerializerOptions))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            });
        }
        /// <inheritdoc />
        public override void Track(PageView pageView)
        {
            if (pageView == null)
            {
                throw new ArgumentException("Page view information cannot be null", nameof(pageView));
            }

            Task.Run(async () =>
            {
                var queryString = "INSERT INTO pageViews VALUES (@id, @timestamp, @appname, @name, @duration, @uri, @correlationid, @metadata);";

                await _semaphore.WaitAsync();

                try
                {
                    using (var command = _connection.CreateCommand(queryString, pageView, _jsonSerializerOptions))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            });
        }
        /// <inheritdoc />
        public override void Track(Request request)
        {
            if (request == null)
            {
                throw new ArgumentException("Request information cannot be null", nameof(request));
            }

            Task.Run(async () =>
            {
                var queryString = "INSERT INTO requests VALUES (@id, @timestamp, @appname, @name, @duration, @responsecode, @client, @uri, @correlationid, @metadata);";

                await _semaphore.WaitAsync();

                try
                {
                    using (var command = _connection.CreateCommand(queryString, request, _jsonSerializerOptions))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            });
        }
    }
}