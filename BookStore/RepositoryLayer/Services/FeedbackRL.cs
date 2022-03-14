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
   public class FeedbackRL:IFeedbackRL
    {

        private SqlConnection connection;
        public IConfiguration Configuration { get; }
        public FeedbackRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddFeedback(FeedbackModel feedback)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("Sp_AddFeedback", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", feedback.userId);
                    cmd.Parameters.AddWithValue("@BookId", feedback.BookId);
                    cmd.Parameters.AddWithValue("@FeedbackUserName", feedback.FeedBackUserName);
                    cmd.Parameters.AddWithValue("@Comments", feedback.Comments);
                    cmd.Parameters.AddWithValue("@Ratings", feedback.Ratings);
                    connection.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result != 1)
                    {
                        return "Feedback added successfully";
                    }
                    else 
                    {
                        return "BookId Not exits";
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
        public List<FeedBack> GetFeedbackDetails(int BookId)
        {
            connection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("sp_GetFeedbacksbyBookId", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<FeedBack> feedback = new List<FeedBack>();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            FeedBack model = new FeedBack();
                            /*BookModel bookModel = new BookModel();
                            bookModel.BookName = dr["BookName"].ToString();
                            bookModel.AuthorName = dr["AuthorName"].ToString();
                            bookModel.DiscountPrice = Convert.ToInt32(dr["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(dr["OriginalPrice"]);
                            bookModel.BookDescription = dr["BookDescription"].ToString();
                            bookModel.Rating = Convert.ToInt32(dr["Rating"]);
                            bookModel.Reviewer = Convert.ToInt32(dr["Reviewer"]);
                            bookModel.Image = dr["Image"].ToString();
                            bookModel.BookCount = Convert.ToInt32(dr["BookCount"]);*/
                            model.FeedbackId = Convert.ToInt32(dr["FeedbackId"]);
                            model.userId = Convert.ToInt32(dr["userId"]);
                            model.BookId = Convert.ToInt32(dr["BookId"]);
                            model.FeedBackUserName = dr["FeedBackUserName"].ToString();
                            model.Comments = dr["Comments"].ToString();
                            model.Ratings = Convert.ToInt32(dr["Ratings"]);
                           
                           // model.model = bookModel;
                            feedback.Add(model);
                        }
                        return feedback;
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

