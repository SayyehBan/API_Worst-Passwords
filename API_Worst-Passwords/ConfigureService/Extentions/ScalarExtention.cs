using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
namespace API_Worst_Passwords.ConfigureService.Extentions;
/// <summary>
/// کلاس افزودنی برای پیکربندی Scalar
/// </summary>
public static class ScalarExtention
{
    /// <summary>
    /// سرویس Scalar
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddScalarService(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((doc, context, cancellationToken) =>
            {
                doc.Info.Title = "سایه بان";
                doc.Info.Version = "v1";
                doc.Info.Description = "مستندات API برای برنامه";

                // تنظیم نسخه OpenAPI
                if (!doc.Extensions.ContainsKey("openapi"))
                {
                    doc.Extensions.Add("openapi", new OpenApiString("3.1.0"));
                }
                else
                {
                    doc.Extensions["openapi"] = new OpenApiString("3.1.0");
                }

                // افزودن طرح امنیتی Bearer JWT
                doc.Components ??= new OpenApiComponents();
                doc.Components.SecuritySchemes ??= new Dictionary<string, OpenApiSecurityScheme>();
                doc.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "هدر احراز هویت JWT با استفاده از طرح Bearer. توکن خود را در زیر وارد کنید (بدون 'Bearer')."
                };

                doc.SecurityRequirements ??= new List<OpenApiSecurityRequirement>();
                doc.SecurityRequirements.Add(new OpenApiSecurityRequirement
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

                return Task.CompletedTask;
            });
        });
        return services;
    }
}