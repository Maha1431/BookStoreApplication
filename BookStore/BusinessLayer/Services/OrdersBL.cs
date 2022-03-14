using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
   public class OrdersBL:IOrdersBL
    {
        IOrdersRL orderRL;
        public OrdersBL(IOrdersRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public string AddOrder(OrderModel order)
        {
            try
            {
                return this.orderRL.AddOrder(order);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Order> GetOrderDetails(int userId)
        {
            try
            {
                return this.orderRL.GetOrderDetails(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
