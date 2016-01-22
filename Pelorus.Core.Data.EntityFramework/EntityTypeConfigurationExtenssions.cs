using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq.Expressions;

namespace Pelorus.Core.Data.EntityFramework
{
    /// <summary>
    /// Extension methods for the EntityTypeConfiguration&lt;T&gt; class.
    /// </summary>
    public static class EntityTypeConfigurationExtensions
    {
        /// <summary>
        /// Maps a one-to-many relationship where the child entities have a non-nullable foreign key to the parent entity.
        /// </summary>
        /// <typeparam name="TParent">Type of the parent entity.</typeparam>
        /// <typeparam name="TChild">Type of the child entity.</typeparam>
        /// <typeparam name="TKey">Type of the foreign key from the child to the parent entity.</typeparam>
        /// <param name="config">Instance of an entity type configuration.</param>
        /// <param name="navPropertyToParent">Navigation property from the child entity to the parent entity.</param>
        /// <param name="navPropertyToChild">Navigation property from the parent property to the child entities.</param>
        /// <param name="foreignKeyProperty">Property on the child entity that is the foreign key to the parent entity.</param>
        public static void OneToMany<TParent, TChild, TKey>(
            this EntityTypeConfiguration<TParent> config,
            Expression<Func<TChild, TParent>> navPropertyToParent,
            Expression<Func<TParent, ICollection<TChild>>> navPropertyToChild,
            Expression<Func<TChild, TKey>> foreignKeyProperty)
            where TParent : class
            where TChild : class
        {
            config.HasMany(navPropertyToChild)
                  .WithRequired(navPropertyToParent)
                  .HasForeignKey(foreignKeyProperty);
        }

        /// <summary>
        /// Maps a many-to-one relationship where the child entities have a non-nullable foreign key to the parent entity.
        /// </summary>
        /// <typeparam name="TParent">Type of the parent entity.</typeparam>
        /// <typeparam name="TChild">Type of the child entity.</typeparam>
        /// <typeparam name="TKey">Type of the foreign key from the child to the parent entity.</typeparam>
        /// <param name="config">Instance of an entity type configuration.</param>
        /// <param name="navPropertyToChild">Navigation property from the parent property to the child entities.</param>
        /// <param name="navPropertyToParent">Navigation property from the child entity to the parent entity.</param>
        /// <param name="foreignKeyProperty">Property on the child entity that is the foreign key to the parent entity.</param>
        public static void ManyToOne<TParent, TChild, TKey>(
            this EntityTypeConfiguration<TChild> config,
            Expression<Func<TParent, ICollection<TChild>>> navPropertyToChild,
            Expression<Func<TChild, TParent>> navPropertyToParent,
            Expression<Func<TChild, TKey>> foreignKeyProperty)
            where TParent : class
            where TChild : class
        {
            config.HasRequired(navPropertyToParent)
                  .WithMany(navPropertyToChild)
                  .HasForeignKey(foreignKeyProperty);
        }

        /// <summary>
        /// Maps a one-to-many relationship where the child entities have a nullable foreign key to the parent entities.
        /// </summary>
        /// <typeparam name="TParent">Type of the parent entity.</typeparam>
        /// <typeparam name="TChild">Type of the child entity.</typeparam>
        /// <typeparam name="TKey">Type of the foreign key from the child to the parent entity.</typeparam>
        /// <param name="config">Instance of an entity type configuration.</param>
        /// <param name="navPropertyToChild">Navigation property from the parent property to the child entities.</param>
        /// <param name="navPropertyToParent">Navigation property from the child entity to the parent entity.</param>
        /// <param name="foreignKeyProperty">Property on the child entity that is the foreign key to the parent entity.</param>
        public static void ZeroOrOneToMany<TParent, TChild, TKey>(
            this EntityTypeConfiguration<TParent> config,
            Expression<Func<TChild, TParent>> navPropertyToParent,
            Expression<Func<TParent, ICollection<TChild>>> navPropertyToChild,
            Expression<Func<TChild, TKey>> foreignKeyProperty)
            where TParent : class
            where TChild : class
        {
            config.HasMany(navPropertyToChild)
                  .WithOptional(navPropertyToParent)
                  .HasForeignKey(foreignKeyProperty);
        }

