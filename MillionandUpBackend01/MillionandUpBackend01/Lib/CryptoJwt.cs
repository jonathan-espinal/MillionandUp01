using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MillionandUpBackend01.Lib {
    public class CryptoJwt {
        public static string CreateToken(Guid id, string username, string secretKey) {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, username),
                new Claim("Id", id.ToString()),
                new Claim("Username", username)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
