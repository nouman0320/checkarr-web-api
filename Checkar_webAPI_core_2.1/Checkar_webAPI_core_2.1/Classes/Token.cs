using Microsoft.Extensions.Configuration;
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
        
        string privateSecretKey = "";

        public Token(string key)
        {
            privateSecretKey = key;
           
        }


        // function to validate activation token
        public Boolean ValidateActivationToken(String activationToken, int user_id)
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

            if (validator.CanReadToken(activationToken))
            {
                ClaimsPrincipal principal;
                try
                {
                    principal = validator.ValidateToken(activationToken, validationParameters, out validatedToken);

                    if (principal.HasClaim(c => c.Type == JwtRegisteredClaimNames.Sid))
                    {
                        String extractedUserID = principal.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sid).First().Value;
                        Boolean dateV;
                        if (validatedToken.ValidTo >= DateTime.UtcNow) dateV = true;
                        else dateV = false;
                        if (extractedUserID.Equals(user_id.ToString()) && dateV) return true;
                        else return false;


                    }
                }
                catch (Exception e)
                {
                    //validationFailures.Add(e);
                    System.Diagnostics.Debug.WriteLine(e);
                    return false;

                }
            }

            return false;
        }


        public bool ValidateRecoveryToken(string recoveryToken, String recoveryEmail)
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

            if (validator.CanReadToken(recoveryToken))
            {
                ClaimsPrincipal principal;
                try
                {
                    principal = validator.ValidateToken(recoveryToken, validationParameters, out validatedToken);

                    if (principal.HasClaim(c => c.Type == ClaimTypes.Email))
                    {
                        String extractedRecoveryEmail = principal.Claims.Where(c => c.Type == ClaimTypes.Email).First().Value;
                        Boolean dateV;
                        if (validatedToken.ValidTo >= DateTime.UtcNow) dateV = true;
                        else dateV = false;
                        if (extractedRecoveryEmail.Equals(recoveryEmail) && dateV) return true;
                        else return false;
                    }
                }
                catch (Exception e)
                {
                    //validationFailures.Add(e);
                    System.Diagnostics.Debug.WriteLine(e);

                }
            }

            return false;
        }


        public bool ValidateResetToken(string resetToken, String resetEmail)
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

            if (validator.CanReadToken(resetToken))
            {
                ClaimsPrincipal principal;
                try
                {
                    principal = validator.ValidateToken(resetToken, validationParameters, out validatedToken);

                    if (principal.HasClaim(c => c.Type == ClaimTypes.Email))
                    {
                        String extractedRecoveryEmail = principal.Claims.Where(c => c.Type == ClaimTypes.Email).First().Value;
                        Boolean dateV;
                        if (validatedToken.ValidTo >= DateTime.UtcNow) dateV = true;
                        else dateV = false;
                        if (extractedRecoveryEmail.Equals(resetEmail) && dateV) return true;
                        else return false;
                    }
                }
                catch (Exception e)
                {
                    //validationFailures.Add(e);
                    System.Diagnostics.Debug.WriteLine(e);

                }
            }

            return false;
        }


        public JwtSecurityToken GenerateResetToken(String recoveryEmail)
        {
            // use this fuction to generate recovery token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, recoveryEmail),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateSecretKey));


            return new JwtSecurityToken(
                issuer: "http://www.checkarr.com",
                audience: "http://www.checkarr.com",
                expires: DateTime.UtcNow.AddDays(1),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
        }


        public JwtSecurityToken GenerateRecoveryToken(String recoveryEmail)
        {
            // use this fuction to generate recovery token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, recoveryEmail),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateSecretKey));


            return new JwtSecurityToken(
                issuer: "http://www.checkarr.com",
                audience: "http://www.checkarr.com",
                expires: DateTime.UtcNow.AddDays(1),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
        }

        public JwtSecurityToken GenerateActivationToken(int user_id)
        {
            // generate activation token for registration
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sid, user_id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateSecretKey));


            return new JwtSecurityToken(
                issuer: "http://www.checkarr.com",
                audience: "http://www.checkarr.com",
                expires: DateTime.UtcNow.AddDays(1),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

        }



        public JwtSecurityToken GenerateToken(int user_id)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sid, user_id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateSecretKey));


            return new JwtSecurityToken(
                issuer: "http://www.checkarr.com",
                audience: "http://www.checkarr.com",
                expires: DateTime.UtcNow.AddMinutes(60),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );


        }


        public JwtSecurityToken GenerateRefreshToken(int user_id)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sid, user_id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateSecretKey));


            return new JwtSecurityToken(
                issuer: "http://www.checkarr.com",
                audience: "http://www.checkarr.com",
                expires: DateTime.UtcNow.AddMonths(6),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );


        }


        public bool ValidateToken(string incomingToken, int user_id)
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

                    if (principal.HasClaim(c => c.Type == JwtRegisteredClaimNames.Sid))
                    {
                        String _user_id = principal.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sid).First().Value;
                        // System.Diagnostics.Debug.WriteLine("++++ " + email);
                        Boolean dateV;
                        System.Diagnostics.Debug.WriteLine("VALID TO TIME " + validatedToken.ValidTo);
                        System.Diagnostics.Debug.WriteLine("SYSTEM TIME " + DateTime.UtcNow);

                        if (validatedToken.ValidTo >= DateTime.UtcNow) dateV = true;
                        else dateV = false;
                        System.Diagnostics.Debug.WriteLine("VALID TO " + validatedToken.ValidTo.ToLocalTime());
                        System.Diagnostics.Debug.WriteLine("VALID: " + dateV);
                        if (_user_id.Equals(user_id.ToString()) && dateV) return true;
                        else return false;


                    }
                }
                catch (Exception e)
                {
                    //validationFailures.Add(e);
                    System.Diagnostics.Debug.WriteLine(e);

                }
            }

            return false;
        }

        public bool ValidateRefreshToken(string incomingToken, int user_id)
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

                    if (principal.HasClaim(c => c.Type == JwtRegisteredClaimNames.Sid))
                    {
                        String _user_id = principal.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sid).First().Value;
                        // System.Diagnostics.Debug.WriteLine("++++ " + email);
                        Boolean dateV;
                        //System.Diagnostics.Debug.WriteLine("VALID TO TIME " + validatedToken.ValidTo);
                        //System.Diagnostics.Debug.WriteLine("SYSTEM TIME " + DateTime.UtcNow);

                        if (validatedToken.ValidTo >= DateTime.UtcNow) dateV = true;
                        else dateV = false;
                        //System.Diagnostics.Debug.WriteLine("VALID TO " + validatedToken.ValidTo.ToLocalTime());
                        //System.Diagnostics.Debug.WriteLine("VALID: " + dateV);
                        if (_user_id.Equals(user_id.ToString()) && dateV) return true;
                        else return false;


                    }
                }
                catch (Exception e)
                {
                    //validationFailures.Add(e);
                    System.Diagnostics.Debug.WriteLine(e);

                }
            }

            return false;
        }
    }
}
