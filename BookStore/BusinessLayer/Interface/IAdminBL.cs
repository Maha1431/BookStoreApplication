using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface IAdminBL
    {
        string AddAdmin(AdminModel admin);

        string Login(string Email, string Password);

        string AdminAddBook(AdminBookModel admin);

        bool AdminUpdateBook(int BookId, int AdminId, AdminUpdateBook admin);

        bool AdminDeleteBook(int BookId);

        List<AdminBookModel> AdminGetAllBooks();
    }
}
