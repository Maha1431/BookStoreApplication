using CommonLayer;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
   public class CartRL:ICartRL
    {
        private SqlConnection connection;
       
        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string AddToCart(CartModel cartModel)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using(connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_AddCart", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", cartModel.BookId);
                    cmd.Parameters.AddWithValue("@UserId", cartModel.userId);
                    cmd.Parameters.AddWithValue("@OrderQuantity", cartModel.OrderQuantity);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return "Book Added succssfully to Cart";
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool UpdateCartQuantity(int CartId, int OrderQuantity)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using(connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateCart", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", CartId);
                    cmd.Parameters.AddWithValue("@OrderQuantity", OrderQuantity);
                    connection.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result != 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }
        public string DeleteCart(int CartId)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using(connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteCartDetails", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", CartId);
                    connection.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result == 1)
                    {
                        return "CartId does not exists";
                    }
                    else
                    {
                        return "Cart details deleted successfully";
                    }
                }
            
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<CartModel> GetCartDetails(int userId)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                SqlCommand cmd = new SqlCommand("sp_GetCartdetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<CartModel> cart = new List<CartModel>();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CartModel cartModel = new CartModel();
                        BookModel bookModel = new BookModel();
                        bookModel.BookName = dr["BookName"].ToString();
                        bookModel.AuthorName = dr["AuthorName"].ToString();
                        bookModel.DiscountPrice = Convert.ToInt32(dr["DiscountPrice"]);
                        bookModel.OriginalPrice = Convert.ToInt32(dr["OriginalPrice"]);
                        bookModel.BookDescription = dr["BookDescription"].ToString();
                        bookModel.Rating = Convert.ToInt32(dr["Rating"]);
                        bookModel.Reviewer = Convert.ToInt32(dr["Reviewer"]);
                        bookModel.Image = dr["Image"].ToString();
                        bookModel.BookCount = Convert.ToInt32(dr["BookCount"]);
                        cartModel.userId = Convert.ToInt32(dr["userId"]);
                        cartModel.BookId = Convert.ToInt32(dr["BookId"]);
                        cartModel.OrderQuantity = Convert.ToInt32(dr["OrderQuantity"]);
                        cartModel.bookModel = bookModel;
                        cart.Add(cartModel);
                    }
                    return cart;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
