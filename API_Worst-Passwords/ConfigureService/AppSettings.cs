namespace API_Worst_Passwords.ConfigureService;
/// <summary>
/// مدل پیکر بندی تنظیمات احراز هویت
/// </summary>
public class AppSettings
{
    /// <summary>
    /// اهراز اطلاعات
    /// </summary>
    public string? JwtIssuer { get; set; }
    /// <summary>
    /// آدرس
    /// </summary>
    public string? JwtAudience { get; set; }
    /// <summary>
    /// کلید
    /// </summary>
    public string? JwtKey { get; set; }
    /// <summary>
    /// تنظیمات
    /// </summary>
    public string? SomeOtherSetting { get; set; }
}