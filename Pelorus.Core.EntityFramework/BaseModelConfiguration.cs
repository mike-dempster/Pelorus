using Pelorus.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Pelorus.Core.Data
{
    /// <summary>
    /// Base class for Entity Framework entity configuration classes.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity that is being configured.</typeparam>
    public abstract class ModelConfigurationBase<TEntity, TKey> : EntityTypeConfiguration<TEntity>
        where TEntity : EntityDao<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Map an entity to a view.
        /// </summary>
        /// <param name="viewName">Name of the view to map the entity to.</param>
        protected void ToView(string viewName)
        {
            this.ToTable(viewName);
        }

        /// <summary>
        /// Map an entity to a view.
        /// </summary>
        /// <param name="viewName">Name of the view to map the entity to.</param>
        /// <param name="schemaName">Schema name of the view.</param>
        protected void ToView(string viewName, string schemaName)
        {
            this.ToTable(viewName, schemaName);
        }
    }
}
