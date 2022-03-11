using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface ICartBL
    {
        string AddToCart(CartModel cartModel);

        bool UpdateCartQuantity(int CartId, int OrderQuantity);

        string DeleteCart(int CartId);

        List<CartModel> GetCartDetails(int userId);
    }
}
