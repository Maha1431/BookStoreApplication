using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAdminRL
    {
        string AddAdmin(AdminModel admin);

        string Login(string Email, string Password);

        string AdminAddBook(AdminBookModel admin);

        bool AdminUpdateBooK(int BookId, int AdminId, AdminUpdateBook admin);

        bool AdminDeleteBook(int BookId);

        List<AdminBookModel> AdminGetAllBooks();
    }
}
