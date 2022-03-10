using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UserBL:IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public async Task RegisterUser(UserPostModel postModel)
        {
            try
            {
                await userRL.RegisterUser(postModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string Login(string Email, string Password)
        {
            try
            {
                return userRL.Login(Email,Password);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool ForgetPassword(string Email)
        {
            try
            {
                return this.userRL.ForgetPassword(Email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(string Email, ResetPassword password)
        {
            try
            {
                return this.userRL.ResetPassword(Email,password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
