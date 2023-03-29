using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class FavouriteBook
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
    }
}