namespace Pogodeo.Core
{
    /// <summary>
    /// The base interface for every entity to implement
    /// </summary>
    /// <typeparam name="T">The type of ID to use, int is most common</typeparam>
    public interface IBaseObject<T>
    {
        /// <summary>
        /// The ID of this entity
        /// </summary>
        T Id { get; set; }
    }
}