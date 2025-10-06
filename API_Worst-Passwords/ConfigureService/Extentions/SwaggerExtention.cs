using Microsoft.OpenApi.Models;
namespace API_Worst_Passwords.ConfigureService.Extentions;
/// <summary>
/// // اتصال فایل XML به مستندات OpenAPI و افزودن احراز هویت Bearer برای Swagger UI
/// </summary>
public static class SwaggerExtention
{
    /// <summary>
    /// افزودن Swagger به خدمات
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "سایه بان",
                Version = "v1",
                Description = "مستندات API برای برنامه با احراز هویت JWT",
                License = new OpenApiLicense
                {
                    Name = "MIT"
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "احراز هویت JWT با استفاده از طرح Bearer. توکن خود را در زیر وارد کنید (بدون 'Bearer')."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new List<string>()
                }
            });

            var xmlFile = "worst-passwords.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"Warning: XML documentation file not found at: {xmlPath}");
            }
            options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        });

        return services;
    }
}