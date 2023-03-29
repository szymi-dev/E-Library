using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ILikeRepository
    {
        Task AddBookToFavorites(int bookId, string username);
        Task RemoveBookFromFavorites(int bookId, string username);
        Task<List<BookDto>> GetUserFavoriteBooks(string username);
    }
}