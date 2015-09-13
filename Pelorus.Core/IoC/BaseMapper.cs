namespace Pelorus.Core.IoC
{
    /// <summary>
    /// Base functionality for IoC container type mappers.
    /// </summary>
    public abstract class BaseMapper
    {
        private IContainer container;

        /// <summary>
        /// When overridden in a derived class, registers all contract and implementation types in the container.
        /// </summary>
        public abstract void MapContracts();

        /// <summary>
        /// Registers all contracts in the given container.
        /// </summary>
        /// <param name="containerInstance">Instance of an IoC container to register the types with.</param>
        internal void InternalMapContracts(IContainer containerInstance)
        {
            this.container = containerInstance;
            this.MapContracts();
            this.container = null;
        }

        /// <summary>
        /// Maps a contract type to an implementation type in the container if it has not already been registered.
        /// </summary>
        /// <typeparam name="TContract">Type of the contract to register with the container.</typeparam>
        /// <typeparam name="TClass">Type of the implementation to register with the container.</typeparam>
        protected void Map<TContract, TClass>()
            where TContract : class
            where TClass : class, TContract
        {
            if (true == this.container.IsRegistered<TContract>())
            {
                return;
            }

            this.container.Register<TContract, TClass>();
        }
    }
}
