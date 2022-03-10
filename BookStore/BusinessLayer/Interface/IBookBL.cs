using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface IBookBL
    {
        string AddBook(BookModel book);

        bool UpdateBook(int BookId, BookModel book);

        bool DeleteBook(int BookId);

        List<Book> GetAllBooks();

        List<BookModel> GetAllBooksbyBookId(int BookId);

    }
}
