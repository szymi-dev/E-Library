using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> GetUserByUsername(string username);
        decimal GetFee(int userId);
        Task UpdateAsync(User user);
        Task ResetFeeAsync(User user);
    }
}