namespace SDCL_ChatTool.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;


        public UnitOfWork(ChatDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public T Repository<T>() where T : class
        {
            return _serviceProvider.GetService(typeof(T)) as T;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
