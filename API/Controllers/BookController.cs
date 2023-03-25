using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            var books = await _bookRepository.GetBooks();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookByID(int id)
        {
            var book = await _bookRepository.GetBookByID(id);

            return book;
        }

        [HttpPost("AddBook")]
        public async Task<ActionResult<BookDto>> AddBook([FromBody] BookDto bookDto)
        {
            var book = await _bookRepository.AddBook(bookDto);

            return book;
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookRepository.DeleteBook(id);
                return Ok("Book deleted succesfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}