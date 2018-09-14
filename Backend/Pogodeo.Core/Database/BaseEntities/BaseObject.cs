namespace Pogodeo.Core
{
    /// <summary>
    /// The base object for every entity to derive from
    /// </summary>
    public abstract class BaseObject<T> : IBaseObject<T>
    {
        /// <summary>
        /// The ID of this entity
        /// </summary>
        public T Id { get; set; }
    }
}
