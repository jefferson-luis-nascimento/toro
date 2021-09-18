using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrendContext.Domain.Data;
using TrendContext.Domain.Entities;
using TrendContext.Domain.Repositories.Interfaces;

namespace TrendContext.Domain.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(InMemoryAppContext appContext) 
            : base (appContext)
        {

        }

        public async Task<bool> CheckCpfAlreadyExists(string cpf)
        {
            return await entities.AnyAsync(user => user.CPF == cpf);
        }

        public async Task<User> GetByCPF(string cpf)
        {
            return await entities.FirstOrDefaultAsync(user => user.CPF == cpf);
        }
    }
}
