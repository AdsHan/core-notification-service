using Microsoft.AspNetCore.Identity;
using SNR.Auth.Domain.Entities;
using SNR.Core.Data;
using System.Threading.Tasks;

namespace SNR.Auth.Domain.Repositories
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<UserModel> GetByUserNameAsync(string userName);
        Task<IdentityResult> CreateAsync(UserModel usiario, string userName);
        Task<SignInResult> SignInAsync(string userName, string password);
        Task<bool> CheckPasswordAsync(string userName, string password);
        string GenerateToken(string UserName);
    }
}
