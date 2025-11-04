using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AgendaNet.Auth
{
    public static class JasonWebTokenMiddleware
    {
        //private static string Auth = "Amordaminhavidadaquiateaeternidade";
        public static void AddJwtMiddleware(this IServiceCollection services, IConfiguration configuration)
        {
            var authKey = configuration["AUTHENTICATION"];
            if (string.IsNullOrEmpty(authKey))
            {
                throw new InvalidOperationException("AUTHENTICATION não configurada.");
            }
            {
                var key = Encoding.ASCII.GetBytes(authKey);


                services.AddAuthorization(options =>
                {
                    options.AddPolicy("AdminPolicy", policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireRole("Admin");
                    });
                });

                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
               .AddJwtBearer(x =>
               {
                   x.RequireHttpsMetadata = true;
                   x.SaveToken = true;
                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });


            }
        }

    }
}
