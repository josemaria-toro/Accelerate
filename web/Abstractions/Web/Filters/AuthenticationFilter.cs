using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Accelerate.Web.Filters
{
    /// <summary>
    /// Base filter to confirm if requests are authorized.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Called early in the filter pipeline to confirm request is authorized.
        /// </summary>
        /// <param name="context">
        /// Filter context.
        /// </param>
        public abstract void OnAuthorization(AuthorizationFilterContext context);
    }
}
