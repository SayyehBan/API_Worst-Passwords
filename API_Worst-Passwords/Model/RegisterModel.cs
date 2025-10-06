namespace API_Worst_Passwords.Model;

using SayehBanTools.Validations.Attributes;
/// <summary>
/// مدل ثبت نام
/// </summary>
public class RegisterModel
{
    [CommonPassword(
        filePath: "wwwroot/file/worst-passwords.txt",  // مسیر فایل نسبت به WebRootPath
        ErrorMessage = "رمز عبور ضعیف است، لطفاً تغییر دهید.")]
    public string Password { get; set; } = string.Empty;
}
