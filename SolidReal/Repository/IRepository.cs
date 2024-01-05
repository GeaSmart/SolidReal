namespace SolidReal.Repository
{
    public interface IRepository<T>
    {
        Task<List<T>> ObtenerAsync();
    }
}
