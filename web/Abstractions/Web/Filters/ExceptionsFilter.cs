using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Accelerate.Web.Filters
{
    /// <summary>
    /// Base class for filters to handling exceptions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class ExceptionsFilter : Attribute, IExceptionFilter
    {
        /// <summary>
        /// Called after an action has thrown an exception.
        /// </summary>
        /// <param name="context">
        /// Filter context.
        /// </param>
        public abstract void OnException(ExceptionContext context);
    }
}
