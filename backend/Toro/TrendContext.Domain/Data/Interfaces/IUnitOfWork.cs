using System.Threading.Tasks;

namespace TrendContext.Domain.Data.Interfaces
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        Task Commit();
        void Rollback();
    }
}
