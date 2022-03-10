using CommonLayer;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
   public interface IUserRL
    {
        Task RegisterUser(UserPostModel postModel);

        string Login(string Email, string Password);

        bool ForgetPassword(string Email);

        bool ResetPassword(string Email, ResetPassword password);
    }
}
