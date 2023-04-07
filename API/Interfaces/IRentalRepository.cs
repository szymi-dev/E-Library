using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.DTOs;

namespace API.Interfaces
{
    public interface IRentalRepository
    {
        Task<List<RentalDto>> GetRentals(string username);
        Task<RentalDto> RentalBook(int bookId, string username);
        Task ReturnBook(int bookId, string username);
        Task<bool> IsBookAvaliable(int bookId);
    }
}