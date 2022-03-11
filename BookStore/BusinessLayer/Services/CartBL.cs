using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
   public class CartBL:ICartBL
    {
        ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public string AddToCart(CartModel cartModel)
        {
            try
            {
                return this.cartRL.AddToCart(cartModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateCartQuantity(int CartId, int OrderQuantity)
        {
            try
            {
                if (cartRL.UpdateCartQuantity(CartId, OrderQuantity))
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public string DeleteCart(int CartId)
        {
            try
            {
                return this.cartRL.DeleteCart(CartId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<CartModel> GetCartDetails(int userId)
        {
            try
            {
                return this.cartRL.GetCartDetails(userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
