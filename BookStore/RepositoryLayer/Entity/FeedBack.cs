using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Entity
{
   public class FeedBack
    {
		public int FeedbackId { get; set; }
		public int userId { get; set; }
		public int BookId { get; set; }
		public string FeedBackUserName { get; set; }
		public string Comments { get; set; }
		public float Ratings { get; set; }
	//	public BookModel model { get; set; }

	}
}
