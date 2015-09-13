using Microsoft.Practices.Unity;
using Pelorus.Core.IoC;
using System;

namespace Pelorus.Core.Unity
{
    /// <summary>
    /// Wrapper to perform IContainer operations on an Unity container.
    /// </summary>
    internal class UnityContainerWrapper : IContainer
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// Gets the unwrapped Unity container.
        /// </summary>
        internal IUnityContainer Container => this._container;

        /// <summary>
        /// Creates a new instance of the wrapper.
        /// </summary>
        /// <param name="container">Instance of a Unity container to wrap with the standard IContainer methods.</param>
        public UnityContainerWrapper(IUnityContainer container)
        {
            this._container = container;
        }

        /// <summary>
        /// Register an implementation of a contract type.
        /// </summary>
        /// <typeparam name="TContract">Type of the contract being registered.</typeparam>
        /// <typeparam name="TClass">Type of the implementation being registered.</typeparam>
        public void Register<TContract, TClass>()
            where TContract : class
            where TClass : class, TContract
        {
            this._container.RegisterType<TContract, TClass>();
        }

        /// <summary>
        /// Register an implementation of a contract type.
        /// </summary>
        /// <param name="contractType">Type of the contract being registered.</param>
        /// <param name="classType">Type of the implementation being registered.</param>
        public void Register(Type contractType, Type classType)
        {
            this._container.RegisterType(contractType, classType);
        }

        /// <summary>
        /// Checks if the given contract is already registered with the container.
        /// </summary>
        /// <typeparam name="TContract">Type of the contract to check for registration in the container.</typeparam>
        /// <returns>True if the contract type is already registered with the container otherwise false.</returns>
        public bool IsRegistered<TContract>()
            where TContract : class
        {
            return this._container.IsRegistered<TContract>();
        }

        /// <summary>
        /// Checks if the given contract is already registered with the container.
        /// </summary>
        /// <param name="contractType">Type of the contract to check for registration in the container.</param>
        /// <returns>True if the contract type is already registered with the container otherwise false.</returns>
        public bool IsRegistered(Type contractType)
        {
            return this._container.IsRegistered(contractType);
        }

        /// <summary>
        /// Gets an instance of the implementation of the given contract type.
        /// </summary>
        /// <typeparam name="TContract">Type of the contract to resolve and instantiate the implementation.</typeparam>
        /// <returns>Instance of the registered implementation of the contract type.</returns>
        public TContract Resolve<TContract>()
        {
            return this._container.Resolve<TContract>();
        }

        /// <summary>
        /// Gets an instance of the implementation of the given contract type.
        /// </summary>
        /// <param name="contractType">Type of the contract to resolve and instantiate the implementation.</param>
        /// <returns>Instance of the registered implementation of the contract type.</returns>
        public object Resolve(Type contractType)
        {
            return this._container.Resolve(contractType);
        }
    }
}
