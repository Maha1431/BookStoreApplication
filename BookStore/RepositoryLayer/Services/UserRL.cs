using CommonLayer;
using CommonLayer.Models;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class UserRL : IUserRL
    {
        private SqlConnection con;

        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task RegisterUser(UserPostModel postModel)
        {
            con = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (con)
                {
                    SqlCommand cmd = new SqlCommand("sp_RegUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FullName", postModel.FullName);
                    cmd.Parameters.AddWithValue("@Email", postModel.Email);
                    cmd.Parameters.AddWithValue("@Password", postModel.Password);
                    cmd.Parameters.AddWithValue("@MobileNo", postModel.MobileNo);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }
        public string Login(string Email, string Password)
        {
            con = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (con)
                {
                    UserPostModel model = new UserPostModel();
                    SqlCommand command = new SqlCommand("sp_UserLogin", con);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    con.Open();
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
                con.Close();
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
                    new Claim("Email", Email),

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
        private static string GenerateJWTToken(string Email, string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", Email),

                   new Claim("userId",userId.ToString())
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
        public bool ForgetPassword(string Email)
        {
            con = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (con)
                {
                    UserPostModel model = new UserPostModel();
                    SqlCommand command = new SqlCommand("sp_ForgetPassword", con);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", Email);
                    con.Open();
                    var result = command.ExecuteNonQuery();

                    MessageQueue queue;
                    //ADD MESSAGE TO QUEUE
                    if (MessageQueue.Exists(@".\Private$\BookStoreQueue"))
                    {
                        queue = new MessageQueue(@".\Private$\BookStoreQueue");
                    }
                    else
                    {
                        queue = MessageQueue.Create(@".\Private$\BookStoreQueue");
                    }

                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = GenerateToken(Email);
                    MyMessage.Label = "Forget Password Email";
                    queue.Send(MyMessage);
                    Message msg = queue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailService.SendEmail(Email, msg.Body.ToString());
                    queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                    queue.BeginReceive();
                    queue.Close();
                    return true;
                }
            }

            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }
        public bool ResetPassword(string Email, ResetPassword password)
        {
            con = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (con)
                {
                    UserPostModel model = new UserPostModel();
                    SqlCommand command = new SqlCommand("sp_ResetPassword", con);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", password.cPassword);
                    con.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            model.Email = dr["Email"].ToString();
                            model.Password = dr["Password"].ToString();
                        }
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
                con.Close();
            }

        }
        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }


            }
        }




    }
}



