using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedbackBL
    {
        string AddFeedback(FeedbackModel feedback);

        List<FeedBack> GetFeedbackDetails(int BookId);
    }
}
