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
        [HttpPost("AddAdmin")]
        public IActionResult AddAdmin(AdminModel admin)
        {
            try
            {
                var result = this.adminBL.AddAdmin(admin);
                if (result.Equals("Admin Added successfully"))
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
        [HttpPost("login/{Email}/{Password}")]
        public IActionResult UserLogin(string Email, string Password)
        {
            try
            {
                var result = this.adminBL.Login(Email, Password);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Login Successful", token = result });
                }
                else
                {
                    return this.Ok(new { Success = false, message = "Invalid Admin email and password" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.Message });

            }
        }
        [HttpPost("AdminAddBook")]
        public IActionResult AdminAddBook(AdminBookModel admin)
        {
            try
            {
                var result = this.adminBL.AdminAddBook(admin);
                if (result.Equals("Admin Added Books"))
                {
                    return this.Ok(new { success = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { success = true, Message = $"Admin Book Added Unsuccessfull" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [HttpPut("AdminupdateBook/{BookId}/{AdminId}")]
        public IActionResult AdminUpdateBook(int BookId,int AdminId, AdminUpdateBook admin)
        {
            try
            {
                var result = this.adminBL.AdminUpdateBook(BookId,AdminId, admin);
                if (result.Equals(true))
                {
                    return this.Ok(new { Status = true, Message = "Admin Updated Book Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Admin Updated Book Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete("AdminDeleteBook/{BookId}")]
        public IActionResult AdminDeleteBook(int BookId)
        {
            try
            {
                var result = this.adminBL.AdminDeleteBook(BookId);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $" Admin Book deleted Successfully ", response = BookId });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpGet("AdminGetAllBooks")]
        public IActionResult GetAllAdmibyAdminId()
        {
            try
            {
               var result = User.FindFirst("Email").Value.ToString();
                if (result!= null)
                {
                   var data = this.adminBL.AdminGetAllBooks();
                    return this.Ok(new { Status = true, Message = "Retrieval the admin all books succssful", response = data});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Retrieval not successfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
