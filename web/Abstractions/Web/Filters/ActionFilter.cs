using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Accelerate.Web.Filters
{
    /// <summary>
    /// Base class for common filters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class ActionFilter : Attribute, IActionFilter
    {
        /// <summary>
        /// Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context">
        /// Filter context.
        /// </param>
        public abstract void OnActionExecuted(ActionExecutedContext context);
        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">
        /// Filter context.
        /// </param>
        public abstract void OnActionExecuting(ActionExecutingContext context);
    }
}
