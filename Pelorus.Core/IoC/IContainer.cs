using System;

namespace Pelorus.Core.IoC
{
    /// <summary>
    /// Standard contract for IoC containers.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Register an implementation of a contract type.
        /// </summary>
        /// <typeparam name="TContract">Type of the contract being registered.</typeparam>
        /// <typeparam name="TClass">Type of the implementation being registered.</typeparam>
        void Register<TContract, TClass>()
            where TContract : class
            where TClass : class, TContract;

        /// <summary>
        /// Register an implementation of a contract type.
        /// </summary>
        /// <param name="contractType">Type of the contract being registered.</param>
        /// <param name="classType">Type of the implementation being registered.</param>
        void Register(Type contractType, Type classType);

        /// <summary>
        /// Checks if the given contract is already registered with the container.
        /// </summary>
        /// <typeparam name="TContract">Type of the contract to check for registration in the container.</typeparam>
        /// <returns>True if the contract type is already registered with the container otherwise false.</returns>
        bool IsRegistered<TContract>()
            where TContract : class;

        /// <summary>
        /// Checks if the given contract is already registered with the container.
        /// </summary>
        /// <param name="contractType">Type of the contract to check for registration in the container.</param>
        /// <returns>True if the contract type is already registered with the container otherwise false.</returns>
        bool IsRegistered(Type contractType);

        /// <summary>
        /// Gets an instance of the implementation of the given contract type.
        /// </summary>
        /// <typeparam name="TContract">Type of the contract to resolve and instantiate the implementation.</typeparam>
        /// <returns>Instance of the registered implementation of the contract type.</returns>
        TContract Resolve<TContract>();

        /// <summary>
        /// Gets an instance of the implementation of the given contract type.
        /// </summary>
        /// <param name="contractType">Type of the contract to resolve and instantiate the implementation.</param>
        /// <returns>Instance of the registered implementation of the contract type.</returns>
        object Resolve(Type contractType);
    }
}
