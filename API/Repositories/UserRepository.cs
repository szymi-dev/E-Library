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
            return await _context.Users.Include(x => x.Rentals).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.Include(x => x.Rentals).SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.Include(x => x.Rentals).ToListAsync();
        }
    }
}