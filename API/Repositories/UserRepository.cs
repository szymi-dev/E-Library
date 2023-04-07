using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.Include(x => x.Rentals).Include(x => x.LikedBooks).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.Include(x => x.Rentals).Include(x => x.LikedBooks).SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.Include(x => x.Rentals).Include(x => x.LikedBooks).ToListAsync();
        }

        public decimal GetFee(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            return user?.Fee ?? 0m;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task ResetFeeAsync(User user)
        {
            user.Fee = 0;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}