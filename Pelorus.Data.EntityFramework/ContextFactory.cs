using System;
using System.Data.Entity;

namespace Pelorus.Data.EntityFramework
{
    /// <summary>
    /// A factory for creating an Entity Framework data context.
    /// </summary>
    /// <typeparam name="TContext">Type of the DbContext to manage.</typeparam>
    public class ContextFactory<TContext> : IContextFactory
        where TContext : DbContext, new()
    {
        private DbContext context;

        /// <summary>
        /// Releases any internally held resources.
        /// </summary>
        ~ContextFactory()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Creates new a data context.
        /// </summary>
        /// <returns>Entity Framework data context.</returns>
        public DbContext Create()
        {
            return this.Create(true);
        }

        /// <summary>
        /// Creates a data context.
        /// </summary>
        /// <param name="createNew">true if a new context should be created; otherwise false to reuse a previously created context instance.</param>
        /// <returns>Entity Framework data context.</returns>
        public DbContext Create(bool createNew)
        {
            if (true == createNew)
            {
                return new TContext();
            }

            if (null == this.context)
            {
                this.context = new TContext();
            }

            return this.context;
        }

        /// <summary>
        /// Dispose the resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the resources.
        /// </summary>
        /// <param name="disposing">true if this is being called because the class is being disposed; otherwise false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if ((false == disposing) || (null == this.context))
            {
                return;
            }

            this.context.Dispose();
            this.context = null;
        }
    }
}
