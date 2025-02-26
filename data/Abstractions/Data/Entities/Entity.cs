using System;

namespace Accelerate.Data.Entities
{
    /// <summary>
    /// Base class for data entities.
    /// </summary>
    [Serializable]
    public abstract class Entity : IEntity
    {
        private Boolean _disposed;

        /// <summary>
        /// Initialize a new instance of class.
        /// </summary>
        protected Entity()
        {
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        public virtual Object Clone()
        {
            var type = GetType();
            var instance = Activator.CreateInstance(type);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(this);
                property.SetValue(instance, value);
            }

            return instance;
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// Indicate if object is currently freeing, releasing, or resetting unmanaged resources.
        /// </param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            _disposed = true;
        }
    }
}