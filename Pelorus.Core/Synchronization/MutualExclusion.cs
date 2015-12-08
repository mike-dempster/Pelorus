﻿using System;
using System.Collections.Generic;

namespace Pelorus.Core.Synchronization
{
    /// <summary>
    /// Base class for mutual exclusion providers.
    /// </summary>
    public abstract class ExclusiveLock : IDisposable
    {
        [ThreadStatic]
        private static List<string> locksOwnedInScope;

        /// <summary>
        /// When overriden in a derived class, disposes of the exclusive lock.
        /// </summary>
        /// <param name="disposing">Indicates that the exclusive lock instance is being disposed.</param>
        protected abstract void Dispose(bool disposing);

        /// <summary>
        /// Name of the exclusive lock.
        /// </summary>
        protected string Name { get; }

        /// <summary>
        /// Creates a new instance of the exclusive lock with the given name.
        /// </summary>
        /// <param name="name">Name of the lock to obtain.</param>
        protected ExclusiveLock(string name)
        {
            this.Name = name;

            if (null == locksOwnedInScope)
            {
                locksOwnedInScope = new List<string>();
            }

            locksOwnedInScope.Add(name);
        }

        /// <summary>
        /// Checks if ownership of the lock is already owned by the current thread.
        /// </summary>
        /// <returns>true if the thread already owns the exclusive lock otherwise false.</returns>
        protected bool ExclusionOwned()
        {
            return locksOwnedInScope.Contains(this.Name);
        }

        /// <summary>
        /// Releases the exclusive lock.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            locksOwnedInScope.Remove(this.Name);
        }
    }
}
