namespace Pogodeo.DataAccess
{
    public interface IBaseObject<T>
    {
        T Id { get; set; }
    }
}