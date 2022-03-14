using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
  public interface IFeedbackRL
    {
        string AddFeedback(FeedbackModel feedback);

        List<FeedBack> GetFeedbackDetails(int BookId);
    }
}
