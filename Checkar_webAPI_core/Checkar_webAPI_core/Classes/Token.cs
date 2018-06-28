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
        string privateSecretKey = "OfED+KgbZxtu4e4+JSQWdtSgTnuNixKy1nMVAEww8QL3IN3idcNgbNDSSaV4491Fo3sq2aGSCtYvekzs7JwXJnNAyvDSJjfK/7M8MpxSMnm1vMscBXyiYFXhGC4wqWlYBE828/5DNyw3QZW5EjD7hvDrY5OlYd4smCTa53helNnJz5NT9HQaDbE2sMwIDAQABAoIBAEs63TvT94njrPDP3A/sfCEXg1F2y0D/PjzUhM1aJGcRiOUXnGlYdViGhLnnJoNZTZm9qI1LT0NWcDA5NmBN6gcrk2EApyTt1D1i4AQ66rYoTF9iEC4Wye28v245BYESA6IIelgIxXGsVyllERsbTkaphzibbYfHmvwMxkn135Zfzd/NOXl/O32vYIomzrNEP+tN2WXhhG8c8+iZ8PErBV3CqrYogYy97d2CeQbXcpd5unPiU4TK0nnzeBAXdgeYuJHFC45YHl9UvShRoe6CHR47ceIGp6WMc5BTyyTkZpctuYJTwaChdj/QuRSkTYmn6jFL+MRfYQJ8VVwSVo5DbkECgYEA4/YIMKcwObYcSuHzgkMwH645CRDoy9M98eptAoNLdJBHYz23U5IbGL1+qHDDCPXxKs9ZG7EEqyWezq42eoFoebLA5O6/xrYXoaeIb094dbCF4D932hAkgAaAZkZVsSiWDCjYSV+JoWX4NVBcIL9yyHRhaaPVULTRbPsZQWq9+hMCgYEA48j4RGO7CaVpgUVobYasJnkGSdhkSCd1VwgvHH3vtuk7/JGUBRaZc0WZGcXkAJXnLh7QnDHOzWASdaxVgnuviaDi4CIkmTCfRqPesgDR2Iu35iQsH7P2/o1pzhpXQS/Ct6J7/GwJTqcXCvp4tfZDbFxS8oewzp4RstILj+pDyWECgYByQAbOy5xB8GGxrhjrOl1OI3V2c8EZFqA/NKy5y6/vlbgRpwbQnbNy7NYj+Y/mV80tFYqldEzQsiQrlei78Uu5YruGgZogL3ccj+izUPMgmP4f6+9XnSuN9rQ3jhy4k4zQP1BXRcim2YJSxhnGV+1hReLknTX2IwmrQxXfUW4xfQKBgAHZW8qSVK5bXWPjQFnDQhp92QM4cnfzegxe0KMWkp+VfRsrw1vXNx";


        public void GenerateToken(String email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateSecretKey));


            token = new JwtSecurityToken(
                issuer: "http://www.checkarr.com",
                audience: "http://www.checkarr.com",
                expires: DateTime.UtcNow.AddMinutes(2),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            
        }
        
        public bool ValidateToken(string incomingToken, String username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateSecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Exception> validationFailures = null;
            SecurityToken validatedToken;
            var validator = new JwtSecurityTokenHandler();

            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidIssuer = "http://www.checkarr.com";
            validationParameters.ValidAudience = "http://www.checkarr.com";
            validationParameters.IssuerSigningKey = key;
            validationParameters.ValidateIssuerSigningKey = true;
            validationParameters.ValidateAudience = true;

            if (validator.CanReadToken(incomingToken))
            {
                ClaimsPrincipal principal;
                try
                {
                    principal = validator.ValidateToken(incomingToken, validationParameters, out validatedToken);
                    
                    if (principal.HasClaim(c => c.Type == ClaimTypes.Email))
                    {
                        String email = principal.Claims.Where(c => c.Type == ClaimTypes.Email).First().Value;
                        // System.Diagnostics.Debug.WriteLine("++++ " + email);
                        Boolean dateV;
                        //System.Diagnostics.Debug.WriteLine("VALID TO TIME " + validatedToken.ValidTo);
                        //System.Diagnostics.Debug.WriteLine("SYSTEM TIME " + DateTime.UtcNow);

                        if (validatedToken.ValidTo >= DateTime.UtcNow) dateV = true;
                        else dateV = false;
                        //System.Diagnostics.Debug.WriteLine("VALID TO " + validatedToken.ValidTo.ToLocalTime());
                        //System.Diagnostics.Debug.WriteLine("VALID: " + dateV);
                        if (email.Equals(username) && dateV) return true;
                        else return false;
                        
                        
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);

                }
            }

            return false;
        }
    }
}
