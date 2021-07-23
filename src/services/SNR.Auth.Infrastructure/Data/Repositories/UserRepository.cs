using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SNR.Auth.Domain.Entities;
using SNR.Auth.Domain.Repositories;
using SNR.Core.Enums;
using SNR.Core.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SNR.Auth.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly TokenSettings _tokenSettings;

        public UserRepository(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, TokenSettings tokenSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenSettings = tokenSettings;
        }

        public async Task<UserModel> GetByUserNameAsync(string userName)
        {
            return await _userManager.Users
                .Where(a => a.Status == EntityStatusEnum.Active)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserName == userName);
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            return await _userManager.Users
                .AsNoTracking().ToListAsync();
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            return await _userManager.Users
                .Where(a => a.Status == EntityStatusEnum.Active)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IdentityResult> CreateAsync(UserModel user, string password)
        {
            return await _userManager.CreateAsync(user, password);

        }

        public async Task<SignInResult> SignInAsync(string userName, string password)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, isPersistent: false, lockoutOnFailure: true);
        }

        public async Task<bool> CheckPasswordAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return await _signInManager.UserManager.CheckPasswordAsync(user, password);
        }


        public string GenerateToken(string UserName)
        {
            // Define as claims do usuário (não é obrigatório, mas melhora a segurança (cria mais chaves no Payload))
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.UniqueName, UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            // Gera uma chave
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretJWTKey));

            // Gera a assinatura digital do token
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tempo de expiracão do token
            var expiracao = _tokenSettings.ExpireHours;
            var expiration = DateTime.UtcNow.AddHours(expiracao);

            // Gera o token
            JwtSecurityToken token = new JwtSecurityToken(
              issuer: _tokenSettings.Issuer,
              audience: _tokenSettings.Audience,
              claims: claims,
              expires: expiration,
              signingCredentials: credenciais);

            // Retorna o token e demais informações
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(UserModel obj)
        {
            throw new NotImplementedException();
        }

        public void Add(UserModel obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }


    }
}
