using System;
using System.Data.Entity;

namespace Pelorus.Core.Data.EntityFramework
{
    /// <summary>
    /// A factory for creating an Entity Framework data context.
    /// </summary>
    public interface IContextFactory : IDisposable
    {
        /// <summary>
        /// Creates a data context.
        /// </summary>
        /// <returns>Entity Framework data context.</returns>
        DbContext Create();

        /// <summary>
        /// Creates a data context.
        /// </summary>
        /// <param name="createNew">true if a new context should be created; otherwise false to reuse a previously created context instance.</param>
        /// <returns>Entity Framework data context.</returns>
        DbContext Create(bool createNew);
    }
}
