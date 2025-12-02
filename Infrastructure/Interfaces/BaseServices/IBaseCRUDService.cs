namespace Infrastructure.Interfaces.BaseServices
{
    public interface IBaseCRUDService<T, TSearch, TInsert, TUpdate> : IBaseReadService<T, TSearch>
       where T : class where TSearch : class where TInsert : class where TUpdate : class
    {
        Task<T> Insert(TInsert request);
        Task<T> Update(int id, TUpdate request);
        Task<T> Delete(int id, bool hardDelete = false);
    }
}
