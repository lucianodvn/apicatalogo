using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace br.com.apicatalogo.Services.Interface
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAcessToken(IEnumerable<Claim> claims,
            IConfiguration _configuration);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token,
            IConfiguration _configuration);
    }
}

