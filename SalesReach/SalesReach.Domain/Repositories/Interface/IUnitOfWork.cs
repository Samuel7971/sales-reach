namespace SalesReach.Domain.Repositories.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransation();
        Task Commit();
        Task Rollback();
    }
}
