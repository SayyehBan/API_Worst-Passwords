using API_Worst_Passwords.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API_Worst_Passwords.Controllers;
/// <summary>
/// پیسورد
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class WorstPasswordsController : ControllerBase
{
    /// <summary>
    /// ثبت نام
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Register([FromForm] RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        // ادامه عملیات ثبت‌نام
        return Ok("OK");
    }
}
