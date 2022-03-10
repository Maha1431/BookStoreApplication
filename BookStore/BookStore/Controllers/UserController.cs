using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                await this.userBL.RegisterUser(userPostModel);
                return this.Ok(new { success = true, Message = $"Registration is successfull{userPostModel.Email}" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }
        [HttpPost("login/{Email}/{Password}")]
        public IActionResult UserLogin(string Email, string Password)
        {
            try
            {
                var result = this.userBL.Login(Email, Password);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Login Successful", token = result });
                }
                else
                {
                    return this.Ok(new { Success = false, message = "Invalid User email and password"});
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.Message });

            }
        }
        [HttpPost("forgotPassword/{Email}")]
        public IActionResult ForgetPassword(string Email)
        {
             try
            {
                if (this.userBL.ForgetPassword(Email))
                {
                    return Ok(new { Success = true, message = "Password reset link sent on mail successfully" });
                }
                else
                {
                    return Ok(new { Success = false, message = "Password reset link not sent" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message, msg = ex.InnerException });
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword( ResetPassword password)
        {
            try
            {
                var userEmail = User.FindFirst("Email").Value.ToString();
                if (this.userBL.ResetPassword(userEmail,password))
                {
                    return Ok(new { Success = true, message = "Password reset successfully" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Password reset Unsuccesfull!" });
                }
            
            }

            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }
    }
}
