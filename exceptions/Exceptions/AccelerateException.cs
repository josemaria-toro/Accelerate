using System;
using System.Collections.Generic;

namespace Accelerate
{
    /// <summary>
    /// Represent the base class for error that occur during program execution.
    /// </summary>
    public abstract class AccelerateException : Exception
    {
        /// <summary>
        /// Initialize a new instance of class.
        /// </summary>
        public AccelerateException() : base()
        {
        }
        /// <summary>
        /// Initializes a new instance of class with a specified error message.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public AccelerateException(String message) : base(message)
        {
        }
        /// <summary>
        /// Initializes a new instance of class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public AccelerateException(String message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Exception metadata.
        /// </summary>
        public virtual IDictionary<String, Object> Metadata { get; set; }
    }
}