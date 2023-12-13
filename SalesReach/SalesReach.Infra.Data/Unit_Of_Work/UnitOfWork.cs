using SalesReach.Domain.Repositories.Interface;

namespace SalesReach.Infra.Data.Unit_Of_Work
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _session;

        public UnitOfWork(DbSession session)
        {
            _session = session;
        }

        public async Task BeginTransation()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public async Task Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public async Task Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
