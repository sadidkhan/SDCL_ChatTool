namespace SDCL_ChatTool.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetQuery();
        TEntity Get(object id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void UpdateEntity(TEntity entity);
        void Delete<TIdentity>(TIdentity id);

        // async
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entity);
        Task<TEntity> GetAsync<TIdentity>(TIdentity id);

    }
}
