using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace RestApi.Service.Interfaces
{
    public interface ITokenService
    {
        public JwtSecurityToken GetToken(List<Claim> authClaims);
    }
}
