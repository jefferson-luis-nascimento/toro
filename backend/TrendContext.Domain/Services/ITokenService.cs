using TrendContext.Domain.Entities;

namespace TrendContext.Domain.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
