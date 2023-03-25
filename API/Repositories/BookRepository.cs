using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public BookRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Book>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookByID(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<BookDto> AddBook(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                PublicationYear = bookDto.PublicationYear,
                Description = bookDto.Description,
                Count = bookDto.Count
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var bookToReturn = _mapper.Map<Book, BookDto>(book);
            return bookToReturn;
        }

        public async Task DeleteBook(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (book == null)
            {
                throw new ArgumentException("Book with provided ID does not exist.");
            }

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();
        }
    }
}