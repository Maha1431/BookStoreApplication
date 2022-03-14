using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface IOrdersBL
    {
        string AddOrder(OrderModel order);

        List<Order> GetOrderDetails(int userId);
    }
}
