using API_Worst_Passwords.ConfigureService;
using API_Worst_Passwords.ConfigureService.Extentions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
// تنظیم ApiBehaviorOptions
builder.Services.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
{
    apiBehaviorOptions.SuppressModelStateInvalidFilter = true;
});

// تنظیم RequestLocalizationOptions
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US") };
    options.RequestCultureProviders = new List<IRequestCultureProvider>();
});

// امکان آپلود فایل با حجم بالا
builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.MultipartHeadersLengthLimit = int.MaxValue;
});
// گرفتن تنظیمات از appsettings.json
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();
// افزودن احراز هویت JWT
builder.Services.AddAuthenticationJWT(appSettings!);
// Add services to the container.

builder.Services.AddControllers();
// سرویس‌های API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService();
builder.Services.AddScalarService();
builder.Services.AddHttpClient();
builder.Services.AddResponseCaching();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// پشته میدلورها
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCaching();

app.UseSwagger(c =>
{
    c.OpenApiVersion = OpenApiSpecVersion.OpenApi2_0;
});
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "سایه بان v1");
    options.DocumentTitle = "سایه بان";
    options.DocExpansion(DocExpansion.None);
    options.RoutePrefix = "swagger";
    options.DisplayRequestDuration();
});

app.MapScalarApiReference(options =>
{
    options.WithTitle("سایه بان")
           .WithTheme(ScalarTheme.Purple)
           .WithSidebar(true);
});

app.MapControllers();

app.Run();