        /// <summary>
        /// Maps a one-to-many relationship where the child entities have a nullable foreign key to the parent entities.
        /// </summary>
        /// <typeparam name="TParent">Type of the parent entity.</typeparam>
        /// <typeparam name="TChild">Type of the child entity.</typeparam>
        /// <typeparam name="TKey">Type of the foreign key from the child to the parent entity.</typeparam>
        /// <param name="config">Instance of an entity type configuration.</param>
        /// <param name="navPropertyToChild">Navigation property from the parent property to the child entities.</param>
        /// <param name="navPropertyToParent">Navigation property from the child entity to the parent entity.</param>
        /// <param name="foreignKeyProperty">Property on the child entity that is the foreign key to the parent entity.</param>
        public static void ManyToZeroOrOne<TParent, TChild, TKey>(
            this EntityTypeConfiguration<TChild> config,
            Expression<Func<TParent, ICollection<TChild>>> navPropertyToChild,
            Expression<Func<TChild, TParent>> navPropertyToParent,
            Expression<Func<TChild, TKey>> foreignKeyProperty)
            where TParent : class
            where TChild : class
        {
            config.HasOptional(navPropertyToParent)
                  .WithMany(navPropertyToChild)
                  .HasForeignKey(foreignKeyProperty);
        }

        /// <summary>
        /// Maps a many-to-many relationship between two entities.
        /// </summary>
        /// <typeparam name="TLeft">Type of the &quot;Left&quot; entity.</typeparam>
        /// <typeparam name="TRight">Type of the &quot;Right&quot; entity.</typeparam>
        /// <param name="config">Instance of an entity type configuration.</param>
        /// <param name="navPropertyLeft">Navigation property from the left entity to the right entities.</param>
        /// <param name="navPropertyRight">Navigation property from the right entity to the left entities.</param>
        /// <param name="tableName">Name of the junction table for the many-to-many association.</param>
        /// <param name="leftKey">Name of the column in the junction table to the left entity.</param>
        /// <param name="rightKey">Name of the column in the junction table to the right entity.</param>
        public static void ManyToMany<TLeft, TRight>(
            this EntityTypeConfiguration<TLeft> config,
            Expression<Func<TLeft, ICollection<TRight>>> navPropertyLeft,
            Expression<Func<TRight, ICollection<TLeft>>> navPropertyRight,
            string tableName,
            string leftKey,
            string rightKey)
            where TLeft : class
            where TRight : class
        {
            config.HasMany(navPropertyLeft)
                  .WithMany(navPropertyRight)
                  .Map(e =>
                  {
                      e.ToTable(tableName);
                      e.MapLeftKey(leftKey);
                      e.MapRightKey(rightKey);
                  });
        }

        /// <summary>
        /// Maps a many-to-many relationship between two entities.
        /// </summary>
        /// <typeparam name="TLeft">Type of the &quot;Left&quot; entity.</typeparam>
        /// <typeparam name="TRight">Type of the &quot;Right&quot; entity.</typeparam>
        /// <param name="config">Instance of an entity type configuration.</param>
        /// <param name="navPropertyLeft">Navigation property from the left entity to the right entities.</param>
        /// <param name="navPropertyRight">Navigation property from the right entity to the left entities.</param>
        /// <param name="tableName">Name of the junction table for the many-to-many association.</param>
        /// <param name="schemaName">Schema name of the junction table.</param>
        /// <param name="leftKey">Name of the column in the junction table to the left entity.</param>
        /// <param name="rightKey">Name of the column in the junction table to the right entity.</param>
        public static void ManyToMany<TLeft, TRight>(
            this EntityTypeConfiguration<TLeft> config,
            Expression<Func<TLeft, ICollection<TRight>>> navPropertyLeft,
            Expression<Func<TRight, ICollection<TLeft>>> navPropertyRight,
            string tableName,
            string schemaName,
            string leftKey,
            string rightKey)
            where TLeft : class
            where TRight : class
        {
            config.HasMany(navPropertyLeft)
                  .WithMany(navPropertyRight)
                  .Map(e =>
                  {
                      e.ToTable(tableName, schemaName);
                      e.MapLeftKey(leftKey);
                      e.MapRightKey(rightKey);
                  });
        }
    }
}
