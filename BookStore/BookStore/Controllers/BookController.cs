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
    public class BookController : ControllerBase
    {
        IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }
        [HttpPost("addNotes")]
        public IActionResult AddNotes(BookModel book)
        {
            try
            {
                var result = this.bookBL.AddBook(book);
                if (result.Equals("Book Added succssfully"))
                {
                    return this.Ok(new { success = true, Message = $"Book Added successfull" });
                }
                else
                {
                    return this.BadRequest(new { success = true, Message = $"Book Added Unsuccessfull" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPut("updatebook/{BookId}")]
        public IActionResult UpdateBook(int BookId, BookModel book)
        {
            try
            {
                var result = this.bookBL.UpdateBook(BookId, book);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Book updated Successfully ", response = book });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpDelete("deletebook/{BookId}")]
        public IActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = this.bookBL.DeleteBook(BookId);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Book deleted Successfully ", response = BookId });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpGet("getAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = this.bookBL.GetAllBooks();
                if (result != null)
                {
                    return this.Ok( new { Status = true, Message = "Retrieval all book details succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No book exists" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet("getallBooksByBookId/{BookId}")]
        public IActionResult GetAllBooksByBookId(int BookId)
        {
            try
            {
                var result = this.bookBL.GetAllBooksbyBookId(BookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrieval of book details succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "BookId doesn't exists" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
