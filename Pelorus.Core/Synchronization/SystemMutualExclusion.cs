using System;
using System.Threading;

namespace Pelorus.Core.Synchronization
{
    /// <summary>
    /// Creates and manages a named system level exclusive lock.
    /// </summary>
    public class SystemMutualExclusion : MutualExclusion
    {
        private readonly Mutex _systemMutex;

        /// <summary>
        /// Creates a new instance of the named lock and returns when ownership of the lock is obtained.
        /// </summary>
        /// <param name="name">Name of the lock.</param>
        public SystemMutualExclusion(string name) : base(name)
        {
            try
            {
                string lockName = $"Global\\{name}";
                this._systemMutex = new Mutex(false, lockName);
                bool ownHandle = this._systemMutex.WaitOne();

                if (false == ownHandle)
                {
                    this._systemMutex = null;
                    throw new Exception("Unable to obtain ownership of the mutex.");
                }
            }
            catch (AbandonedMutexException)
            {
                // Don't do anything if the mutex was abandoned in another process.
            }
        }

        /// <summary>
        /// Releases the system level lock.
        /// </summary>
        /// <param name="disposing">Indicates if the method is being called because the instance is being disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (null == this._systemMutex)
            {
                return;
            }

            try
            {
                this._systemMutex.ReleaseMutex();
                this._systemMutex.Dispose();
            }
            catch
            {
                // Don't need to do anything.
            }
        }
    }
}
