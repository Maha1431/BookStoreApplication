using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
   public class OrdersRL:IOrdersRL
    {
        private SqlConnection connection;
        public IConfiguration Configuration { get; }
        public OrdersRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddOrder(OrderModel order)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_AddingOrders", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@userId", order.userId);
                    cmd.Parameters.AddWithValue("@AddressId", order.AddressId);
                    cmd.Parameters.AddWithValue("@BookId", order.BookId);
                    cmd.Parameters.AddWithValue("TotalPrice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@BookQuantity", order.BookQuantity);

                    connection.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result == 1)
                    {
                        return "BookId not exists";
                    }
                    else if (result == 1)
                    {
                        return "userid not exists";
                    }
                    else
                    {
                        return "Books Ordered successfully";
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public List<Order> GetOrderDetails(int userId)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_GetAllOrdersbyuserId", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@userId", userId);
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Order> order = new List<Order>();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Order orderModel = new Order();
                            BookModel bookModel = new BookModel();
                            bookModel.BookName = dr["BookName"].ToString();
                            bookModel.AuthorName = dr["AuthorName"].ToString();
                            bookModel.DiscountPrice = Convert.ToInt32(dr["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(dr["OriginalPrice"]);
                            bookModel.BookDescription = dr["BookDescription"].ToString();
                            bookModel.Rating = Convert.ToInt32(dr["Rating"]);
                            bookModel.Reviewer = Convert.ToInt32(dr["Reviewer"]);
                            bookModel.Image = dr["Image"].ToString();
                            orderModel.OrderId = Convert.ToInt32(dr["OrderId"]);
                            orderModel.userId = Convert.ToInt32(dr["userId"]);
                            orderModel.AddressId = Convert.ToInt32(dr["AddressId"]);
                            orderModel.BookId = Convert.ToInt32(dr["BookId"]);
                            orderModel.TotalPrice = Convert.ToInt32(dr["TotalPrice"]);
                            orderModel.BookQuantity = Convert.ToInt32(dr["BookQuantity"]);
                            orderModel.OrderDate = dr["OrderDate"].ToString();
                            orderModel.bookModel = bookModel;
                            order.Add(orderModel);
                        }
                        return order;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
