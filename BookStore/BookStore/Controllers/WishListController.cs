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
    public class WishListController : ControllerBase
    {
        IWishListBL wishlistBL;

        public WishListController(IWishListBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }

        [HttpPost("addToWishlist")]
        public IActionResult AddWishlist( WishListModel wishlist)
        {
            try
            {
                string result = this.wishlistBL.AddWishlist(wishlist);
                if (result.Equals("Book Wishlisted successfully"))
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
        [HttpDelete("deleteWishlist/{WishlistId}")]
        public IActionResult RemoveBookFromWishlist(int WishlistId)
        {
            try
            {
                var result = this.wishlistBL.RemoveBookFromWishlist(WishlistId);
                if (result.Equals(true))
                {

                    return this.Ok(new { Status = true, Message = "Wishlist deleted successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "WishList deleted unSuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet("getWishlistDetails/{userId}")]
        public IActionResult RetrieveWishlist(int userId)
        {
            try
            {
                var result = this.wishlistBL.GetWishlistbyuserId(userId);
                if (result != null)
                {

                    return this.Ok(new { Status = true, Message = "Retrieve wishlist successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Retrieval unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
       /* [HttpGet("getAllWishlists")]
        public IActionResult GetAllWishlists()
        {
            try
            {
                var result = this.wishlistBL.GetAllWislists();
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrieval all wishlist succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Retrieval is unsucessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }*/
    }
}
