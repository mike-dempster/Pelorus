using System;

namespace Pelorus.Core.Synchronization
{
    /// <summary>
    /// Base class for mutual exclusion providers.
    /// </summary>
    public abstract class ExclusiveLock : IDisposable
    {
        // TODO: Implement a method that derived classes can use to check if it is operating within the scope of a previous instance of the mutual exclusion.

        /// <summary>
        /// When overriden in a derived class, disposes of the exclusive lock.
        /// </summary>
        /// <param name="disposing">Indicates that the exclusive lock instance is being disposed.</param>
        protected abstract void Dispose(bool disposing);

        /// <summary>
        /// Name of the exclusive lock.
        /// </summary>
        protected string Name { get; private set; }

        /// <summary>
        /// Creates a new instance of the exclusive lock with the given name.
        /// </summary>
        /// <param name="name">Name of the lock to obtain.</param>
        protected ExclusiveLock(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Releases the exclusive lock.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
