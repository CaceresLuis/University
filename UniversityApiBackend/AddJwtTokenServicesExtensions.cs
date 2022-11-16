using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend
{
    public static class AddJwtTokenServicesExtensions
    {
        public static void AddJwtTokenServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            //Add JWT Settings
            var bindJwtSettings = new JwtSettings();
            Configuration.Bind("JsonWebTokenKey", bindJwtSettings);

            //Add singleton of JWT Settings
            Services.AddSingleton(bindJwtSettings);

            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata= false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigninKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigninKey)),
                    ValidateIssuer= bindJwtSettings.ValidateIssuer,
                    ValidIssuer = bindJwtSettings.ValidIsuer,
                    ValidateAudience = bindJwtSettings.ValidateAudience,
                    ValidAudience= bindJwtSettings.ValidAudience,
                    RequireExpirationTime= bindJwtSettings.RequireExpirationTime,
                    ValidateLifetime = bindJwtSettings.ValidateLifeTime,
                    ClockSkew = TimeSpan.FromDays(1)
                };
            });
        }
    }
}
