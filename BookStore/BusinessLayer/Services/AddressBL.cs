using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
   public class AddressBL:IAddressBL
    {
        IAddressRL addressRL;
        
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }
        public string AddAddress(int userId, AddressModel address)
        {
            try
            {
                return this.addressRL.AddAddress(userId,address);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateAddress(int AddressId, AddressModel model)
        {
            try
            {
                if (addressRL.UpdateAddress(AddressId, model))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool DeleteAddress(int AddressId)
        {
            try
            {
                if (addressRL.DeleteAddress(AddressId))
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Addresss> GetAllAddress()
        {
            try
            {
                return this.addressRL.GetAllAddress();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       
        public List<Addresss> GetAllAddressbyuserId(int userId)
        {
            try
            {
                return this.addressRL.GetAllAddressbyuserId(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
