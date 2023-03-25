using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooks();
        Task<Book> GetBookByID(int id);
        Task<BookDto> AddBook(BookDto bookDto);
        Task DeleteBook(int id);
    }
}