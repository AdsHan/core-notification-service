using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SNR.Auth.Domain.Entities;

namespace SNR.Auth.Infrastructure.Data
{
    public class AuthDbContext : IdentityDbContext<UserModel>
    {

        public AuthDbContext()
        {

        }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

    }
}
