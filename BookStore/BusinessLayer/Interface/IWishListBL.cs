using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishListBL
    {
        string AddWishlist(WishListModel wishlist);

        bool RemoveBookFromWishlist(int WishlistId);

        List<WishList> GetWishlistbyuserId(int userId);
    }
}

        