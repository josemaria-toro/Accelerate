using System;

namespace Accelerate
{
    /// <summary>
    /// Represent an error that occur when a service or object is unavailable.
    /// </summary>
    public class UnavailableException : AccelerateException
    {
        /// <summary>
        ///Initializes a new instance of class.
        /// </summary>
        public UnavailableException() : base()
        {
        }
        /// <summary>
        /// Initializes a new instance of class with a specified error message.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public UnavailableException(String message) : base(message)
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
        public UnavailableException(String message, Exception innerException) : base(message, innerException)
        {
        }
    }
}