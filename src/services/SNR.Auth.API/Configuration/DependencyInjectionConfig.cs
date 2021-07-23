using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SNR.Auth.API.Application.Messages.Commands.UserCommand;
using SNR.Auth.Domain.Entities;
using SNR.Auth.Domain.Repositories;
using SNR.Auth.Infrastructure.Data;
using SNR.Auth.Infrastructure.Data.Repositories;
using SNR.Core.Extensions;
using SNR.Core.Mediator;

namespace SNR.Auth.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            // Usando com banco de dados em memória
            services.AddDbContext<AuthDbContext>(options => options.UseInMemoryDatabase("GenerateNotifications"));

            // Usando com SqlServer
            //services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SQLServerCs")));            

            services.AddIdentity<UserModel, IdentityRole>(options =>
                {
                    // Configurações de senha
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 2;
                    options.Password.RequiredUniqueChars = 0;
                })
                .AddErrorDescriber<IdentityPortugues>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddMediatR(typeof(AddUserCommand));

            return services;
        }

    }
}