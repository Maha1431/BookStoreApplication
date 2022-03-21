using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        private SqlConnection connection;

        public AdminRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string AddAdmin(AdminModel admin)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_AddAdmin", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                  //  cmd.Parameters.AddWithValue("@BookId", admin.BookId);

                    cmd.Parameters.AddWithValue("@AdminName", admin.AdminName);
                    cmd.Parameters.AddWithValue("@Email", admin.Email);
                    cmd.Parameters.AddWithValue("@password", admin.Password);
                    connection.Open();
                   var result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return "Admin Alredy exists";
                    }
                    else if (result == 2)
                    {
                        return "Only one Admin Needed";

                    }
                    else
                    {
                        return "Admin Added successfully";
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
        public string Login(string Email, string Password)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    AdminModel model = new AdminModel();
                    SqlCommand command = new SqlCommand("sp_AdminLogin", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            model.Email = dr["Email"].ToString();
                            model.Password = dr["Password"].ToString();
                        }
                        string token = GenerateToken(Email);
                        return token;
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
        private static string GenerateToken(string Email)
        {
            if (Email == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", Email)

                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        public string AdminAddBook(AdminBookModel admin)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_AddAdminforBook", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AdminId", admin.AdminId);
                    cmd.Parameters.AddWithValue("@BookId", admin.BookId);
                    cmd.Parameters.AddWithValue("@BookName", admin.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", admin.AuthorName);
                    cmd.Parameters.AddWithValue("@DiscountPrice", admin.DiscountPrice);
                    cmd.Parameters.AddWithValue("@OriginalPrice", admin.OriginalPrice);
                    cmd.Parameters.AddWithValue("@BookDescription", admin.BookDescription);
                    cmd.Parameters.AddWithValue("@Rating", admin.Rating);
                    cmd.Parameters.AddWithValue("@Reviewer", admin.Reviewer);
                    cmd.Parameters.AddWithValue("Image", admin.Image);
                    cmd.Parameters.AddWithValue("@BookCount", admin.BookCount);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return "Admin Added Books";
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

        public bool AdminUpdateBooK(int BookId, int AdminId, AdminUpdateBook admin)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_AdminUpdateBook", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdminId", AdminId);
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    cmd.Parameters.AddWithValue("@BookName", admin.BookName);
                    cmd.Parameters.AddWithValue("@BookCount", admin.BookCount);
                    connection.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result == 1)
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
        public bool AdminDeleteBook(int BookId)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_AdminDeleteBookbyBookId", connection);
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

        public List<AdminBookModel> AdminGetAllBooks()
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                List<AdminBookModel> model = new List<AdminBookModel>();
                SqlCommand cmd = new SqlCommand("sp_AdminGetAllBooks", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AdminBookModel books = new AdminBookModel();
                        books.AdminId = Convert.ToInt32(dr["AdminId"]);
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

                        model.Add(books);
                    }
                    return model;
                }
                else
                {
                    return null;
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

