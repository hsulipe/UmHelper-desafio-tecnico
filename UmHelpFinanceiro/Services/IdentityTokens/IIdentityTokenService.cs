using Domain.Models.Dtos.Users;
using Domain.Models.Entities;

namespace UmHelpFinanceiro.Services.IdentityTokens
{
    public interface IIdentityTokenService
    {
        string GenerateToken(UserAccount user);
    }
}
