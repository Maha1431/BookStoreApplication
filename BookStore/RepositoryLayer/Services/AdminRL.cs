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
                    cmd.Parameters.AddWithValue("@userId", admin.userId);
                    cmd.Parameters.AddWithValue("@AdminName", admin.AdminName);
                    cmd.Parameters.AddWithValue("@Email", admin.Email);
                    cmd.Parameters.AddWithValue("@password", admin.Password);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return "Admin Added succssfully";
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
        public bool UpdateAdmin(int AdminId, AdminModel admin)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateAdminbyAdminId", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdminId", AdminId);
                    cmd.Parameters.AddWithValue("@AdminName", admin.AdminName);
                    cmd.Parameters.AddWithValue("@Email", admin.Email);
                    cmd.Parameters.AddWithValue("@password", admin.Password);
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

        public List<Admin> GetAllAdminByAdminId(int AdminId)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                List<Admin> model = new List<Admin>();
                SqlCommand cmd = new SqlCommand("sp_GetAllAdminbyAdminId", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AdminId", AdminId);
              //  cmd.Parameters.AddWithValue("@userId", userId);
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Admin admin = new Admin();
                        admin.AdminId = Convert.ToInt32(dr["AdminId"]);
                        admin.userId = Convert.ToInt32(dr["userId"]);
                        admin.AdminName = dr["AdminName"].ToString();
                        admin.Email = dr["Email"].ToString();
                        admin.Password = dr["password"].ToString();

                        model.Add(admin);
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

