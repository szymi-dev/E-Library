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
    public class RentalRepository : IRentalRepository
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        
        public RentalRepository(DataContext context, IUserRepository userRepo, IMapper mapper)
        {
            _context = context;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<RentalDto> RentalBook(int bookId, string username)
        {
            var book = await _context.Books.FindAsync(bookId);
            var user = await _userRepo.GetUserByUsername(username);

            if(book == null)
            {
                throw new ArgumentException("Something gone wrong, there is no book with such Id.");
            }

            if(book.Count <= 0)
            {
                throw new ArgumentException("This book is no longer available!");
            }

            if(user.Fee > 0)
            {
                throw new ArgumentException("You cannot borrow a book until you have paid your fees.");
            }

            if(user.Rentals.Count >= 3)
            {
                throw new ArgumentException("You cannot borrow more books, you have exceeded the borrowing limit.");
            }

            if(user.Rentals.Any(x => x.BookId == bookId))
            {
                throw new ArgumentException("You have already borrow this book.");
            }

            var bookToRent = new Rental
            {
                User = user,
                BookId = book.Id,
                RentalDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(14),
                Status = true
            };

            user.Rentals.Add(bookToRent);

            var bookToReturn = _mapper.Map<Rental, RentalDto>(bookToRent);

            book.Count--;

            await _context.SaveChangesAsync();

            return bookToReturn;
        }

        public async Task ReturnBook(int bookId, string username)
        {
            var user = await _userRepo.GetUserByUsername(username);

            var bookToReturn = user.Rentals.FirstOrDefault(x => x.BookId == bookId);

            var book = await _context.Books.FindAsync(bookId);

            if(bookToReturn == null)
            {
                throw new ArgumentException("There is no such a book in your rentals!");
            }

            if(bookToReturn.Status == false)
            {
                throw new ArgumentException("You have already returned this book!");
            }

            var daysLate = (DateTime.Now - bookToReturn.RentalDate).Days;
            if (daysLate > 0)
            {
                var fee = daysLate * 0.5m;
            }

            //user.Rentals.Find(x =>x.Id == bookToReturn.Id);
            user.Rentals.Remove(bookToReturn);
            book.Count++;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsBookAvaliable(int bookId)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);
            
            return book.Count <= 0;
        }

        public async Task<List<RentalDto>> GetRentals(string username)
        {
            var user = await _userRepo.GetUserByUsername(username);

            if(user == null)
            {
                throw new ArgumentException("Something gone wrong, try again.");
            }

            var rentalDtos = user.Rentals.Select(x => new RentalDto
            {
                Username = user.UserName,
                BookId = x.BookId,
                RentalDate = x.RentalDate,
                ReturnDate = x.ReturnDate
            }).ToList();

            await _context.SaveChangesAsync();
            
            return rentalDtos;
        }
    }
}