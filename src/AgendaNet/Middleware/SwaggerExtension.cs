using Microsoft.OpenApi.Models;

namespace AgendaNet.Models
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerMiddleware(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API - AgendaNet",
                    Version = "v1",
                    Description = ".",
                    //TermsOfService = new Uri("https://google.com/"),
                    Contact = new OpenApiContact
                    {
                        Name = "AGendaNet",
                        Email = string.Empty,

                        Url = new Uri("https://google.com/")
                    },
                });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

        }
    }
}