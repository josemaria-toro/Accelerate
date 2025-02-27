using System;

namespace Accelerate.Telemetry
{
    /// <summary>
    /// Service for tracking telemetry information about a services or applications.
    /// </summary>
    public interface ITelemetryService : IDisposable
    {
        /// <summary>
        /// Track the availablity information of a service or application.
        /// </summary>
        /// <param name="availability">
        /// Service or application availability information.
        /// </param>
        void Track(Availability availability);
        /// <summary>
        /// Track the dependency information of a service or application.
        /// </summary>
        /// <param name="dependency">
        /// Service or application dependency information.
        /// </param>
        void Track(Dependency dependency);
        /// <summary>
        /// Track a service or application event.
        /// </summary>
        /// <param name="event">
        /// Service or application event information.
        /// </param>
        void Track(Event @event);
        /// <summary>
        /// Track a service or application metric.
        /// </summary>
        /// <param name="metric">
        /// Service or application metric information.
        /// </param>
        void Track(Metric metric);
        /// <summary>
        /// Track a page view of a user.
        /// </summary>
        /// <param name="pageView">
        /// Page information.
        /// </param>
        void Track(PageView pageView);
        /// <summary>
        /// Track an http request information.
        /// </summary>
        /// <param name="request">
        /// Http request information.
        /// </param>
        void Track(Request request);
    }
}