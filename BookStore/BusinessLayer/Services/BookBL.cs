using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
   public class BookBL:IBookBL
    {
        IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public string AddBook(BookModel book)
        {
            try
            {
                return this.bookRL.AddBook(book);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool UpdateBook(int BookId, BookModel book)
        {
            try
            {
                if (bookRL.UpdateBook(BookId, book))
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool DeleteBook(int BookId)
        {
            try
            {
                if (bookRL.DeleteBook(BookId))
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Book> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<BookModel> GetAllBooksbyBookId(int BookId)
        {
            try
            {
                return this.bookRL.GetAllBooksbyBookId(BookId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
