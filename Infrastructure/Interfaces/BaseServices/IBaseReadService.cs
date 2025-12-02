namespace Infrastructure.Interfaces.BaseServices
{
    public interface IBaseReadService<T, TSearch> where T : class where TSearch : class
    {
        public IEnumerable<T> Get(TSearch search = null);
        public T GetById(int id);
    }
}
