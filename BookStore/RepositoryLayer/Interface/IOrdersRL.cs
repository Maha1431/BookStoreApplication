using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IOrdersRL
    {
        string AddOrder(OrderModel order);

        List<Order> GetOrderDetails(int userId);
    }
}
