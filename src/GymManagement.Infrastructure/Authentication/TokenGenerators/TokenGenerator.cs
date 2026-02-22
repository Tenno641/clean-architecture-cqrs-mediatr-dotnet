using System.IdentityModel.Tokens.Jwt;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Users;
using Microsoft.IdentityModel.Tokens;

namespace GymManagement.Infrastructure.Authentication;

public class TokenGenerator : ITokenGenerator
{
    private readonly JwtOptions _jwtOptions;
    
    public TokenGenerator(JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }
    
    public string GenerateToken(User user)
    {
        
        JwtSecurityToken token = new JwtSecurityToken()
        
        SigningCredentials signingCredentials = new SigningCredentials()


        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
    }
}