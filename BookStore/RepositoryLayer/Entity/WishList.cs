using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Entity
{
   public class WishList
    {
        public int WishlistId { get; set; }
        public int userId { get; set; }
        public int BookId { get; set; }
        public BookModel Book { get; set; }
    }
}
