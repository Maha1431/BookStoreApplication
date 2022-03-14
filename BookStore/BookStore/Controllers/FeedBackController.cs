using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class FeedBackController : ControllerBase
    {
        IFeedbackBL feedbackBL;
        public FeedBackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }
        [HttpPost]
        [Route("addFeedbacks")]
        public IActionResult AddFeedback( FeedbackModel feedback)
        {
            try
            {
                var result = this.feedbackBL.AddFeedback(feedback);
                if (result.Equals("Feedback added successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet("getFeedbacks/{BookId}")]
        public IActionResult GetFeedbackDetails(int BookId)
        {
            try
            {
                var result = this.feedbackBL.GetFeedbackDetails(BookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Get Feedback successful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Retrival unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

    }
}
