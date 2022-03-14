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
    public class OrderController : ControllerBase
    {
        IOrdersBL orderBL;

        public OrderController(IOrdersBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [HttpPost("addOrders")]
        public IActionResult AddOrder(OrderModel order)
        {
            try
            {
                var result = this.orderBL.AddOrder(order);
                if (result.Equals("Books Ordered successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet("GetOrdersDetails/{userId}")]
        public IActionResult RetrieveOrderDetails(int userId)
        {
            try
            {
                var result = this.orderBL.GetOrderDetails(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Get order details successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Get order details unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
