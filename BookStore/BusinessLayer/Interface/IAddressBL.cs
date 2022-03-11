using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAddressBL
    {
        string AddAddress(int userId, AddressModel address);

        bool UpdateAddress(int AddressId, AddressModel model);

        bool DeleteAddress(int AddressId);

        List<Addresss> GetAllAddress();

        List<Addresss> GetAllAddressbyuserId(int userId);
    }
}
