using System;
using System.Data.Entity;

namespace Pelorus.Core.EntityFramework
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
    }
}
