using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
   public class CartModel
    {
       // public int CartID { get; set; }
        public int userId { get; set; }
        public int BookId { get; set; }
        public int OrderQuantity { get; set; }
        public BookModel bookModel { get; set; }
    }
}
