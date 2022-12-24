using JWTApp.BackOffice.Core.DTOs;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTApp.BackOffice.Infrastructure.Tools
{
    public class JWTGenerator
    {
        public static JWTRepsonse GenerateToken(CheckUserResponseDTO dto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key));
            SigningCredentials credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();

            //foreach (var item in new string[] {"",""}) birden fazla rol olsaydı bu şekilde geçerli olanları alıp, döngü ile ekleyebilirdik
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            //}

            claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            claims.Add(new Claim(ClaimTypes.Name, dto.Username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()));

            var expireDate = DateTime.UtcNow.AddMinutes(JWTSettings.Expire);
            //var expireDate2 = DateTime.UtcNow.AddMinutes(1);

            JwtSecurityToken token = new JwtSecurityToken(issuer:JWTSettings.Issuer, audience:JWTSettings.Audience, claims:claims, notBefore:DateTime.UtcNow, expires:expireDate, signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return new JWTRepsonse(handler.WriteToken(token), expireDate);
        }
    }
}
