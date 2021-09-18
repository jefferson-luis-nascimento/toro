using System.Threading.Tasks;
using TrendContext.Domain.Entities;
using TrendContext.Shared.Repository;

namespace TrendContext.Domain.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> CheckCpfAlreadyExists(string cpf);
    }
}
