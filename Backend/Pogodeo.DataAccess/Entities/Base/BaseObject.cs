namespace Pogodeo.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseObject<T> : IBaseObject<T>
    {
        public T Id { get; set; }
    }
}
