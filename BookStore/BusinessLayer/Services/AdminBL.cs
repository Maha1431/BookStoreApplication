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

        public bool UpdateAdmin(int AdminId, AdminModel admin)
        {
            try
            {

                if (adminRL.UpdateAdmin(AdminId, admin))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Admin> GetAllAdminByAdminId(int AdminId)
        {
            try
            {
                return this.adminRL.GetAllAdminByAdminId(AdminId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
