using CommonLayer;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface ICartRL
    {
        string AddToCart(CartModel cartModel);

        bool UpdateCartQuantity(int CartId, int OrderQuantity);

        string DeleteCart(int CartId);

        List<CartModel> GetCartDetails(int userId);

       
    }
}
