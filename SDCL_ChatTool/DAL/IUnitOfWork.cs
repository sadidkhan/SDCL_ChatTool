namespace SDCL_ChatTool.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        Task<int> SaveChangesAsync();
        T Repository<T>() where T : class;
    }
}
