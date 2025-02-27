using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;

namespace Accelerate.Logging.Extensions
{
    /// <summary>
    /// Extension methods for ILoggingBuilder class.
    /// </summary>
    public static class ILoggingBuilderExtensions
    {
        /// <summary>
        /// Add a logging provider.
        /// </summary>
        /// <param name="loggingBuilder">
        /// Type for configuring logging providers.
        /// </param>
        /// <typeparam name="TProvider">
        /// Type of logger provider.
        /// </typeparam>
        /// <typeparam name="TOptions">
        /// Type of provider configuration options.
        /// </typeparam>
        public static ILoggingBuilder AddProvider<TProvider, TOptions>(this ILoggingBuilder loggingBuilder) where TProvider : LoggerProvider<TOptions>
                                                                                                            where TOptions : LoggerServiceOptions
        {
            loggingBuilder.AddConfiguration();

            var serviceDescriptor = ServiceDescriptor.Singleton<ILoggerProvider, TProvider>();

            loggingBuilder.Services.TryAddEnumerable(serviceDescriptor);

            LoggerProviderOptions.RegisterProviderOptions<TOptions, TProvider>(loggingBuilder.Services);

            return loggingBuilder;
        }
        /// <summary>
        /// Add a logging provider.
        /// </summary>
        /// <param name="loggingBuilder">
        /// Type for configuring logging providers.
        /// </param>
        /// <param name="options">
        /// Logging provider configuration options.
        /// </param>
        /// <typeparam name="TProvider">
        /// Type of logger provider.
        /// </typeparam>
        /// <typeparam name="TOptions">
        /// Type of provider configuration options.
        /// </typeparam>
        public static ILoggingBuilder AddProvider<TProvider, TOptions>(this ILoggingBuilder loggingBuilder, Action<TOptions> options) where TProvider : LoggerProvider<TOptions>
                                                                                                                                      where TOptions : LoggerServiceOptions
        {
            loggingBuilder.AddConfiguration();

            var serviceDescriptor = ServiceDescriptor.Singleton<ILoggerProvider, TProvider>();

            loggingBuilder.Services.TryAddEnumerable(serviceDescriptor);
            loggingBuilder.Services.Configure(options);

            return loggingBuilder;
        }
    }
}