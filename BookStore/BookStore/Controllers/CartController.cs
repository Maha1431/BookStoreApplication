using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CartController : Controller
    {
        ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }
        [HttpPost]
        [Route("addToCarts")]
        public IActionResult AddToCart(CartModel cartModel)
        {
            try
            {
                var result = this.cartBL.AddToCart(cartModel);
                if (result.Equals("Book Added succssfully to Cart"))
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
        [HttpPut("updatebook/{CartId}/{OrderQuantity}")]
        public IActionResult UpdateBook(int CartId, int OrderQuantity)
        {
            try
            {
                var result = this.cartBL.UpdateCartQuantity(CartId, OrderQuantity);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"cart updated Successfully ", response = OrderQuantity });
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
        [HttpDelete("deleteBook/{CartId}")]
        public IActionResult DeleteCart(int CartId)
        {
            try
            {
                var result = this.cartBL.DeleteCart(CartId);
                if (result.Equals("Cart details deleted successfully"))
                {
                    return this.Ok(new  { Status = true, Message = result });
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
        
        [HttpGet("getCartDetails/{userId}")]
        public IActionResult GetCartDetails(int userId)
        {
            try
            {
                var result = this.cartBL.GetCartDetails(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Data retrieved successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Get cart details is unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
