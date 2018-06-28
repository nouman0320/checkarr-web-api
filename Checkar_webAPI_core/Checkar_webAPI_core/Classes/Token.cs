using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Classes
{
    public class Token
    {
        public JwtSecurityToken token = null;

        public void GenerateToken(String email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            string privateSecretKey = "OfED+KgbZxtu4e4+JSQWdtSgTnuNixKy1nMVAEww8QL3IN3idcNgbNDSSaV4491Fo3sq2aGSCtYvekzs7JwXJnNAyvDSJjfK/7M8MpxSMnm1vMscBXyiYFXhGC4wqWlYBE828/5DNyw3QZW5EjD7hvDrY5OlYd4smCTa53helNnJz5NT9HQaDbE2sMwIDAQABAoIBAEs63TvT94njrPDP3A/sfCEXg1F2y0D/PjzUhM1aJGcRiOUXnGlYdViGhLnnJoNZTZm9qI1LT0NWcDA5NmBN6gcrk2EApyTt1D1i4AQ66rYoTF9iEC4Wye28v245BYESA6IIelgIxXGsVyllERsbTkaphzibbYfHmvwMxkn135Zfzd/NOXl/O32vYIomzrNEP+tN2WXhhG8c8+iZ8PErBV3CqrYogYy97d2CeQbXcpd5unPiU4TK0nnzeBAXdgeYuJHFC45YHl9UvShRoe6CHR47ceIGp6WMc5BTyyTkZpctuYJTwaChdj/QuRSkTYmn6jFL+MRfYQJ8VVwSVo5DbkECgYEA4/YIMKcwObYcSuHzgkMwH645CRDoy9M98eptAoNLdJBHYz23U5IbGL1+qHDDCPXxKs9ZG7EEqyWezq42eoFoebLA5O6/xrYXoaeIb094dbCF4D932hAkgAaAZkZVsSiWDCjYSV+JoWX4NVBcIL9yyHRhaaPVULTRbPsZQWq9+hMCgYEA48j4RGO7CaVpgUVobYasJnkGSdhkSCd1VwgvHH3vtuk7/JGUBRaZc0WZGcXkAJXnLh7QnDHOzWASdaxVgnuviaDi4CIkmTCfRqPesgDR2Iu35iQsH7P2/o1pzhpXQS/Ct6J7/GwJTqcXCvp4tfZDbFxS8oewzp4RstILj+pDyWECgYByQAbOy5xB8GGxrhjrOl1OI3V2c8EZFqA/NKy5y6/vlbgRpwbQnbNy7NYj+Y/mV80tFYqldEzQsiQrlei78Uu5YruGgZogL3ccj+izUPMgmP4f6+9XnSuN9rQ3jhy4k4zQP1BXRcim2YJSxhnGV+1hReLknTX2IwmrQxXfUW4xfQKBgAHZW8qSVK5bXWPjQFnDQhp92QM4cnfzegxe0KMWkp+VfRsrw1vXNx";
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateSecretKey));


            token = new JwtSecurityToken(
                issuer: "http://www.checkarr.com",
                audience: "http://www.checkarr.com",
                expires: DateTime.UtcNow.AddMinutes(5),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            
        }
        /*
        private static bool ValidateToken(string token, out string username)
        {
            // working on it 
            username = null;

            var simplePrinciple = GetPrincipal(token);
            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Email);
            username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
                return false;

            // More validate to check whether username exists in system

            return true;
        }*/
    }
}
