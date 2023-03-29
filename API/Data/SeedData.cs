using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Data
{
    public class SeedData
    {
        public static async Task SeedAsync(DataContext context, ILoggerFactory loggerFactory)
        {
            if(!context.Books.Any())
            {
                var booksData = File.ReadAllText("C:\\Users\\szzie\\OneDrive\\Pulpit\\.netcore\\GithubApps\\LibraryApp\\API\\Data\\SampleData\\books.json");
                var books = JsonSerializer.Deserialize<List<Book>>(booksData);

                foreach (var book in books)
                {
                    context.Books.Add(book);
                }

                    await context.SaveChangesAsync();
            }
        }
    }
}