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

        bool UpdateAdmin(int AdminId, AdminModel admin);

        List<Admin> GetAllAdminByAdminId(int AdminId);
    }
}
