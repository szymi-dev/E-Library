using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public decimal Funds { get; set; }
        public decimal Fee { get; set; }
        public List<Rental> Rentals { get; set; }
        public List<Book> LikedBooks { get; set; }
    }
}