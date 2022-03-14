using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
   public class WishListBL:IWishListBL
    {
        IWishListRL wishlistRL;
        public WishListBL(IWishListRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }

        public string AddWishlist(WishListModel wishlist)
        {
            try
            {
                return this.wishlistRL.AddWishlist(wishlist);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool RemoveBookFromWishlist(int WishlistId)
        {
            try
            {
                if (wishlistRL.RemoveBookFromWishlist(WishlistId))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<WishList> GetWishlistbyuserId(int userId)
        {
            try
            {
                return this.wishlistRL.GetWishlistbyuserId(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

       /* public List<WishList> GetAllWislists()
        {
            try
            {
                return this.wishlistRL.GetAllWislists();
            }
            catch (Exception e)
            {
                throw e;
            }

        }*/
    }
}
