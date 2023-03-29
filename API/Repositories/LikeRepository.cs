using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepo;
        public LikeRepository(DataContext context, IUserRepository userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }

        public async Task<List<BookDto>> GetUserFavoriteBooks(string username)
        {
            var user = await _userRepo.GetUserByUsername(username);

            if(user == null)
            {
                throw new ArgumentException("Something gone wrong, try again.");
            }

             user.LikedBooks ??= new List<Book>();
            
            var bookDtos = user.LikedBooks.Select(x => new BookDto
                {
                    Title = x.Title,
                    Author = x.Author,
                    PublicationYear = x.PublicationYear,
                    Description = x.Description,
                    Count = x.Count
                })
                .ToList();

            await _context.SaveChangesAsync();

            return bookDtos;
        }

        public async Task AddBookToFavorites(int bookId, string username)
        {
            var book = await _context.Books.FindAsync(bookId);

            if(book == null)
            {
               throw new ArgumentException("Book with provided ID does not exist.");
            }
            
            var user = await _userRepo.GetUserByUsername(username);

            if(user == null)
            {
                throw new ArgumentException("User with provided username does not exist.");
            }

            if(user.LikedBooks == null)
            {
                user.LikedBooks = new List<Book>();
            }

            if(user.LikedBooks.Any(x => x.Id == bookId))
            {
                throw new ArgumentException("This book is already in your Liked list!");
            }

            user.LikedBooks.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveBookFromFavorites(int bookId, string username)
        {
            var book = await _context.Books.FindAsync(bookId);

            if(book == null)
            {
               throw new ArgumentException("Book with provided ID does not exist.");
            }

            var user = await _userRepo.GetUserByUsername(username);

            if(user == null)
            {
                throw new ArgumentException("User with provided username does not exist.");
            }

            if(user.LikedBooks.Contains(book))
            {
                user.LikedBooks.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}