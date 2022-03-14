using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public int userId { get; set; }
        public int AddressId { get; set; }
        public int BookId { get; set; }
        public int TotalPrice { get; set; }
        public int BookQuantity { get; set; }
        public string OrderDate { get; set; }
        public BookModel bookModel { get; set; }
    }
}
