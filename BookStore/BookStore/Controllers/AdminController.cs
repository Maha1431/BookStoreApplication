using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AdminController : ControllerBase
    {

        IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }
        [HttpPost("addAdmin")]
        public IActionResult AddAdmin(AdminModel admin)
        {
            try
            {
                var result = this.adminBL.AddAdmin(admin);
                if (result.Equals("Admin Added succssfully"))
                {
                    return this.Ok(new { success = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { success = true, Message = $"Admin Added Unsuccessfull" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPut("updateAdmin/{AdminId}")]
        public IActionResult UpdateAdmin(int AdminId, AdminModel admin)
        {
            try
            {
                var result = this.adminBL.UpdateAdmin(AdminId, admin);
                if (result.Equals(true))
                {
                    return this.Ok(new { Status = true, Message = "Admin Updated Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Admin Updated Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("getAllAdminbyAdminId/{AdminId}")]
        public IActionResult GetAllAdmibyAdminId(int AdminId)
        {
            try
            {
               /* int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                var result1 = Convert.ToInt32(User.FindFirst("userId").Value);
               */
               var result = User.FindFirst("Email").Value.ToString();
                if (result!= null)
                {
                   var data = this.adminBL.GetAllAdminByAdminId(AdminId);
                    return this.Ok(new { Status = true, Message = "Retrieval all admin succssful", response = data});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "AdminId not Exist" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
