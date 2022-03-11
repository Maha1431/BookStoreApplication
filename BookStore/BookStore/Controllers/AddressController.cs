using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AddressController : Controller
    {
        IAddressBL addressBL;

        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [HttpPost("addAddress/{userId}")]

        public IActionResult AddAddress(int userId, AddressModel address)
        {
            try
            {
                var result = this.addressBL.AddAddress(userId,address);

                if (result.Equals("Address Added successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Address Added Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut("updateAddress/{AddressId}")]
        public IActionResult UpdateAddress(int AddressId, AddressModel address)
        {
            try
            {
                var result = this.addressBL.UpdateAddress(AddressId,address);
                if (result.Equals(true))
                {
                    return this.Ok(new { Status = true, Message = "Address Updated Successfully"});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Address Updated Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete("deletebook/{AddressId}")]
        public IActionResult DeleteAddress(int AddressId)
        {
            try
            {
                var result = this.addressBL.DeleteAddress(AddressId);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Address deleted Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Address Deleted UnSuccessfull"});
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpGet]
        [Route("getAllAddress")]
        public IActionResult GetAllAddresses()
        {
            try
            {
                var result = this.addressBL.GetAllAddress();
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrieval all addresses succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new  { Status = false, Message = "Retrieval is unsucessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new  { Status = false, Message = ex.Message });
            }
        }
        [HttpGet("getAddressbyuserId/{userId}")]
        public IActionResult GetAddressesbyuserId(int userId)
        {
            try
            {
                var result = this.addressBL.GetAllAddressbyuserId(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrieval all addresses succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "userId not Exist" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
