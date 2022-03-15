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
    public class WishListRL : IWishListRL
    {
        private SqlConnection connection;
        public IConfiguration Configuration { get; }
        public WishListRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddWishlist(WishListModel wishlist)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {

                    SqlCommand cmd = new SqlCommand("sp_AddWishlist", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@userId", wishlist.userId);
                    cmd.Parameters.AddWithValue("@BookId", wishlist.BookId);

                    connection.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result != 1)
                    {
                        return "Book Wishlisted successfully";
                    }
                    else
                    {
                        return "BookId not exists";
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

        public bool RemoveBookFromWishlist(int WishlistId)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_RemoveWishlist", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@WishlistId", WishlistId);
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
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

        }

        public List<WishList> GetWishlistbyuserId(int userId)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    List<WishList> wishlist = new List<WishList>();
                    SqlCommand cmd = new SqlCommand("sp_GetWishlistbyuserId", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", userId);
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            WishList wish = new WishList();
                            BookModel bookModel = new BookModel();
                            bookModel.BookName = dr["BookName"].ToString();
                            bookModel.AuthorName = dr["AuthorName"].ToString();
                            bookModel.DiscountPrice = Convert.ToInt32(dr["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(dr["OriginalPrice"]);
                            bookModel.Rating = Convert.ToInt32(dr["Rating"]);
                            bookModel.Reviewer = Convert.ToInt32(dr["Reviewer"]);
                            bookModel.Image = dr["Image"].ToString();
                            bookModel.BookCount = Convert.ToInt32(dr["BookCount"]);
                            wish.WishlistId = Convert.ToInt32(dr["WishlistId"]);
                            wish.userId = Convert.ToInt32(dr["userId"]);
                            wish.BookId = Convert.ToInt32(dr["BookId"]);
                            wish.Book = bookModel;
                            wishlist.Add(wish);
                        }
                        return wishlist;
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

    



