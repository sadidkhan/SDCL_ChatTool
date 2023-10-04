using Microsoft.EntityFrameworkCore;

namespace SDCL_ChatTool.DAL
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext context;
        public GenericRepository(ChatDbContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
            context = dbContext;
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async void Delete<TIdentity>(TIdentity id)
        {
            var entity = await GetAsync(id);
            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TEntity> GetAsync<TIdentity>(TIdentity id)
        {
            return await _dbSet.FindAsync(id);
        }

        public TEntity Get(object id)
        {
            return _dbSet.Find(id);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);

        }

        public void UpdateEntity(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
