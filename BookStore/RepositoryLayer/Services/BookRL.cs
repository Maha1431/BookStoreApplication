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
    public class BookRL : IBookRL
    {
        private SqlConnection connection;
        public IConfiguration Configuration { get; }
        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddBook(BookModel book)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_AddBook", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookName", book.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                    cmd.Parameters.AddWithValue("@DiscountPrice", book.DiscountPrice);
                    cmd.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice);
                    cmd.Parameters.AddWithValue("@BookDescription", book.BookDescription);
                    cmd.Parameters.AddWithValue("@Rating", book.Rating);
                    cmd.Parameters.AddWithValue("@Reviewer", book.Reviewer);
                    cmd.Parameters.AddWithValue("Image", book.Image);
                    cmd.Parameters.AddWithValue("@BookCount", book.BookCount);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return "Book Added succssfully";

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

        public bool UpdateBook(int BookId, BookModel book)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_BookUpdate", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    cmd.Parameters.AddWithValue("@BookName", book.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                    cmd.Parameters.AddWithValue("@DiscountPrice", book.DiscountPrice);
                    cmd.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice);
                    cmd.Parameters.AddWithValue("@BookDescription", book.BookDescription);
                    cmd.Parameters.AddWithValue("@Rating", book.Rating);
                    cmd.Parameters.AddWithValue("@Reviewer", book.Reviewer);
                    cmd.Parameters.AddWithValue("Image", book.Image);
                    cmd.Parameters.AddWithValue("@BookCount", book.BookCount);
                    connection.Open();
                    // cmd.ExecuteNonQuery();
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
        public bool DeleteBook(int BookId)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteBook", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
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

        public List<Book> GetAllBooks()
        {
           connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    List<Book> book = new List<Book>();
                    SqlCommand cmd = new SqlCommand("sp_GetAllBooks", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Book books = new Book();
                            books.BookId = Convert.ToInt32(dr["BookId"]);
                            books.BookName = dr["BookName"].ToString();
                            books.AuthorName = dr["AuthorName"].ToString();
                            books.DiscountPrice = Convert.ToInt32(dr["DiscountPrice"]);
                            books.OriginalPrice = Convert.ToInt32(dr["OriginalPrice"]);
                            books.BookDescription = dr["BookDescription"].ToString();
                            books.Rating = Convert.ToInt32(dr["Rating"]);
                            books.Reviewer = Convert.ToInt32(dr["Reviewer"]);
                            books.Image = dr["Image"].ToString();
                            books.BookCount = Convert.ToInt32(dr["BookCount"]);
                            book.Add(books);
                        }
                        return book;
                    }
                    else
                    {
                        return null;
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

        public List<BookModel> GetAllBooksbyBookId(int BookId)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {

                    List<BookModel> book = new List<BookModel>();
                    SqlCommand cmd = new SqlCommand("sp_GetAllBookByBookId", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            BookModel model = new BookModel();
                            model.BookName = dr["BookName"].ToString();
                            model.AuthorName = dr["AuthorName"].ToString();
                            model.DiscountPrice = Convert.ToInt32(dr["DiscountPrice"]);
                            model.OriginalPrice = Convert.ToInt32(dr["OriginalPrice"]);
                            model.BookDescription = dr["BookDescription"].ToString();
                            model.Rating = Convert.ToInt32(dr["Rating"]);
                            model.Reviewer = Convert.ToInt32(dr["Reviewer"]);
                            model.Image = dr["Image"].ToString();
                            model.BookCount = Convert.ToInt32(dr["BookCount"]);
                            book.Add(model);
                        }
                        return book;
                    }
                    else
                    {
                        return null;
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
    }
}
