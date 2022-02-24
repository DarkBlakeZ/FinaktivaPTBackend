using FinaktivaPT.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinaktivaPT.Authentificaticon
{
    public class TokenAuthenticationService: ITokenService
    {
        private readonly TokenManagement _tokenManagement;
        public TokenAuthenticationService(IOptions<TokenManagement> tokenManagement)
        {
            _tokenManagement = tokenManagement.Value;
        }
        public bool IsAuthenticated(CUUsuarios request, out string token)
        {
            token = string.Empty;

            var claims = new[]
            {
                new Claim("id", request.Id.ToString()),
                new Claim("name", request.Nombre),
                new Claim("username",request.Username),
                new Claim("rol",request.Rol.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_tokenManagement.Issuer, 
                _tokenManagement.Audience, 
                claims, 
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration), 
                signingCredentials: credentials);

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return true;
        }
    }
}
