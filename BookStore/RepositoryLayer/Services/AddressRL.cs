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
   public class AddressRL:IAddressRL
    {
        private SqlConnection connection;
        public IConfiguration Configuration { get; }
        public AddressRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddAddress(int userId, AddressModel address)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using(connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_AddAddress", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@Address", address.Address);
                    cmd.Parameters.AddWithValue("@City", address.City);
                    cmd.Parameters.AddWithValue("@State", address.State);
                    cmd.Parameters.AddWithValue("@TypeId", address.TypeId);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return "Address Added successfully";
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

        public bool UpdateAddress(int AddressId, AddressModel model)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using(connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateAddress", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId", AddressId);
                    cmd.Parameters.AddWithValue("@Address", model.Address);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@TypeId", model.TypeId);
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
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }
        public bool DeleteAddress(int AddressId)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteAddress", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId", AddressId);
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

        public List<Addresss> GetAllAddress()
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    List<Addresss> model = new List<Addresss>();
                    SqlCommand cmd = new SqlCommand("sp_GetAllAddresses", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Addresss address = new Addresss();
                            address.AddressId = Convert.ToInt32(dr["AddressId"]);
                            address.Address = dr["Address"].ToString();
                            address.City = dr["City"].ToString();
                            address.State = dr["State"].ToString();
                            address.TypeId = Convert.ToInt32(dr["TypeId"]);
                            model.Add(address);
                        }
                        return model;
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

        public List<Addresss> GetAllAddressbyuserId(int userId)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                List<Addresss> model = new List<Addresss>();
                SqlCommand cmd = new SqlCommand("sp_GetAddressbyuserId", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Addresss address = new Addresss();
                        address.AddressId = Convert.ToInt32(dr["AddressId"]);
                        address.Address = dr["Address"].ToString();
                        address.City = dr["City"].ToString();
                        address.State = dr["State"].ToString();
                        address.TypeId = Convert.ToInt32(dr["TypeId"]);
                        model.Add(address);
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
