using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
   public class AdminBL:IAdminBL
    {

        IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public string AddAdmin(AdminModel admin)
        {
            try
            {
                return this.adminRL.AddAdmin(admin);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string Login(string Email, string Password)
        {
            try
            {
                return adminRL.Login(Email, Password);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string AdminAddBook(AdminBookModel admin)
        {
            try
            {
                return this.adminRL.AdminAddBook(admin);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

       
        public bool AdminUpdateBook(int BookId,int AdminId, AdminUpdateBook admin)
        {
            try
            {

                if (adminRL.AdminUpdateBooK(BookId,AdminId, admin))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool AdminDeleteBook(int BookId)
        {
            try
            {
                if (adminRL.AdminDeleteBook(BookId))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AdminBookModel> AdminGetAllBooks()
        {
            try
            {
                return this.adminRL.AdminGetAllBooks();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
