using InventoryManagement.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class JwtTokenGenerator: IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public JwtTokenGenerator(IConfiguration configuration)  
        {
            _configuration = configuration;
        }
        public async Task<(string token ,DateTime expires)> GererateToken(string username,string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,username),
                new Claim("Role",role)
            };

            var expires = DateTime.Now.AddMinutes(30);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims:claims,
                expires: expires,
                signingCredentials:credentials );

            var tokenHandler = new JwtSecurityTokenHandler();
            var writtenToken = await Task.Run(() => tokenHandler.WriteToken(token));

            return (writtenToken,expires);
        }
    }
}
