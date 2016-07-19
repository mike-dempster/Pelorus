using Pelorus.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Pelorus.Data.EntityFramework
{
    /// <summary>
    /// Builds an expression tree to null out navigation properties on Entity Framework entities.
    /// </summary>
    internal static class EntityNavigationExpressionBuilder
    {
        /// <summary>
        /// Build an expression for the given entity type.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to build the expression tree for.</typeparam>
        /// <returns>Expression that when compiled and invoked will null out the navigation properties on the entity.</returns>
        public static Expression<Action<TEntity>> BuildExpression<TEntity>()
            where TEntity : class
        {
            var entityType = typeof(TEntity);
            var properties = entityType.GetProperties();
            var expressions = new List<Expression>();
            var inputParam = Expression.Variable(typeof(TEntity));
            expressions.Add(inputParam);

            foreach (var prop in properties)
            {
                if (IsPropertyEntityType(prop.PropertyType))
                {
                    var expr = CreateAssignNullExpression(prop, inputParam);
                    expressions.Add(expr);
                }
            }

            var block = Expression.Block(expressions);
            var nullNavPropertiesExpression = Expression.Lambda<Action<TEntity>>(block, inputParam);

            return nullNavPropertiesExpression;
        }

        /// <summary>
        /// Creates an expression that assigns a null value to the given property on the entity.
        /// </summary>
        /// <param name="property">Property to null out.</param>
        /// <param name="variableExpression">Input variable of the entity.</param>
        /// <returns>Null assignment expression.</returns>
        private static Expression CreateAssignNullExpression(PropertyInfo property, Expression variableExpression)
        {
            if (null == property)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (null == variableExpression)
            {
                throw new ArgumentNullException(nameof(variableExpression));
            }

            var constExpr = Expression.Constant(null, property.PropertyType);
            var propertyExpr = Expression.Property(variableExpression, property);
            var nullAssignmentExpr = Expression.Assign(propertyExpr, constExpr);

            return nullAssignmentExpr;
        }

        /// <summary>
        /// Determines if the property on an entity is a navigation property.
        /// </summary>
        /// <param name="propertyType">Type of the property on the entity.</param>
        /// <returns>True if the property is a navigation property otherwise false.</returns>
        private static bool IsPropertyEntityType(Type propertyType)
        {
            if (null == propertyType)
            {
                throw new ArgumentNullException(nameof(propertyType));
            }

            if (propertyType.IsValueType)
            {
                return false;
            }

            if (typeof(IEnumerable).IsAssignableFrom(propertyType))
            {
                if (false == propertyType.IsGenericType)
                {
                    return false;
                }

                propertyType = propertyType.GetGenericArguments().First();
            }

            var baseType = GetBaseType(propertyType.BaseType);

            while (null != baseType)
            {
                if (typeof(Entity<>) == baseType)
                {
                    return true;
                }

                if (null == baseType.BaseType)
                {
                    break;
                }

                baseType = GetBaseType(baseType.BaseType);
            }

            return false;
        }

        /// <summary>
        /// Gets the base type of a property type.
        /// </summary>
        /// <param name="propertyBaseType">property's base type.</param>
        /// <returns>Base type of the given property type.</returns>
        private static Type GetBaseType(Type propertyBaseType)
        {
            if (null == propertyBaseType)
            {
                throw new ArgumentNullException(nameof(propertyBaseType));
            }

            var baseType = propertyBaseType;

            if (baseType.IsGenericType)
            {
                baseType = baseType.GetGenericTypeDefinition();
            }

            return baseType;
        }
    }
}
