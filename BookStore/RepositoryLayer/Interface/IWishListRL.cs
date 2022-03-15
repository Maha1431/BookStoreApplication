using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IWishListRL
    {
        string AddWishlist(WishListModel wishlist);

        bool RemoveBookFromWishlist(int WishlistId);

        List<WishList> GetWishlistbyuserId(int userId);

    }
}
