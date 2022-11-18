using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Helpers
{
    public static class JwHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            List<Claim> claims = new()
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MM ddd dd yyyy HH:mm:ss tt"))
            };

            if (userAccounts.Role == "Administrator")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }
            else if (userAccounts.Role == "User")
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", $"User: {userAccounts.UserName}"));
            }
            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }

        public static UserTokens GetTokenKey(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                UserTokens userToken = new ();
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }

                //Obtain SECRET KEY
                byte[] key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigninKey);

                Guid Id;

                //Expires in 1 day
                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                //Validity of our token
                userToken.Validity = expireTime.TimeOfDay;

                //GENERATE OUR JWT
                JwtSecurityToken jwToken = new (
                            issuer: jwtSettings.ValidIsuer,
                            audience: jwtSettings.ValidAudience,
                            claims: GetClaims(model, out Id),
                            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                            expires: new DateTimeOffset(expireTime).DateTime,
                            signingCredentials: new SigningCredentials(
                                        new SymmetricSecurityKey(key),
                                        SecurityAlgorithms.HmacSha256
                                    )
                        );

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.UserName = model.UserName;
                userToken.Id= model.Id;
                userToken.GuidId= Id;
                userToken.EmailId = model.EmailId;
                userToken.Role =model.Role;

                return userToken;

            }
            catch (Exception ex)
            {
                throw new Exception("Error generating the JWT: ", ex);
            }
        }
    }
}
