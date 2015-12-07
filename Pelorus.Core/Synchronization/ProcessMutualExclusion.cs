using System.Collections.Generic;
using System.Threading;

namespace Pelorus.Core.Synchronization
{
    /// <summary>
    /// Creates and manages a named system level exclusive lock.
    /// </summary>
    public class ProcessExclusiveLock : ExclusiveLock
    {
        private static readonly IDictionary<string, object> _allLocks;
        private static readonly object _dictionaryLock;

        private readonly object lockObject;

        /// <summary>
        /// Initializes the static properties.
        /// </summary>
        static ProcessExclusiveLock()
        {
            _dictionaryLock = new object();

            lock (_dictionaryLock)
            {
                _allLocks = new Dictionary<string, object>();
            }
        }

        /// <summary>
        /// Creates a new instance of the named lock and returns when ownership of the lock is obtained.
        /// </summary>
        /// <param name="name">Name of the lock.</param>
        public ProcessExclusiveLock(string name) : base(name)
        {
            lock (_dictionaryLock)
            {
                if (false == _allLocks.TryGetValue(name, out this.lockObject))
                {
                    this.lockObject = new object();
                    _allLocks.Add(name, this.lockObject);
                }

                Monitor.Enter(this.lockObject);
            }
        }

        /// <summary>
        /// Releases the process level lock.
        /// </summary>
        /// <param name="disposing">Indicates if the method is being called because the instance is being disposed.</param>
        protected override void Dispose(bool disposing)
        {
            Monitor.Exit(this.lockObject);
        }
    }
}
